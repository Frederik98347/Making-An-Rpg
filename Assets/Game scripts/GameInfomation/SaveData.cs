using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData{

	public static GameInfoContainer dataContainer = new GameInfoContainer();

	public delegate void SerializeAction();
	public static event SerializeAction OnLoaded;
	public static event SerializeAction OnBeforeSave;

	public static void Load(string path){
		dataContainer = LoadGameData (path);

		foreach (GameData data in dataContainer.List) {
			GameController.CreateInfo (data, GameController.playerPath, data.pos, Quaternion.identity);
		}

		OnLoaded ();

		ClearList ();
	}

	public static void Save(string path, GameInfoContainer dataContainer) {
		OnBeforeSave();

		SaveGameData(path, dataContainer);

		ClearList();
	}

	public static void AddGameData(GameData gameData) {
		dataContainer.List.Add (gameData);
	}

	public static void ClearList () {
		dataContainer.List.Clear ();
	}

	static GameInfoContainer LoadGameData(string path) {
		string json = File.ReadAllText(path);

		return JsonUtility.FromJson<GameInfoContainer> (json);
	}

	static void SaveGameData(string path, GameInfoContainer dataContainer) {
		string json = JsonUtility.ToJson (dataContainer);

		StreamWriter sw = File.CreateText (path);
		sw.Close ();

		File.WriteAllText (path, json);
	}
}