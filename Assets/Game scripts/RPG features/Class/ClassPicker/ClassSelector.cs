using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassSelector : MonoBehaviour {
    public SpecToggleGroup toggleGroup;

    public bool isRogue;
    public bool isMage;
    public bool isWarrior;

    [SerializeField] TextMeshProUGUI Text;

    [Header("Slider properties")]
    public Slider StrengthSlider;
    public Slider StaminaSlider;
    public Slider AgilitySlider;
    public Slider IntelligenceSlider;

    [SerializeField] int StatMaxValue;
    [SerializeField] int CurStatValue;
    [Range(0,5)]
    [SerializeField] int strengthSliderValue;
    [Range(0, 5)]
    [SerializeField] int staminaSliderValue;
    [Range(0, 5)]
    [SerializeField] int agilitySliderValue;
    [Range(0, 5)]
    [SerializeField] int intelligenceSliderValue;

    [Header("ClassInfo")]
    public string ClassName;
    public string ClassDescription;
    public string ClassBonus;

    [Header("Attributes")]
    public int Agility;
    public int Stamina;
    public int Strength;
    public int Intelligence;
    public int Critchance;
    public int Defence;
    public int Endurance;
    public int Haste;
    public int Memory;
    public int Resistance;
 
    public void StatsBySlider()
    {
        if (StrengthSlider != null && StaminaSlider != null && AgilitySlider != null && IntelligenceSlider != null)
        {
            if (CurStatValue < StatMaxValue)
            {
                strengthSliderValue = (int)StrengthSlider.value;
                staminaSliderValue = (int)StaminaSlider.value;
                agilitySliderValue = (int)AgilitySlider.value;
                intelligenceSliderValue = (int)IntelligenceSlider.value;

                CurStatValue = ((((int)StrengthSlider.value) + ((int)StaminaSlider.value) + ((int)IntelligenceSlider.value) + ((int)AgilitySlider.value)));
                SetText("Stat points left: " + TextCalc());
            }
            else if (CurStatValue >= StatMaxValue)
            {
                //dont allow stat to progress over statMax

                if (CurStatValue == StatMaxValue)
                {
                    CurStatValue = StatMaxValue;
                    strengthSliderValue = (int)StrengthSlider.value;
                    staminaSliderValue = (int)StaminaSlider.value;
                    agilitySliderValue = (int)AgilitySlider.value;
                    intelligenceSliderValue = (int)IntelligenceSlider.value;


                    //limit slider here to StatMaxValue
                    SetText("Stat points left: " + TextCalc());
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        if (toggleGroup == null)
        {
          toggleGroup = FindObjectOfType<SpecToggleGroup>();
        }

        StatMaxValue = 10;
        CurStatValue = 0;

        if (Text == null)
        {
            Text = GameObject.Find("StatpointsliderText").GetComponent<TextMeshProUGUI>();
        }

        ClassPick();
        StatsBySlider();
    }

    public int[] ClassStats(int _Agility, int _Stamina, int _Strength, int _Intelligence, int _Crit, int _def, int _endurance, int _haste, int _memory, int _resistance)
    {
        int[] stats = new int[10];
        stats[0] = _Agility;
        stats[1] = _Stamina;
        stats[2] = _Strength;
        stats[3] = _Intelligence;
        stats[4] = _Crit;
        stats[5] = _def;
        stats[6] = _endurance;
        stats[7] = _haste;
        stats[8] = _memory;
        stats[9] = _resistance;
        
        Agility = stats[0];
        Stamina = stats[1];
        Strength = stats[2];
        Intelligence = stats[3];
        Critchance = stats[4];
        Defence = stats[5];
        Endurance = stats[6];
        Haste = stats[7];
        Memory = stats[8];
        Resistance = stats[9];

        return stats;

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
                isRogue = true;
                isWarrior = false;
                isMage = false;
                if (SelectedClass != null)
                {
                    SelectedClass.RogueClass();
                    SelectedClass.BaseAgility += agilitySliderValue;
                    SelectedClass.BaseStamina += staminaSliderValue;
                    SelectedClass.BaseStrength += strengthSliderValue;
                    SelectedClass.BaseIntellect += intelligenceSliderValue;

                    ClassName = SelectedClass.CharacterClassName;
                    ClassDescription = SelectedClass.CharacterClassDescription;
                    ClassBonus = SelectedClass.ClassBonus;

                    ClassStats(SelectedClass.BaseAgility, SelectedClass.BaseStamina, SelectedClass.BaseStrength, SelectedClass.BaseIntellect, SelectedClass.BaseCritchance,
                        SelectedClass.BaseDefence, SelectedClass.BaseEndurance, SelectedClass.BaseHaste, SelectedClass.BaseMemory, SelectedClass.BaseResistance);
                }
            }
            else if (toggleGroup.toggleMage.isOn == true)
            {
                //class = Mage
                isMage = true;
                isRogue = false;
                isWarrior = false;
                BaseMageClass SelectedClass = new BaseMageClass();
                if (SelectedClass != null)
                {
                    SelectedClass.MageClass();
                    SelectedClass.BaseAgility += agilitySliderValue;
                    SelectedClass.BaseStamina += staminaSliderValue;
                    SelectedClass.BaseStrength += strengthSliderValue;
                    SelectedClass.BaseIntellect += intelligenceSliderValue;

                    ClassName = SelectedClass.CharacterClassName;
                    ClassDescription = SelectedClass.CharacterClassDescription;
                    ClassBonus = SelectedClass.ClassBonus;

                    ClassStats(SelectedClass.BaseAgility, SelectedClass.BaseStamina, SelectedClass.BaseStrength, SelectedClass.BaseIntellect, SelectedClass.BaseCritchance,
                          SelectedClass.BaseDefence, SelectedClass.BaseEndurance, SelectedClass.BaseHaste, SelectedClass.BaseMemory, SelectedClass.BaseResistance);
                }
            }
            else if (toggleGroup.toggleWarrior.isOn == true)
            {
                //class = warrior
                isWarrior = true;
                isRogue = false;
                isMage = false;
                BaseWarriorClass SelectedClass = new BaseWarriorClass();
                if (SelectedClass != null)
                {
                    SelectedClass.WarriorClass();
                    SelectedClass.BaseAgility += agilitySliderValue;
                    SelectedClass.BaseStamina += staminaSliderValue;
                    SelectedClass.BaseStrength += strengthSliderValue;
                    SelectedClass.BaseIntellect += intelligenceSliderValue;

                    ClassName = SelectedClass.CharacterClassName;
                    ClassDescription = SelectedClass.CharacterClassDescription;
                    ClassBonus = SelectedClass.ClassBonus;

                    ClassStats(SelectedClass.BaseAgility, SelectedClass.BaseStamina, SelectedClass.BaseStrength, SelectedClass.BaseIntellect, SelectedClass.BaseCritchance,
                         SelectedClass.BaseDefence, SelectedClass.BaseEndurance, SelectedClass.BaseHaste, SelectedClass.BaseMemory, SelectedClass.BaseResistance);
                }
            }
        }
    }

    void SetText(string text)
    {
        Text.text = text;
    }

    int TextCalc()
    {
        return StatMaxValue - CurStatValue;
    }
}