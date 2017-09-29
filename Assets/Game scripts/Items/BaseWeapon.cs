using UnityEngine;
using System.Collections;

public class BaseWeapon : BaseStatItem {

	public enum WeaponTypes{
		DAGGER,
		STAFF,
		SWORD,
		POLEARM,                    
		BOW,
		AXE,
		SHIELD
	}

	private WeaponTypes weaponType;
	private int spellEffectID;

	public WeaponTypes WeaponType{
		get{return weaponType;}
		set{weaponType = value;}

	}
	public int SpellEffectID{
		get{return spellEffectID;}
		set{spellEffectID = value;}
	}
}