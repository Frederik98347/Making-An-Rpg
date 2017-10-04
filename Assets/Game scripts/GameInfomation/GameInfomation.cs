using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInfomation : MonoBehaviour {
	public BasePlayer newPlayer;
	public CreateNewCharacter newChar;
	public GameData data = new GameData ();

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	public void StoreInfo (){
		if (newPlayer != null && data != null && newChar != null) {
			
			//Storing into GameData class
			data.CharacterIndex = newChar.CharacterIndex;
			data.name = newPlayer.PlayerName;
			data.pos = GetComponent<Player> ().transform.position;
			data.health = GetComponent<Player> ().health;

			data.level = newPlayer.PlayerLevel;
			data.Class = newPlayer.PlayerClass;

			data.agility = newPlayer.Agility;
			data.intellect = newPlayer.Intellect;
			data.stamina = newPlayer.Stamina;
			data.strength = newPlayer.Strength;
		} else {
			Debug.Log ("Unable to save");
		}
	}

	public void LoadData() {

		if (newPlayer != null && data != null && newChar != null) {
			
			//Stats
			newPlayer.Stamina = data.stamina;
			newPlayer.Strength = data.strength;
			newPlayer.Agility = data.agility;
			newPlayer.Intellect = data.intellect;
			newPlayer.PlayerLevel = data.level;

			//Location && health from player Script
			GetComponent<Player> ().transform.position = data.pos;
			GetComponent<Player> ().health = data.health;

			newPlayer.PlayerClass = data.Class;
			newChar.CharacterIndex = data.CharacterIndex;
		} else {
			Debug.LogError ("Unable to load Data");
		}
	}

	public void ApplyData(){
		SaveData.AddGameData (data);
	}

	void OnEnable() {
		SaveData.OnLoaded += LoadData;
		SaveData.OnBeforeSave += StoreInfo;
		SaveData.OnBeforeSave += ApplyData;
	}

	void OnDisable() {
		SaveData.OnLoaded -= LoadData;
		SaveData.OnBeforeSave -= StoreInfo;
		SaveData.OnBeforeSave += ApplyData;
	}
}

[Serializable]
public class GameData {
	//Amount of characters
	public int CharacterIndex;

	public string name;
	public Vector3 pos;
	public float health;
	public int level;

	//Class
	public BaseCharacterClass Class;

	//Stats
	public int agility;
	public int intellect;
	public int stamina;
	public int strength;

	//Inventory
	//ItemsEquipped
}