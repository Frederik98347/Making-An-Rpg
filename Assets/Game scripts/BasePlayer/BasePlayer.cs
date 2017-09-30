using UnityEngine;
using System.Collections;

public class BasePlayer{
	public Player player;

	string playerName;
	int playerLevel;

	BaseCharacterClass playerClass;
	private int stamina;
	private int agility;
	private int strength;
	private int intellect;


	public string PlayerName {
		get{ return playerName;}
		set{player.CharacterName = value;}
	}

	public int PlayerLevel {
		get{ return playerLevel;}
		set{player.Level = value;}
	}

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

