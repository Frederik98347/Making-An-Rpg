using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ClassSelector script allows you to create a class, updating stats & applying all bonuses and drawbacks to the player before instantiation.
/// </summary>
public class ClassSelector : MonoBehaviour {
    [HideInInspector]
    public SpecToggleGroup toggleGroup;
    [HideInInspector]
    public Birthday birthDay;
    [HideInInspector]
    public FamiliyTraits familyTraits;
    FamilyStatus Status;

    public bool isRogue;
    public bool isMage;
    public bool isWarrior;

    [Header("Menus")]
    public GameObject ThisMenu;
    public GameObject NextMenu;

    [SerializeField] TextMeshProUGUI Text;

    public GameObject FamilyNameError;
    public GameObject FirstNameError;

    [Header("Slider properties")]
    public Slider StrengthSlider;
    public Slider StaminaSlider;
    public Slider AgilitySlider;
    public Slider IntelligenceSlider;

    [SerializeField] int StatMaxValue;
    [SerializeField] int CurStatValue;
    [Range(0, 5)]
    [SerializeField]
    int strengthSliderValue;
    [Range(0, 5)]
    [SerializeField]
    int staminaSliderValue;
    [Range(0, 5)]
    [SerializeField]
    int agilitySliderValue;
    [Range(0, 5)]
    [SerializeField]
    int intelligenceSliderValue;

    [Header("Buttons")]
    [SerializeField] Button StaminaButtonPlus;
    [SerializeField] Button StaminaButtonMinus;

    [SerializeField] Button AgilityButtonPlus;
    [SerializeField] Button AgilityButtonMinus;

    [SerializeField] Button StrengthButtonPlus;
    [SerializeField] Button StrengthButtonMinus;

    [SerializeField] Button IntelligenceButtonPlus;
    [SerializeField] Button IntelligenceButtonMinus;

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

   [Header("Speed attributes")]
    public float MovementSpeed;
    public float AttackSpeed;
    public float CastingSpeed;

    [Header("Player info")]
    public string PlayerName;
    public string FamilyName;
    public int PlayerLevel;

    [Header("Birthday")]
    public int Age;
    public int Day;
    public int Month;
    public int Year;

    [Header("FamilyStatus")]
    public Item[] StartItems;
    public int Amount;
    public bool StackAble;

    public string MoneyName = "Gold Coins";
    public float Money = 0;
    public float IncomePrDay = 0;

    [Header("Next Button")]
    public Button NextButton;

    public void StatsBySlider()
    {
        if (StrengthSlider != null && StaminaSlider != null && AgilitySlider != null && IntelligenceSlider != null)
        {
            if (CurStatValue <= StatMaxValue)
            {
                StrengthSlider.maxValue = StatMaxValue/2;
                AgilitySlider.maxValue = StatMaxValue/2;
                IntelligenceSlider.maxValue = StatMaxValue/2;
                StaminaSlider.maxValue = StatMaxValue/2;

                strengthSliderValue = (int)StrengthSlider.value;
                staminaSliderValue = (int)StaminaSlider.value;
                agilitySliderValue = (int)AgilitySlider.value;
                intelligenceSliderValue = (int)IntelligenceSlider.value;

                CurStatValue = ((((int)StrengthSlider.value) + ((int)StaminaSlider.value) + ((int)IntelligenceSlider.value) + ((int)AgilitySlider.value)));
                SetText("Stat points left: " + TextCalc());
            }
        }
    }

    public void AddorDecreaseValue(Button btn)
    {
        //when you click the rigth button add value
        // When you click the left Button DecreaseValue

        //Find out which Button is pressed
        if (btn.name == StaminaButtonMinus.name && CurStatValue != 0)
        {
            //decrease value
            StaminaSlider.value -= 1; 
        } else if (btn.name == StaminaButtonPlus.name && CurStatValue != StatMaxValue)
        {
            //AddValue value
            StaminaSlider.value += 1;
        }

        if (btn.name == AgilityButtonMinus.name && CurStatValue != 0)
        {
            //decrease value
            AgilitySlider.value -= 1;
        }
        else if (btn.name == AgilityButtonPlus.name && CurStatValue != StatMaxValue)
        {
            //AddValue value
            AgilitySlider.value += 1;
        }
        if (btn.name == IntelligenceButtonMinus.name && CurStatValue != 0)
        {
            //decrease value
            IntelligenceSlider.value -= 1;
        }
        else if (btn.name == IntelligenceButtonPlus.name && CurStatValue != StatMaxValue)
        {
            //AddValue value
            IntelligenceSlider.value += 1;
        }
        if (btn.name == StrengthButtonMinus.name && CurStatValue != 0)
        {
            //decrease value
            StrengthSlider.value -= 1;
        }
        else if (btn.name == StrengthButtonPlus.name && CurStatValue != StatMaxValue)
        {
            //AddValue value
            StrengthSlider.value += 1;
        }
    }

    private void OnEnable()
    {
        if (birthDay == null)
        {
            birthDay = FindObjectOfType<Birthday>();
        }

        if (Status == null)
        {
            Status = FindObjectOfType<FamilyStatus>();

            StartItems = new Item[]{
                Status.StartItem_Boots, Status.StartItem_Chest,
                Status.StartItem_Pants, Status.StartItem_Weapon};

            MoneyName = Status.MoneyName;
            Money = Status.Money;
            IncomePrDay = Status.Income;
            
        }

        if (familyTraits == null)
        {
            familyTraits = FindObjectOfType<FamiliyTraits>();

            //making sure these varibles inside this exact script is equal to familytraits set variables
            StrengthButtonMinus = familyTraits.strengthButtonMinus;
            StrengthButtonPlus = familyTraits.strengthButtonPlus;

            StaminaButtonMinus = familyTraits.staminaButtonMinus;
            StaminaButtonPlus = familyTraits.staminaButtonPlus;

            AgilityButtonMinus = familyTraits.agilityButtonMinus;
            AgilityButtonPlus = familyTraits.agilityButtonPlus;

            IntelligenceButtonMinus = familyTraits.intelligenceButtonMinus;
            IntelligenceButtonPlus = familyTraits.intelligenceButtonPlus;
        }


    }

    // Use this for initialization
    void Start () {
        StrengthSlider.maxValue = 5;
        AgilitySlider.maxValue = 5;
        IntelligenceSlider.maxValue = 5;
        StaminaSlider.maxValue = 5;

        if (NextButton == null)
        {
            NextButton = GameObject.Find("NextButton").GetComponent<Button>();
        }

        if (birthDay != null)
        {
            Age = birthDay.Age;
            Day = birthDay.Day;
            Month = birthDay.Month;
            Year = birthDay.year;
        }

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

        StatsBySlider();

        if (FirstNameError == null)
        {
            FirstNameError = GameObject.Find("FirstNameErrorBox");
            FirstNameError.SetActive(false);
        }

        if (FamilyNameError == null)
        {
            FamilyNameError = GameObject.Find("FamilyNameErrorBox");
            FamilyNameError.SetActive(false);

        }
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

                    //birthday
                    Age = birthDay.Age;
                    Day = birthDay.Day;
                    Month = birthDay.Month;
                    Year = birthDay.year;

                    //classbased speed
                    AttackSpeed = SelectedClass.Attackspeed;
                    MovementSpeed = SelectedClass.RunningSpeed;
                    CastingSpeed = SelectedClass.CastingSpeed;

                    //setting up so the name typed is = to playerName
                    SelectedClass.PlayerName = GameObject.Find("Name").GetComponentInChildren<TMP_InputField>().text;
                    SelectedClass.FamilyName = GameObject.Find("Family Name").GetComponentInChildren<TMP_InputField>().text;

                    //base Player info
                    PlayerLevel = SelectedClass.Level;
                    FamilyName = SelectedClass.FamilyName;
                    PlayerName = SelectedClass.PlayerName;

                    CheckName(PlayerName, FamilyName);
                    Status.DropDowns();
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

                    //birthday
                    Age = birthDay.Age;
                    Day = birthDay.Day;
                    Month = birthDay.Month;
                    Year = birthDay.year;

                    //classbased speed
                    AttackSpeed = SelectedClass.Attackspeed;
                    MovementSpeed = SelectedClass.RunningSpeed;
                    CastingSpeed = SelectedClass.CastingSpeed;

                    //setting up so the name typed is = to playerName
                    SelectedClass.PlayerName = GameObject.Find("Name").GetComponentInChildren<TMP_InputField>().text;
                    SelectedClass.FamilyName = GameObject.Find("Family Name").GetComponentInChildren<TMP_InputField>().text;

                    //base Player info
                    PlayerLevel = SelectedClass.Level;
                    FamilyName = SelectedClass.FamilyName;
                    PlayerName = SelectedClass.PlayerName;

                    CheckName(PlayerName, FamilyName);
                    Status.DropDowns();
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

                    //birthday
                    Age = birthDay.Age;
                    Day = birthDay.Day;
                    Month = birthDay.Month;
                    Year = birthDay.year;

                    //classbased speed
                    AttackSpeed = SelectedClass.Attackspeed;
                    MovementSpeed = SelectedClass.RunningSpeed;
                    CastingSpeed = SelectedClass.CastingSpeed;

                    //setting up so the name typed is = to playerName
                    SelectedClass.PlayerName = GameObject.Find("Name").GetComponentInChildren<TMP_InputField>().text;
                    SelectedClass.FamilyName = GameObject.Find("Family Name").GetComponentInChildren<TMP_InputField>().text;

                    //base Player info
                    PlayerLevel = SelectedClass.Level;
                    FamilyName = SelectedClass.FamilyName;
                    PlayerName = SelectedClass.PlayerName;

                    CheckName(PlayerName, FamilyName);
                    Status.DropDowns();
                }
            }
        }
    }

    void SetText(string text)
    {
        Text.text = text;
    }

    public int TextCalc()
    {
        CurStatValue = ((((int)StrengthSlider.value) + ((int)StaminaSlider.value) + ((int)IntelligenceSlider.value) + ((int)AgilitySlider.value)));

        return StatMaxValue - CurStatValue;
    }

    void CheckName(string name, string familyname)
    {
        if (GameObject.Find("Name").GetComponentInChildren<TMP_InputField>().text == string.Empty)
        {
            Debug.Log("true");
            if(FirstNameError != null)
            {
                FirstNameError.SetActive(true);

                string firstNameError = "Name missing: Please fill out name fields";
                FirstNameError.GetComponentInChildren<TextMeshProUGUI>().text = firstNameError;
            }
        } else
        {
            FirstNameError.SetActive(false);
        }

        if (GameObject.Find("Family Name").GetComponentInChildren<TMP_InputField>().text == string.Empty)
        {
            if (FamilyNameError != null)
            {
                FamilyNameError.SetActive(true);

                string firstNameError = "Family Name missing: Please fill out name fields";
                FamilyNameError.GetComponentInChildren<TextMeshProUGUI>().text = firstNameError;
            }
        }
        else
        {
            FamilyNameError.SetActive(false);
        }

        if (GameObject.Find("Family Name").GetComponentInChildren<TMP_InputField>().text != string.Empty && GameObject.Find("Name").GetComponentInChildren<TMP_InputField>().text != string.Empty)
        {
            NextPage(NextButton);
        }
    }

    void NextPage(Button btn)
    {
        ThisMenu.SetActive(false);

        //I dont have a next menu atm, so to avoid error
        //NextMenu.SetActive(true);
    }
}