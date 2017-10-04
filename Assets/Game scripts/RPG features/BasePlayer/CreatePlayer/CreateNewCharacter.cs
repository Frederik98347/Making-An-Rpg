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

		if (player.CharacterName != "Enter Name" || player.CharacterName == null) {
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
			this.newPlayer.PlayerLevel = player.Level;
			this.newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
			this.newPlayer.Agility = newPlayer.PlayerClass.Agility;
			this.newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
			this.newPlayer.Strength = newPlayer.PlayerClass.Strength;

			if (player.CharacterName == GUILayout.TextField (player.CharacterName, 12)) {
				this.newPlayer.PlayerName = player.CharacterName;
			} else {
				this.newPlayer.PlayerName = player.CharacterName;
			}
	}
}