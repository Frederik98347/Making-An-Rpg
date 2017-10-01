using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public class mapGenerator : MonoBehaviour {

    public enum Drawmode {noiseMap, colourMap, Mesh };
    public Drawmode drawMode;

	[Space]
	[Header("Details")]
	public const int mapChunkSize = 241;
	[Range(0,6)]
	public int levelofDetail;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacurnarity;

    public int seed;
    public Vector2 offset;
	[Space]
	[Header("Mesh Manipulation")]
	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;

    public bool AutoUpdate;
	[Space]
	[Header("Area types to generate")]
    public TerrainType[] regions;

	Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
	Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

	public void DrawMapInEditor() {
		MapData mapData = GenerateMapData ();

		mapDisplay display = GetComponent<mapDisplay>();
		if (drawMode == Drawmode.noiseMap) {
			display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
		} else if (drawMode == Drawmode.colourMap) {
			display.DrawTexture(TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == Drawmode.Mesh) {
			display.DrawMesh (meshGenerator.GenerateTerrainMesh (mapData.heightMap, meshHeightMultiplier, meshHeightCurve, levelofDetail), TextureGenerator.TextureFromColourMap (mapData.colourMap, mapChunkSize, mapChunkSize));
		}
	
	}

	public void RequestMapData(Action<MapData> callback) {
		ThreadStart threadStart = delegate {
			MapDataThread (callback);
		};

		new Thread (threadStart).Start ();
	}

	void Update() {
		if (mapDataThreadInfoQueue.Count > 0) {
			for (int i = 0; i < mapDataThreadInfoQueue.Count; i++) {
				MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue ();
				threadInfo.callback (threadInfo.parameter);
			}
		}

		if (meshDataThreadInfoQueue.Count > 0) {
			for (int i = 0; i < meshDataThreadInfoQueue.Count; i++) {
				MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue ();
				threadInfo.callback (threadInfo.parameter);
			}
		}
	}

	void MapDataThread(Action<MapData> callback) {
		MapData mapData = GenerateMapData ();

		lock (mapDataThreadInfoQueue) {
			mapDataThreadInfoQueue.Enqueue (new MapThreadInfo<MapData> (callback, mapData));
		}
	}

	public void RequestMeshData(MapData mapData, Action<MeshData> callback) {
		ThreadStart threadStart = delegate {
			MeshDataThread (mapData, callback);
		};

		new Thread (threadStart).Start ();
	}

	void MeshDataThread(MapData mapData, Action<MeshData> callback) {
		MeshData meshData = meshGenerator.GenerateTerrainMesh (mapData.heightMap, meshHeightMultiplier, meshHeightCurve, levelofDetail);
		lock (meshDataThreadInfoQueue) {
			meshDataThreadInfoQueue.Enqueue (new MapThreadInfo<MeshData> (callback, meshData));
		}
	}


    MapData GenerateMapData()
    {
        float[,] noiseMap = Perlin_Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacurnarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++) {
            for (int x = 0; x < mapChunkSize; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].Height) {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

		return new MapData (noiseMap, colourMap);
    }

    void OnValidate() {
       
        if (lacurnarity < 1)
        {
            lacurnarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        } 
    }

	struct MapThreadInfo<T> {
		public readonly Action<T> callback;
		public readonly T parameter;

		public MapThreadInfo (Action<T> callback, T parameter)
		{
			this.callback = callback;
			this.parameter = parameter;
		}
		
	}

    [System.Serializable]
    public struct TerrainType {
        public string name;
        public float Height;
        public Color colour;
    }
}

public struct MapData {
	public readonly float[,] heightMap;
	public readonly Color[] colourMap;

	public MapData (float[,] heightMap, Color[] colourMap){
		this.heightMap = heightMap;
		this.colourMap = colourMap;
	}
}
