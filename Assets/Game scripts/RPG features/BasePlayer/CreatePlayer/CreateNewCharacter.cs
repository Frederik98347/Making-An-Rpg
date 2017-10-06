using UnityEngine;
using System.Collections;

public class CreateNewCharacter : MonoBehaviour {
	public BasePlayer newPlayer;

	Player player;
    public GameController gameController;

	bool isMageClass;
	bool isWarriorClass;
	bool isRogueClass;
	public string NameField;
	public bool hasCreated;

	public int CharacterIndex;
	int CharacterIndexMax = 10;

	// Use this for initialization
	void Start () {
		newPlayer = new BasePlayer();
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

		if (GUI.Button (new Rect(10,100, 50, 25),("Create"))) {
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

                    if (hasCreated == true) {
                        gameController.Save();
                    }
				}

			} else {
				hasCreated = false;
				Debug.Log ("You cant Create more Characters");
			}
		}
			
		NameField = GUI.TextField (new Rect(10,100, 50, 25), player.CharacterName, 12);
		if (player.CharacterName != "Enter Name" && player.CharacterName != null) {
			newPlayer.PlayerName = this.player.CharacterName;
		} else {
			player.CharacterName = "Enter Name";
			NameField = GUI.TextField (new Rect(10,100, 50, 25), player.CharacterName, 12);
		}

			//Set Different Class Stats
		if (newPlayer != null && newPlayer.PlayerClass != null) {
			newPlayer.PlayerLevel = player.Level;
			newPlayer.Stamina = this.newPlayer.PlayerClass.Stamina;
			newPlayer.Agility = this.newPlayer.PlayerClass.Agility;
			newPlayer.Intellect = this.newPlayer.PlayerClass.Intellect;
			newPlayer.Strength = this.newPlayer.PlayerClass.Strength;
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