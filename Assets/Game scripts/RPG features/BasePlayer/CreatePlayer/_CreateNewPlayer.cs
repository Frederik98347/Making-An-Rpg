using UnityEngine;

public class _CreateNewPlayer : MonoBehaviour {

    private BasePlayer newPlayer;
    bool isMageClass;
    bool isWarriorClass;
    bool isRogueClass;

	// Use this for initialization
	void Start () {
        newPlayer = new BasePlayer();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        isMageClass = GUILayout.Toggle(isMageClass, "Mage Class");
        isWarriorClass = GUILayout.Toggle(isWarriorClass, "Warrior Class");
        isRogueClass = GUILayout.Toggle(isRogueClass, "Rogue Class");

        if(GUILayout.Button("Create"))
        {
            if(isMageClass)
            {
                newPlayer.PlayerClass = new BaseMageClass();
            }
            if (isWarriorClass)
            {
                newPlayer.PlayerClass = new BaseWarriorClass();
            }
            if (isRogueClass)
            {
                newPlayer.PlayerClass = new BaseRogueClass();
            }

            newPlayer.Playerlevel = 1;
            //newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
            //newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
            //newPlayer.Strength = newPlayer.PlayerClass.Strength;
            //newPlayer.Agility = newPlayer.PlayerClass.Agility;
            //newPlayer.Critchance = newPlayer.PlayerClass.Critchance;
            //newPlayer.Defense = newPlayer.PlayerClass.Defense;

            Debug.Log("Player Class: " + newPlayer.PlayerClass.CharacterClassName);
            Debug.Log("Player Description: " + newPlayer.PlayerClass.CharacterClassDescription);
            Debug.Log("Player Level: " + newPlayer.Playerlevel);
            //Debug.Log("Player stamina: " + newPlayer.Stamina);
            //Debug.Log("Player agility: " + newPlayer.Agility);
            //Debug.Log("Player intellect: " + newPlayer.Intellect);
            //Debug.Log("Player Defense: " + newPlayer.Defense);
            //Debug.Log("Player CritChance: " + newPlayer.Critchance);

        }
    }
}