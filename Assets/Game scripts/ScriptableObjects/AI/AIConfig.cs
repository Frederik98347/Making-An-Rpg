using UnityEngine;
using System.Collections;

public class AIConfig : ScriptableObject
{
	public Animation attack = null;
	public Animation Idle = null;
	public Animation Die = null;
	public Animation Run = null;
	public Animation Walk = null;

	public int EnemyHP;
	public string EnemyName = "";
	public int EnemyLevel = 1;
	public int MinAutoDamage = 0;
	public int MaxAutoDamage = 0;
	public Texture2D EnemyIcon = null;
	public double AttackSpeed = 0;
	public double MovementSpeed = 0;
	public double DetectionRange = 0;

	// ai abilities
	public GameObject abilityPrefab_1 = null;
	public GameObject abilityPrefab_2 = null;
	public GameObject abilityPrefab_3 = null;
	public GameObject abilityPrefab_4 = null;

	//Audio
	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;
}