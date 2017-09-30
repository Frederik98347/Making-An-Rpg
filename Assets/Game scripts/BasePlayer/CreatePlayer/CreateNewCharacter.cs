using UnityEngine;
using System.Collections;

public class CreateNewCharacter : MonoBehaviour
{
	BasePlayer newPlayer;
	bool isMageClass;
	bool isWarriorClass;
	bool isRogueClass;
	// Use this for initialization
	void Start ()
	{
		newPlayer = new BasePlayer();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
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

			if (isMageClass) {
				newPlayer.PlayerClass = new BaseMageClass ();
			} else if (isWarriorClass) {
				newPlayer.PlayerClass = new BaseWarriorClass ();
			} else if (isRogueClass) {
				newPlayer.PlayerClass = new BaseRogueClass ();
			}

			//Set Different Class Stats
			newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
			newPlayer.Agility = newPlayer.PlayerClass.Agility;
			newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
			newPlayer.Strength = newPlayer.PlayerClass.Strength;

			Debug.Log ("Player class: " + newPlayer.PlayerClass.CharacterClassName + "/n Stats: Agility " + newPlayer.PlayerClass.Agility + ": Strength " + newPlayer.PlayerClass.Strength + ": Intellect " +newPlayer.PlayerClass.Intellect + ": Stamina " + newPlayer.PlayerClass.Stamina );
		}
	}
}

