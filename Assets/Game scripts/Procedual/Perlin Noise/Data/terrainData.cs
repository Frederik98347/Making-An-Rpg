using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class terrainData : ScriptableObject {

	public float uniformScale = 2.5f;

	public bool useFlatShading;
	public bool useFalloff;

	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;
}
