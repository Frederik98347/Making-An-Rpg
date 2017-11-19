using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInfomation : MonoBehaviour {
	public CreateNewCharacter newChar;
	public GameData data = new GameData ();

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	public void StoreInfo (){
		if (newChar.nameCreated == true && newChar.hasCreated == true) {
			
			//Storing into GameData class
			data._CharacterIndex = newChar.CharacterIndex;
			data.name = newChar.newPlayer.PlayerName;
			data.pos = GetComponent<Player> ().transform.position;
			data.rotation = GetComponent<Player> ().transform.rotation;
			data.health = GetComponent<Player> ().health;

			data.level = newChar.newPlayer.PlayerLevel;
			data.Class = newChar.newPlayer.PlayerClass.CharacterClassName;

			data.agility =  newChar.newPlayer.Agility;
			data.intellect = newChar.newPlayer.Intellect;
			data.stamina = newChar.newPlayer.Stamina;
			data.strength = newChar.newPlayer.Strength;
		} else {
			Debug.Log ("Unable to save");
		}
	}

	public void LoadData() {

		if (data != null) {
			
			//Stats
			newChar.newPlayer.Stamina = data.stamina;
			newChar.newPlayer.Strength = data.strength;
			newChar.newPlayer.Agility = data.agility;
			newChar.newPlayer.Intellect = data.intellect;
			newChar.newPlayer.PlayerLevel = data.level;

			//Location && health from player Script
			GetComponent<Player> ().transform.position = data.pos;
			GetComponent<Player> ().transform.rotation = data.rotation;
			GetComponent<Player> ().health = data.health;

			newChar.newPlayer.PlayerClass.CharacterClassName = data.Class;
			newChar.newPlayer.PlayerName = data.name;
			newChar.CharacterIndex = data._CharacterIndex;
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
	public int _CharacterIndex;

	public string name;
	public Vector3 pos;
	public Quaternion rotation;
	public float health;
	public int level;

	//Class
	public string Class;

	//Stats
	public int agility;
	public int intellect;
	public int stamina;
	public int strength;

	//Inventory
	//ItemsEquipped
}