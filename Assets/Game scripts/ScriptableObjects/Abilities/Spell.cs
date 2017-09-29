using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject {
	
	public string spellName = "";
	public string spellDescription = "";
	public GameObject spellPrefab = null;
	public GameObject spellCollisionParticle = null;
	public Texture2D spellIcon = null;
	
	public int ManaCost = 0;
	public int RageCost = 0;
	public int EnergyCost = 0;
	
	public int MinRange = 0;
	public int MaxRange = 0;
	
	public int MinDamage = 0;
	public int MaxDamage = 0;
	
	public int projectileSpeed = 0;	
}