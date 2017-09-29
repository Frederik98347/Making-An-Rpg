﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyCreator : EditorWindow {
	[MenuItem("Rpg Tools/EnemyCreator")]
	static void Init() {

		EnemyCreator enemyWindow = (EnemyCreator)CreateInstance(typeof(EnemyCreator));
		enemyWindow.Show();
	}

	AIConfig tempEnemy = null;
	EnemyManager enemyManager = null;

	void OnGUI() {

		if (enemyManager == null) {

			enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		}

		if (tempEnemy) {

			tempEnemy.EnemyName = EditorGUILayout.TextField("Enemy Name", tempEnemy.EnemyName);
			tempEnemy.EnemyHP = EditorGUILayout.IntField("Health", tempEnemy.EnemyHP);
			tempEnemy.EnemyLevel = EditorGUILayout.IntField("Level", tempEnemy.EnemyLevel);
			tempEnemy.EnemyIcon = (Texture2D)EditorGUILayout.ObjectField("Spell Icon", tempEnemy.EnemyIcon, typeof(Texture2D), false);
			tempEnemy.MinAutoDamage = EditorGUILayout.IntField("MinAutoDamage", tempEnemy.MinAutoDamage);
			tempEnemy.MaxAutoDamage = EditorGUILayout.IntField("MaxAutoDamage", tempEnemy.MaxAutoDamage);
			tempEnemy.AttackSpeed = EditorGUILayout.DoubleField("AttackSpeed", tempEnemy.AttackSpeed);
			tempEnemy.MovementSpeed = EditorGUILayout.DoubleField("MovementSpeed", tempEnemy.MovementSpeed);
			tempEnemy.DetectionRange = EditorGUILayout.DoubleField("DetectionRange", tempEnemy.DetectionRange);
			tempEnemy.abilityPrefab_1 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.abilityPrefab_1, typeof(GameObject), false);
			tempEnemy.abilityPrefab_2 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.abilityPrefab_2, typeof(GameObject), false);
			tempEnemy.abilityPrefab_3 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.abilityPrefab_3, typeof(GameObject), false);
			tempEnemy.abilityPrefab_4 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.abilityPrefab_4, typeof(GameObject), false);

		}

		EditorGUILayout.Space();

		if(tempEnemy == null) {

			if(GUILayout.Button("Create Enemy")) {

				tempEnemy = CreateInstance<AIConfig>();
			}

		} else if (GUILayout.Button("Create Scriptable Object")) {
			AssetDatabase.CreateAsset(tempEnemy, "Assets/Resources/Enemies/" + tempEnemy.EnemyName + ".asset");
			AssetDatabase.SaveAssets();
			enemyManager.enemyList.Add(tempEnemy);
			Selection.activeObject = tempEnemy;

			tempEnemy = null;
		}

		if(GUILayout.Button("Reset")) {
			Reset();

		}

	}
	void Reset (){

		if(tempEnemy) {

			tempEnemy.EnemyName = "";
			tempEnemy.EnemyHP = 0;
			tempEnemy.EnemyLevel = 0;
			tempEnemy.EnemyIcon = null;
			tempEnemy.MinAutoDamage = 0;
			tempEnemy.MaxAutoDamage = 0;
			tempEnemy.AttackSpeed = 0;
			tempEnemy.MovementSpeed = 0;
			tempEnemy.DetectionRange = 0;
			tempEnemy.abilityPrefab_1 = null;
			tempEnemy.abilityPrefab_2 = null;
			tempEnemy.abilityPrefab_3 = null;
			tempEnemy.abilityPrefab_4 = null;

		}
	}		
}
