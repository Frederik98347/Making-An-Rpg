using UnityEngine;

public class ClassSelector : MonoBehaviour {
    public SpecToggleGroup toggleGroup;

    public bool isRogue;
    public bool isMage;
    public bool isWarrior;
    // Use this for initialization
    void Start () {
        if (toggleGroup == null)
        {
          toggleGroup = FindObjectOfType<SpecToggleGroup>();
        }

        ClassPick();
    }
	
	// Update is called once per frame
	void Update () {
    }

   public void ClassPick()
    {
        if (toggleGroup != null)
        {
            if (toggleGroup.toggleRogue.isOn == true)
            {
                //class = rogue
                //get rogue data here
                BaseRogueClass SelectedClass = new BaseRogueClass();
                if (SelectedClass != null)
                {
                    SelectedClass.RogueClass();
                    Debug.Log(SelectedClass.CharacterClassName);
                    Debug.Log("Agility: " + SelectedClass.BaseAgility);
                    Debug.Log("Strength: " + SelectedClass.BaseStrength);
                    Debug.Log("Intellect: " + SelectedClass.BaseIntellect);
                    Debug.Log("Stamina: " + SelectedClass.BaseStamina);
                    Debug.Log("Endurance: " + SelectedClass.BaseEndurance);
                    Debug.Log("CritChance: " + SelectedClass.BaseCritchance);
                    Debug.Log("Haste: " + SelectedClass.BaseHaste);
                    Debug.Log("Defence: " + SelectedClass.BaseDefence);
                    Debug.Log("Resistance: " + SelectedClass.BaseResistance);
                }
            }
            else if (toggleGroup.toggleMage.isOn == true)
            {
                //class = Mage
                BaseMageClass SelectedClass = new BaseMageClass();
                if (SelectedClass != null)
                {
                    SelectedClass.MageClass();
                    Debug.Log(SelectedClass.CharacterClassName);
                    Debug.Log("Agility: " + SelectedClass.BaseAgility);
                    Debug.Log("Strength: " + SelectedClass.BaseStrength);
                    Debug.Log("Intellect: " + SelectedClass.BaseIntellect);
                    Debug.Log("Stamina: " + SelectedClass.BaseStamina);
                    Debug.Log("Endurance: " + SelectedClass.BaseEndurance);
                    Debug.Log("CritChance: " + SelectedClass.BaseCritchance);
                    Debug.Log("Haste: " + SelectedClass.BaseHaste);
                    Debug.Log("Defence: " + SelectedClass.BaseDefence);
                    Debug.Log("Resistance: " + SelectedClass.BaseResistance);
                }
            }
            else if (toggleGroup.toggleWarrior.isOn == true)
            {
                //class = warrior
                BaseWarriorClass SelectedClass = new BaseWarriorClass();
                if (SelectedClass != null)
                {
                    SelectedClass.WarriorClass();
                    Debug.Log(SelectedClass.CharacterClassName);
                    Debug.Log("Agility: " + SelectedClass.BaseAgility);
                    Debug.Log("Strength: " + SelectedClass.BaseStrength);
                    Debug.Log("Intellect: " + SelectedClass.BaseIntellect);
                    Debug.Log("Stamina: " + SelectedClass.BaseStamina);
                    Debug.Log("Endurance: " + SelectedClass.BaseEndurance);
                    Debug.Log("CritChance: " + SelectedClass.BaseCritchance);
                    Debug.Log("Haste: " + SelectedClass.BaseHaste);
                    Debug.Log("Defence: " + SelectedClass.BaseDefence);
                    Debug.Log("Resistance: " + SelectedClass.BaseResistance);
                }
            }
        }
    }
}