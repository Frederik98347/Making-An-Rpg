using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public class mapGenerator : MonoBehaviour {

    public enum Drawmode {noiseMap, colourMap, Mesh, FalloffMap };
    public Drawmode drawMode;

	public Perlin_Noise.NormalizeMode normalizeMode;
	[Space]
	[Header("Details")]
	public const int mapChunkSize = 239;
	[Range(0,6)]
	public int editorPreviewLOD;
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
	public bool useFalloff;
	[Space]
	[Header("Area types to generate")]
    public TerrainType[] regions;

	float [,] falloffMap;

	Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
	Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

	void Awake() {
		falloffMap = FallOutGeneration.GenerateFallofMap (mapChunkSize);
	}

	public void DrawMapInEditor() {
		MapData mapData = GenerateMapData (Vector2.zero);

		mapDisplay display = GetComponent<mapDisplay>();
		if (drawMode == Drawmode.noiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (mapData.heightMap));
		} else if (drawMode == Drawmode.colourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap (mapData.colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == Drawmode.Mesh) {
			display.DrawMesh (meshGenerator.GenerateTerrainMesh (mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorPreviewLOD), TextureGenerator.TextureFromColourMap (mapData.colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == Drawmode.FalloffMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (FallOutGeneration.GenerateFallofMap (mapChunkSize)));
		}
	
	}

	public void RequestMapData(Vector2 centre, Action<MapData> callback) {
		ThreadStart threadStart = delegate {
			MapDataThread (centre, callback);
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

	void MapDataThread(Vector2 centre, Action<MapData> callback) {
		MapData mapData = GenerateMapData (centre);

		lock (mapDataThreadInfoQueue) {
			mapDataThreadInfoQueue.Enqueue (new MapThreadInfo<MapData> (callback, mapData));
		}
	}

	public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback) {
		ThreadStart threadStart = delegate {
			MeshDataThread (mapData, lod, callback);
		};

		new Thread (threadStart).Start ();
	}

	void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback) {
		MeshData meshData = meshGenerator.GenerateTerrainMesh (mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod);
		lock (meshDataThreadInfoQueue) {
			meshDataThreadInfoQueue.Enqueue (new MapThreadInfo<MeshData> (callback, meshData));
		}
	}


	MapData GenerateMapData(Vector2 centre)
    {
		float[,] noiseMap = Perlin_Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacurnarity, centre + offset, normalizeMode);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++) {
            for (int x = 0; x < mapChunkSize; x++) {
				
				if (useFalloff) {
					noiseMap [x, y] = Mathf.Clamp01(noiseMap [x, y] - falloffMap [x, y]);
				}

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
					if (currentHeight >= regions [i].Height) {
						colourMap [y * mapChunkSize + x] = regions [i].colour;
					} else {
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

		falloffMap = FallOutGeneration.GenerateFallofMap (mapChunkSize);
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
}

    [System.Serializable]
    public struct TerrainType {
        public string name;
        public float Height;
        public Color colour;
    }

public struct MapData {
	public readonly float[,] heightMap;
	public readonly Color[] colourMap;

	public MapData (float[,] heightMap, Color[] colourMap){
		this.heightMap = heightMap;
		this.colourMap = colourMap;
	}
}
