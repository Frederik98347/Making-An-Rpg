using System.Collections;
using UnityEngine;
[CreateAssetMenu()]
public class NoiseData : ScriptableObject {

	//Setting variables
	public Perlin_Noise.NormalizeMode normalizeMode;
	public float noiseScale;
	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacurnarity;

	public int seed;
	public Vector2 offset;

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
}
