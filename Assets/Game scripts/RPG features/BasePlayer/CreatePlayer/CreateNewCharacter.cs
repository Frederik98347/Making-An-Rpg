using UnityEngine;
using System.Collections;

public class CreateNewCharacter : MonoBehaviour {
	public BasePlayer newPlayer;
	public Player player;

	bool isMageClass;
	bool isWarriorClass;
	bool isRogueClass;
	public int CharacterIndex;
	int CharacterIndexMax = 10;

	// Use this for initialization
	void Start () {
		newPlayer = new BasePlayer();

		if (player.CharacterName == null) {
			player.CharacterName = "Enter Name";
		}
	}

	void OnGUI() {

		if(GUI.Toggle(new Rect(10, 10, 50, 50), isMageClass, "Mage Class")) {
			isMageClass = true;
			isWarriorClass = false;
			isRogueClass = false;
		}

		if(GUI.Toggle(new Rect(10,10, 50, 50), isWarriorClass, "Warrior Class")) {
			isMageClass = false;
			isWarriorClass = true;
			isRogueClass = false;
		}

		if(GUI.Toggle(new Rect(10,10, 50, 50), isRogueClass, "Rogue class")) {
			isMageClass = false;
			isWarriorClass = false;
			isRogueClass = true;
		}

		if (GUILayout.Button ("Create")) {
			if (CharacterIndex <= CharacterIndexMax) {
				
				for (int i = 0; i <= CharacterIndexMax; i++) {
					CharacterIndex = i;
				
					if (isMageClass) {
						newPlayer.PlayerClass = new BaseMageClass ();
					} else if (isWarriorClass) {
						newPlayer.PlayerClass = new BaseWarriorClass ();
					} else if (isRogueClass) {
						newPlayer.PlayerClass = new BaseRogueClass ();
					}
				}

			} else {
				Debug.Log ("You cant Create more Characters");
			}
		}

			//Set Different Class Stats
		if (newPlayer != null && player != null && newPlayer.PlayerClass != null) {
			newPlayer.PlayerLevel = player.Level;
			newPlayer.Stamina = this.newPlayer.PlayerClass.Stamina;
			newPlayer.Agility = this.newPlayer.PlayerClass.Agility;
			newPlayer.Intellect = this.newPlayer.PlayerClass.Intellect;
			newPlayer.Strength = this.newPlayer.PlayerClass.Strength;

			if (player.CharacterName == GUILayout.TextField (player.CharacterName, 12)) {
				newPlayer.PlayerName = player.CharacterName;
			} else {
				newPlayer.PlayerName = player.CharacterName;
			}
		}
	}
}