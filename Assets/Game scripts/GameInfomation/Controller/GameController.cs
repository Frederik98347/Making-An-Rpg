using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;
	public const string playerPath = "Resources/PlayerPrefab";

	static string dataPath = string.Empty;

	void Awake() {
		dataPath = System.IO.Path.Combine (Application.persistentDataPath, "Gameinfomation.json");
	}

	public static GameInfomation CreateInfo (string path, Vector3 pos, Quaternion rotation) {
		GameObject prefab = Resources.Load<GameObject> (path);
		GameObject go = Instantiate (prefab, pos, rotation) as GameObject;

		GameInfomation gameData = go.GetComponent<GameInfomation> () ?? go.AddComponent<GameInfomation> ();

		return gameData;
	}

	public static GameInfomation CreateInfo (GameData data, string path, Vector3 pos, Quaternion rotation) {
		GameInfomation Gamedata = CreateInfo(path, pos, rotation);
		Gamedata.data = data;

		return Gamedata;
	}

	public void Save() {
		SaveData.Save (dataPath, SaveData.dataContainer);
	}

	public void Load() {
		SaveData.Load (dataPath);
	}
}
