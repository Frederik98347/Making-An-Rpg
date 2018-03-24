using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Birthday : MonoBehaviour {

    [Header("Age Area")]
    [Range(16,30)]
    [SerializeField] int Age;
    public Slider AgeSlider;
    public TMP_Text AgeText;
    public TMP_Dropdown AgeDropDown;
    public int AgeDropDownValue;

    [Header("Day Area")]
    [Range(1, 31)]
    [SerializeField] int Day;
    public Slider DaySlider;
    public TMP_Text DayText;
    public TMP_Dropdown DayDropDown;
    public int DayDropDownValue;

    [Header("Month Area")]
    [Range(1, 12)]
    [SerializeField] int Month;
    public Slider MonthSlider;
    public TMP_Text MonthText;
    public TMP_Dropdown MonthDropDown;
    public int MonthDropDownValue;

    [Header("BirthDay Area")]
    [SerializeField] int year;
    string BirthDateText;
    public TMP_Text _BirthDayText;

    // Use this for initialization
    void Start () {
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
            year = (Age + 800);
            BirthDateText = Day + "/" + Month + "/" + year;
            _BirthDayText.text = BirthDateText;
        }
    }
	
	// Update is called once per frame
	void Update () {
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
    }

    public void SetAge()
    {
        if (AgeSlider)
        {
            for (int i = 0; i <= AgeSlider.maxValue; i++)
            {
                AgeText.text = Age + "";
                AgeDropDown.value = Age-16;
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
                } else if (MonthDropDown.value == 1){
                    MonthText.text = "February";
                } else if (MonthDropDown.value == 2)
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