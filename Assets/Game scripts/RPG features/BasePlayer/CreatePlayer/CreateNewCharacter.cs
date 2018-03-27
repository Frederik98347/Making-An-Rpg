using UnityEngine;
using System.Text.RegularExpressions;

public class CreateNewCharacter : MonoBehaviour {
	public BasePlayer newPlayer;

	public bool isMageClass;
	public bool isWarriorClass;
	public bool isRogueClass;
	public bool hasCreated;
	public bool nameCreated;
	bool savedNcreated;
    [Range(0, 10)]
    public int CharacterIndex = 0;
    int CharacterIndexMax = 10;
	bool canCreate;
	public string charName;
	public string className;
	bool inputEdit;

    //Stats


	// Use this for initialization
	void Start () {
		newPlayer = new BasePlayer();
		nameCreated = false;
		inputEdit = true;
        charName = "Enter Name";

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
		InputVal ();
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
            CheckClass(isMageClass, isWarriorClass, isRogueClass);
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
                //newPlayer.Stats();

                //newPlayer.Playerlevel = 1;
                charName = newPlayer.PlayerName;
                //newPlayer.stats[0]
                //newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
				//newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
				//newPlayer.Strength = newPlayer.PlayerClass.Strength;
                //newPlayer.Critchance = newPlayer.PlayerClass.Critchance;
                //newPlayer.Defense = newPlayer.PlayerClass.Defense;


                //store info
                savedNcreated = true;
				inputEdit = false;

            }
			break;
		}

        TestCreating(savedNcreated);

        if (CharacterIndex == 1)
        {
            // Spawn into world now
        } else if(CharacterIndex > 1)
        {
            // goto Character selecting screen.
        }
    }

	void InputVal() {

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

    void TestCreating(bool isCreated)
    {
        isCreated = savedNcreated;
        if (isCreated == true)
        {
           /* Debug.Log("Player Name" + newPlayer.PlayerName);
            Debug.Log("Character Name: " + charName);
            Debug.Log("Class: " + newPlayer.PlayerClass.CharacterClassName);
            Debug.Log("ClassDescription: " + newPlayer.PlayerClass.CharacterClassDescription);
            Debug.Log("Stats: ");
            Debug.Log("Agility: " + newPlayer.Agility);
            Debug.Log("Stamina: " + newPlayer.Stamina);
            Debug.Log("Intellect: " + newPlayer.Intellect);
            Debug.Log("Strength: " + newPlayer.Strength);
            Debug.Log("Defense: " + newPlayer.Defense);
            Debug.Log("Critchance: " + newPlayer.Critchance);*/
        }
        
    }

    void CheckClass (bool isMage, bool isWarrior, bool isRogue)
    {
        isMage = isMageClass;
        isWarrior = isWarriorClass;
        isRogue = isRogueClass;

        if (isMage == true)
        {
            newPlayer.PlayerClass = new BaseMageClass();
        } else if (isWarrior == true)
        {
            newPlayer.PlayerClass = new BaseWarriorClass();
        } else if (isRogue == true)
        {
            newPlayer.PlayerClass = new BaseRogueClass();
        }
    }
}