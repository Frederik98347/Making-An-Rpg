﻿using System.Collections;
using UnityEngine;

public static class Perlin_Noise {
	
	public enum NormalizeMode{Local, Global};
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode) {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;

        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

			maxPossibleHeight += amplitude;
			amplitude *= persistance;
        }

        if (scale<= 0) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float ampltitude = 1;
                //float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
					float sampleX = (x-halfWidth + octaveOffsets[i].x) / scale * frequency;
					float sampleY = (y-halfHeight + octaveOffsets[i].y) / scale * frequency;

                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
                        noiseHeight += perlinValue * ampltitude;

                        ampltitude *= persistance;
                        frequency *= lacunarity;
                        //noiseMap[x, y] = perlinValue;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y=0; y<mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
				if (normalizeMode == NormalizeMode.Local) {
					noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]); // returns 0 BETWEEN 1
				} else {
					float normalizedHeight = (noiseMap[x,y] + 1) / (2f*maxPossibleHeight / 2f); // estimated divistion, optimazion could be to find difference of heights > 1 and then use that instead as division
					noiseMap [x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
				}
                
            }

        }

        return noiseMap;
    }

}

    