using UnityEngine;
using System.Collections;

public class BaseStatItem : BaseItem {

	private int stamina;
	private int agility;
	private int strength;
	private int intellect;
	private int critchance;

	public int Stamina{
		get {return stamina;}
		set {stamina = value;}
	}
	public int Agility{
		get {return agility;}
		set {agility = value;}
	}
	public int Strength{
		get {return strength;}
		set {strength = value;}
	}
	public int Intellect{
		get {return intellect;}
		set {intellect = value;}
	}
	public int CritChance{
		get {return critchance;}
		set {critchance = value;}
	}
}
