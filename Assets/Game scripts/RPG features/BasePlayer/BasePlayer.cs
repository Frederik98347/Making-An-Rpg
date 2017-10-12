using UnityEngine;
using System.Collections;

public class BasePlayer {

	BaseCharacterClass playerClass;
	private int stamina;
	private int agility;
	private int strength;
	private int intellect;


	public string PlayerName{ get; set;}

	public int PlayerLevel{ get; set;}

	public BaseCharacterClass PlayerClass{
		get{ return playerClass; }
		set{playerClass = value;}
	}

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
}