using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class CreateNewCharacter : MonoBehaviour {
	public BasePlayer newPlayer;
	GameController gameController;

	bool isMageClass;
	bool isWarriorClass;
	bool isRogueClass;
	public bool hasCreated;

	public int CharacterIndex;
	int CharacterIndexMax = 10;

	// Use this for initialization
	void Start () {
		newPlayer = new BasePlayer();
		newPlayer.PlayerName = "Enter Name";
	}

	void OnGUI() {

		if(GUI.Toggle(new Rect(10, 40, 100, 30), isMageClass, "Mage Class")) {
			isMageClass = true;
			isWarriorClass = false;
			isRogueClass = false;
		}

		if(GUI.Toggle(new Rect(10,20, 100, 30), isWarriorClass, "Warrior Class")) {
			isMageClass = false;
			isWarriorClass = true;
			isRogueClass = false;
		}

		if(GUI.Toggle(new Rect(10,60, 100, 30), isRogueClass, "Rogue class")) {
			isMageClass = false;
			isWarriorClass = false;
			isRogueClass = true;
		}

		if (GUI.Button (new Rect(10,110, 50, 25),("Create"))) {
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

					hasCreated = true;
				}
			}
		}

		if (newPlayer.PlayerName != "Enter Name" && newPlayer.PlayerName != null) {
			newPlayer.PlayerName = GUI.TextField (new Rect (10, 85, 100, 20), newPlayer.PlayerName, 12);
			newPlayer.PlayerName = Regex.Replace (newPlayer.PlayerName, @"[^a-zA-Z]", "");

			newPlayer.PlayerName = this.newPlayer.PlayerName;
		} else {
			newPlayer.PlayerName = GUI.TextField (new Rect (10, 85, 100, 20), newPlayer.PlayerName, 12);
			newPlayer.PlayerName = Regex.Replace (newPlayer.PlayerName, @"[^a-zA-Z]", "");
		}

			//Set Different Class Stats
		if (newPlayer != null && newPlayer.PlayerClass != null && hasCreated == true && newPlayer.PlayerName != null && newPlayer.PlayerName != "Enter Name" && CharacterIndex <= CharacterIndexMax) {
			CharacterIndex = this.CharacterIndex;
			newPlayer.PlayerLevel = 1;
			newPlayer.Stamina = this.newPlayer.PlayerClass.Stamina;
			newPlayer.Agility = this.newPlayer.PlayerClass.Agility;
			newPlayer.Intellect = this.newPlayer.PlayerClass.Intellect;
			newPlayer.Strength = this.newPlayer.PlayerClass.Strength;
			newPlayer.PlayerName = this.newPlayer.PlayerName;
			 
			//store info
			//gameController.Save();

			// spawn player into the world at start position
		}
	}
}

/*
[System.Serializable]
public class GUI_Editor{
	[Header("GUI options")]
	public bool Createtoggle;
	public bool Createbutton;
	int[] buttons;
	int[] toggle;
	[Space]
	[Header("Size of GUI")]
	public int x = 10;
	public int y = 0;

	public int width = 100;
	public int height = 50;
	public string Text = string.Empty;
	[Space]
	[Header("Texture")]
	public bool aTexture;
	public Texture texture;

	void createToggle (bool Createtoggle) {
		for (int i = 0; i < buttons.Length; i++) {
			int x = 10;
			int y = 0;

			int width = 100;
			int height = 50;
			string Text = string.Empty;
		}

		x = x;
		y = y;
		width = width;
		height = height;
		Text = Text;
	}

	void createButton (bool Createbutton) {
		for (int i = 0; i < buttons.Length; i++) {
			int x = 10;
			int y = 0;

			int width = 100;
			int height = 50;
			string Text = string.Empty;

			if (aTexture) {
				this.texture = texture;
			}
		}

		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.Text = Text;
	}
}*/