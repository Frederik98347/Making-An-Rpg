using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Birthday : MonoBehaviour
{
    BirthdayBonuses birthdayBonuses;

    [Header("Age Area")]
    [Range(16, 30)]
    [SerializeField]
    public int Age;
    public Slider AgeSlider;
    public TMP_Text AgeText;
    public TMP_Dropdown AgeDropDown;
    public int AgeDropDownValue;

    [Header("Day Area")]
    [Range(1, 31)]
    [SerializeField]
    public int Day;
    public Slider DaySlider;
    public TMP_Text DayText;
    public TMP_Dropdown DayDropDown;
    public int DayDropDownValue;

    [Header("Month Area")]
    [Range(1, 12)]
    [SerializeField]
    public int Month;
    public Slider MonthSlider;
    public TMP_Text MonthText;
    public TMP_Dropdown MonthDropDown;
    public int MonthDropDownValue;

    [Header("BirthDay Area")]
    [SerializeField]
    public int year;
    int baseYear = 800;
    string BirthDateText;
    public TMP_Text _BirthDayText;

    // Use this for initialization
    void Start()
    {
        AgeSlider = GameObject.Find("Age").GetComponentInChildren<Slider>();
        DaySlider = GameObject.Find("BirthDay").GetComponentInChildren<Slider>();
        MonthSlider = GameObject.Find("BirthMonth").GetComponentInChildren<Slider>();

        Age = (int)AgeSlider.value;
        Day = (int)DaySlider.value;
        Month = (int)MonthSlider.value;

        AgeText = AgeSlider.GetComponentInChildren<TMP_Dropdown>().GetComponentInChildren<TextMeshProUGUI>();
        AgeDropDown = AgeSlider.GetComponentInChildren<TMP_Dropdown>();
        AgeDropDownValue = AgeDropDown.value;

        DayText = DaySlider.GetComponentInChildren<TMP_Dropdown>().GetComponentInChildren<TextMeshProUGUI>();
        DayDropDown = DaySlider.GetComponentInChildren<TMP_Dropdown>();
        DayDropDownValue = DayDropDown.value;

        MonthText = MonthSlider.GetComponentInChildren<TMP_Dropdown>().GetComponentInChildren<TextMeshProUGUI>();
        MonthDropDown = MonthSlider.GetComponentInChildren<TMP_Dropdown>();
        MonthDropDownValue = MonthDropDown.value;

        SetBirthDate();
        _BirthDayText = GameObject.Find("YearofBirthText").GetComponent<TextMeshProUGUI>();

        if (_BirthDayText != null)
        {
            year = (Age + baseYear);
            BirthDateText = Day + "/" + Month + "/" + year;
            _BirthDayText.text = BirthDateText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_BirthDayText != null)
        {
            year = (Age + 800);
            BirthDateText = Day + "/" + Month + "/" + year;
            _BirthDayText.text = BirthDateText;
        }

        Age = (int)AgeSlider.value;
        AgeDropDown.value = Age - 16;
        AgeDropDownValue = AgeDropDown.value;

        Day = (int)DaySlider.value;
        DayDropDown.value = Day - 1;
        DayDropDownValue = DayDropDown.value;

        Month = (int)MonthSlider.value;
        MonthDropDown.value = Month - 1;
        MonthDropDownValue = MonthDropDown.value;

        //LimitDaysByMonth();
    }

    public void SetAge()
    {
        if (AgeSlider)
        {
            for (int i = 0; i <= AgeSlider.maxValue; i++)
            {
                AgeText.text = Age + "";
                AgeDropDown.value = Age - 16;
            }
        }
    }

    public void SetDay()
    {
        if (DaySlider)
        {
            for (int i = 0; i <= DaySlider.maxValue; i++)
            {
                DayText.text = Day + "";
                DayDropDown.value = Day - 1;
            }
        }
    }

    void LimitDaysByMonth()
    {
        if (MonthDropDown.value == 0)
        {
            if (DaySlider.value > birthdayBonuses.january_length)
            {
                DaySlider.value = birthdayBonuses.january_length;
                DaySlider.maxValue = birthdayBonuses.january_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.january_length;
            }
        }
        else if (MonthDropDown.value == 1)
        {
            if (DaySlider.value > birthdayBonuses.februar_length)
            {
                DaySlider.value = birthdayBonuses.februar_length;
                DaySlider.maxValue = birthdayBonuses.februar_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.februar_length;
            }
        }
        else if (MonthDropDown.value == 2)
        {
            if (DaySlider.value > birthdayBonuses.marts_length)
            {
                DaySlider.value = birthdayBonuses.marts_length;
                DaySlider.maxValue = birthdayBonuses.marts_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.marts_length;
            }
        }
        else if (MonthDropDown.value == 3)
        {
            if (DaySlider.value > birthdayBonuses.april_length)
            {
                DaySlider.value = birthdayBonuses.april_length;
                DaySlider.maxValue = birthdayBonuses.april_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.april_length;
            }
        }
        else if (MonthDropDown.value == 4)
        {
            if (DaySlider.value > birthdayBonuses.maj_length)
            {
                DaySlider.value = birthdayBonuses.maj_length;
                DaySlider.maxValue = birthdayBonuses.maj_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.maj_length;
            }
        }
        else if (MonthDropDown.value == 5)
        {
            if (DaySlider.value > birthdayBonuses.juni_length)
            {
                DaySlider.value = birthdayBonuses.juni_length;
                DaySlider.maxValue = birthdayBonuses.juni_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.juni_length;
            }
        }
        else if (MonthDropDown.value == 6)
        {
            if (DaySlider.value > birthdayBonuses.juli_length)
            {
                DaySlider.value = birthdayBonuses.juli_length;
                DaySlider.maxValue = birthdayBonuses.juli_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.juli_length;
            }
        }
        else if (MonthDropDown.value == 7)
        {
            if (DaySlider.value > birthdayBonuses.august_length)
            {
                DaySlider.value = birthdayBonuses.august_length;
                DaySlider.maxValue = birthdayBonuses.august_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.august_length;
            }
        }
        else if (MonthDropDown.value == 8)
        {
            if (DaySlider.value > birthdayBonuses.september_length)
            {
                DaySlider.value = birthdayBonuses.september_length;
                DaySlider.maxValue = birthdayBonuses.september_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.september_length;
            }
        }
        else if (MonthDropDown.value == 9)
        {
            if (DaySlider.value > birthdayBonuses.october_length)
            {
                DaySlider.value = birthdayBonuses.october_length;
                DaySlider.maxValue = birthdayBonuses.october_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.october_length;
            }
        }
        else if (MonthDropDown.value == 10)
        {
            if (DaySlider.value > birthdayBonuses.november_length)
            {
                DaySlider.value = birthdayBonuses.november_length;
                DaySlider.maxValue = birthdayBonuses.november_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.november_length;
            }
        }
        else if (MonthDropDown.value == 11)
        {
            if (DaySlider.value > birthdayBonuses.december_length)
            {
                DaySlider.value = birthdayBonuses.december_length;
                DaySlider.maxValue = birthdayBonuses.december_length;
            }
            else
            {
                DaySlider.maxValue = birthdayBonuses.december_length;
            }
        }
    }

    public void SetMonth()
    {
        if (MonthSlider)
        {
            for (int i = 0; i <= MonthSlider.maxValue; i++)
            {
                MonthDropDown.value = Month - 1;

                if (MonthDropDown.value == 0)
                {
                    MonthText.text = "January";
                }
                else if (MonthDropDown.value == 1)
                {
                    MonthText.text = "February";
                }
                else if (MonthDropDown.value == 2)
                {
                    MonthText.text = "March";

                }
                else if (MonthDropDown.value == 3)
                {
                    MonthText.text = "April";

                }
                else if (MonthDropDown.value == 4)
                {
                    MonthText.text = "May";

                }
                else if (MonthDropDown.value == 5)
                {
                    MonthText.text = "June";

                }
                else if (MonthDropDown.value == 6)
                {
                    MonthText.text = "July";

                }
                else if (MonthDropDown.value == 7)
                {
                    MonthText.text = "August";

                }
                else if (MonthDropDown.value == 8)
                {
                    MonthText.text = "September";

                }
                else if (MonthDropDown.value == 9)
                {
                    MonthText.text = "October";

                }
                else if (MonthDropDown.value == 10)
                {
                    MonthText.text = "November";

                }
                else if (MonthDropDown.value == 11)
                {
                    MonthText.text = "December";
                }
            }
        }
    }

    public void SetBirthDate()
    {
        SetAge();
        SetDay();
        SetMonth();
    }

}

public class BirthdayBonuses
{
    public Birthday _day;

    //name of each trait 
    // Born in the Month of Hope, allowing you to grow stronger faster f.x. ;)
    string january_traitname = "Month Of Janus";
    string februar_traitname = "Month Of Februa";
    string marts_traitname = "Month Of Mars";
    string april_traitname = "Month Of Aphrodite";
    string mai_traitname = "Month Of Maia";
    string juni_traitname = "Month Of Juno";
    string juli_traitname = "Month Of Julius";
    string august_traitname = "Month Of Augustus";
    string september_traitname = "Month Of Vulcan";
    string november_traitname = "Month Of Rebirth";
    string december_traitname = "Month Of Hope";

    // length of eact month
    public int january_length = 29;
    public int februar_length = 28;
    public int marts_length = 31;
    public int april_length = 30;
    public int maj_length = 31;
    public int juni_length = 30;
    public int juli_length = 31;
    public int august_length = 30;
    public int september_length = 31;
    public int october_length = 31;
    public int november_length = 30;
    public int december_length = 31;

    //stat bonus, effect bonus


    public string January_traitname
    {
        get
        {
            return january_traitname;
        }

        set
        {
            january_traitname = value;
        }
    }

    public string Februar_traitname
    {
        get
        {
            return februar_traitname;
        }

        set
        {
            februar_traitname = value;
        }
    }

    public string Marts_traitname
    {
        get
        {
            return marts_traitname;
        }

        set
        {
            marts_traitname = value;
        }
    }

    public string April_traitname
    {
        get
        {
            return april_traitname;
        }

        set
        {
            april_traitname = value;
        }
    }

    public string Mai_traitname
    {
        get
        {
            return mai_traitname;
        }

        set
        {
            mai_traitname = value;
        }
    }

    public string Juni_traitname
    {
        get
        {
            return juni_traitname;
        }

        set
        {
            juni_traitname = value;
        }
    }

    public string Juli_traitname
    {
        get
        {
            return juli_traitname;
        }

        set
        {
            juli_traitname = value;
        }
    }

    public string August_traitname
    {
        get
        {
            return august_traitname;
        }

        set
        {
            august_traitname = value;
        }
    }

    public string September_traitname
    {
        get
        {
            return september_traitname;
        }

        set
        {
            september_traitname = value;
        }
    }

    public string November_traitname
    {
        get
        {
            return november_traitname;
        }

        set
        {
            november_traitname = value;
        }
    }

    public string December_traitname
    {
        get
        {
            return december_traitname;
        }

        set
        {
            december_traitname = value;
        }
    }

    Age _Age;
    Day _Day;
    Month _Month;

    public enum Age
    {
        TEEN = 0, // 16-19 // quick and agile, quick learning, less strength & hp
        GROWNUP, //20-23 // agile medium hp & def & resistance
        MATURED_GROWNUP, // 24-27 // less agile, good hp&def&resistance
        SEASONED_GROWNUP, //28-30 // good hp & def& resistance, but slowest
    }

    void AgeBonus()
    {
        if (_day.Age < 20)
        {
            _Age = Age.TEEN;
        }
        else if (_day.Age < 24)
        {
            _Age = Age.GROWNUP;
        }
        else if (_day.Age < 28)
        {
            _Age = Age.MATURED_GROWNUP;
        }
        else if (_day.Age >= 28)
        {
            _Age = Age.SEASONED_GROWNUP;
        }

        //implent bonuses here. .. . 
        if (_Age == Age.TEEN)
        {
            //Somekind of bonus
            
        }
        else if (_Age == Age.GROWNUP)
        {
            //Somekind of bonus
        }
        else if (_Age == Age.MATURED_GROWNUP)
        {
            //Somekind of bonus
        }
        else if (_Age == Age.SEASONED_GROWNUP)
        {
            //Somekind of bonus
        }
    }

    void DayBonus()
    {
        //code here
    }

    void MonthBonus()
    {
        //code here
    }

    public void BirthdayBonus()
    {
        AgeBonus();
        DayBonus();
        MonthBonus();
    }

    public enum Day
    {
        DAY_OF_THE_MOON = 0, //do something
        DAY_OF_TYR, // Dualwielding or better dualwielding
        DAY_OF_ODIN, // increase mana overall & memory by 1
        DAY_OF_THOR, // Increase strength & stamina
        DAY_OF_FREYA, //increase speed(movement & attack/castingspeed) & get healed everytime you kill someone
        SATURNS_DAY, // DO SOMETHING
        SUNS_DAY, // Do something 
    }

    public enum Month
    {
        JANUS_MONTH = 0, // 29days
        MONTH_OF_FEBRUA, // 28days
        MARS_MONTH, //31 days
        APHRODITES_MONTH, // 30days
        MAIAS_MONTH, // 31days
        JUNOS_MONTH, // 30days
        JULIUS, // 31days
        AUGUSTUS, //31DAYS
        SEPTEMBER, //30DAYS
        OCTOBER, // 31DAYS
        NOVEMBRIS, //30DAYS
        DECEMBRE // 31DAYS
    }
}