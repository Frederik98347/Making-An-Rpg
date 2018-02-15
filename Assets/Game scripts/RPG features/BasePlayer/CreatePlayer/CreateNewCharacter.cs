using UnityEngine;
using System.Text.RegularExpressions;

public class CreateNewCharacter : MonoBehaviour {
	public BasePlayer newPlayer;
	public GameController gameController;

	public bool isMageClass;
	public bool isWarriorClass;
	public bool isRogueClass;
	public bool hasCreated;
	public bool nameCreated;
	bool savedNcreated;
	public int CharacterIndex = 0;
	int CharacterIndexMax = 10;
	bool canCreate;
	public string charName;
	public string className;
	bool inputEdit;

	// Use this for initialization
	void Start () {
		newPlayer = new BasePlayer();
		nameCreated = false;
		inputEdit = true;

		if (CharacterIndex < CharacterIndexMax) {
			canCreate = true;
		} else {
			CharacterIndex = 10;
			canCreate = false;
		}

		if (CharacterIndex == 0 || hasCreated == true) {
			savedNcreated = false;
		}
	}

	void Update() {
		inputVal ();
	}

	void OnGUI() {
		
		while (savedNcreated == false) {
		
			if (GUI.Toggle (new Rect (10, 50, 100, 25), isMageClass, "Mage Class")) {
				isMageClass = true;
				isWarriorClass = false;
				isRogueClass = false;
			}

			if (GUI.Toggle (new Rect (10, 25, 100, 25), isWarriorClass, "Warrior Class")) {
				isMageClass = false;
				isWarriorClass = true;
				isRogueClass = false;
			}

			if (GUI.Toggle (new Rect (10, 75, 100, 25), isRogueClass, "Rogue class")) {
				isMageClass = false;
				isWarriorClass = false;
				isRogueClass = true;
			}

			if (GUI.Button (new Rect (10, 125, 50, 25), ("Create"))) {
				if (canCreate == true && CharacterIndex < CharacterIndexMax && nameCreated == true) {
				
					if (isMageClass && nameCreated == true) {
						newPlayer.PlayerClass = new BaseMageClass ();
                  		CharacterIndex = CharacterIndex + 1;

					} else if (isWarriorClass && nameCreated == true) {
						newPlayer.PlayerClass = new BaseWarriorClass ();
						CharacterIndex = CharacterIndex + 1;

					} else if (isRogueClass && nameCreated == true) {
						newPlayer.PlayerClass = new BaseRogueClass ();
						CharacterIndex = CharacterIndex + 1;
					}

					hasCreated = true;
				} else if(CharacterIndex >= CharacterIndexMax) {
					CharacterIndex = 10;
					hasCreated = false;
					canCreate = false;
					nameCreated = false;
					Debug.LogError ("Error creating char: " + canCreate + " " + CharacterIndex);
				}
			}

			SetupData ();
			break;
		}
	}

	void SetupData() {
		while (savedNcreated != true) {
			if (inputEdit == true) {
				charName = GUI.TextField (new Rect (10, 100, 100, 20), charName, 12);
				charName = Regex.Replace (charName, @"[^a-zA-Z]", "");

				if (charName != string.Empty) {
					charName = GUI.TextField (new Rect (10, 100, 100, 20), charName, 12);
					charName = Regex.Replace (charName, @"[^a-zA-Z]", "");

					if (charName != "EnterName") {
						nameCreated = true;
					}
				}
			}

			//Set Different Class Stats
			if (hasCreated == true && canCreate == true && nameCreated == true) {
				// set level to 1 because it creates a new char

				newPlayer.PlayerLevel = 1;
                charName = newPlayer.PlayerName;
                newPlayer.Agility = newPlayer.PlayerClass.Agility;
                newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
				newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
				newPlayer.Strength = newPlayer.PlayerClass.Strength;

				newPlayer.PlayerClass.CharacterClassName = className;


				//store info
				//gameController.Save();
				savedNcreated = true;
				inputEdit = false;
				Debug.Log (newPlayer.PlayerName);
                Debug.Log(charName);
				Debug.Log (newPlayer.PlayerClass.CharacterClassName);
				Debug.Log (newPlayer.PlayerClass.CharacterClassDescription);
				Debug.Log (newPlayer.Agility);
				Debug.Log (newPlayer.Stamina);
				Debug.Log (newPlayer.Intellect);
				Debug.Log (newPlayer.Strength);

				// spawn player into the world at start position
			}
			break;
		}
		// goto Character selecting screen.
	}

	void inputVal() {

		if (charName == "EnterName" || charName == string.Empty) {
			nameCreated = false;
			inputEdit = true;
		}

		if (isMageClass == true || isWarriorClass == true || isRogueClass == true) {
			canCreate = true;
		} else {
			canCreate = false;
		}

		if (CharacterIndex >= CharacterIndexMax) {
			canCreate = false;
			Debug.Log ("Too many characters created, delete one before creating another");
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