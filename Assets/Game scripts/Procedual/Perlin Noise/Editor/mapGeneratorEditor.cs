﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (mapGenerator))]
public class mapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		mapGenerator mapGen = (mapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.AutoUpdate) {
				mapGen.DrawMapInEditor();
			}
		}

		if (GUILayout.Button ("Generate")) {
			mapGen.DrawMapInEditor();
		}
	}
}
