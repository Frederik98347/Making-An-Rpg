using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

    public enum Drawmode {noiseMap, colourMap, Mesh };
    public Drawmode drawMode;
    //public TextureGenerator textureGenerator;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacurnarity;

    public int seed;
    public Vector2 offset;

    public bool AutoUpdate;

    public TerrainType[] regions;


    public void GenerateMap()
    {
        float[,] noiseMap = Perlin_Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacurnarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].Height) {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        mapDisplay display = GetComponent<mapDisplay>();
        if (drawMode == Drawmode.noiseMap) {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == Drawmode.colourMap) {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
		} else if (drawMode == Drawmode.Mesh) {
			display.DrawMesh (meshGenerator.GenerateTerrainMesh (noiseMap), TextureGenerator.TextureFromColourMap (colourMap, mapWidth, mapHeight));
		}

    }

    void OnValidate() {
        if (mapWidth < 1) {
            mapWidth = 1;
       }

        if (mapHeight < 1) {
            mapHeight = 1;
        }

        if (lacurnarity < 1)
        {
            lacurnarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }
    [System.Serializable]
    public struct TerrainType {
        public string name;
        public float Height;
        public Color colour;
    }
}
