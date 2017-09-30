using UnityEngine;
using System.Collections;

public class AIConfig : ScriptableObject
{
	public int EnemyHP;
	public string EnemyName = "";
	public int EnemyLevel = 1;
	public int expTogive = 0;
	public int MinAutoDamage = 0;
	public int MaxAutoDamage = 0;
	public Texture2D EnemyIcon = null;

	public float AttackSpeed = 0f;
	public float MovementSpeed = 0f;
	public float DetectionRange = 0f;

	// ai abilities
	public GameObject abilityPrefab_1 = null;
	public GameObject abilityPrefab_2 = null;
	public GameObject abilityPrefab_3 = null;
	public GameObject abilityPrefab_4 = null;
}