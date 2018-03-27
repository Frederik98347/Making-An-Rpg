using UnityEngine.UI;
using UnityEngine;

public class FamiliyTraits : MonoBehaviour {
    //Stat tooltips
    [Header("Tooltip")]
    [SerializeField] string StaminaTooltip = "Increase HP";
    [SerializeField] string StrengthTooltip = "Increase x amount of y";
    [SerializeField] string AgilityTooltip = "Increase x amount of y";
    [SerializeField] string IntelligenceTooltip = "Increase x amount of y * z";

    [Header("Sliders")]
    public Slider staminaSlider;
    public Slider strenghtSlider;
    public Slider intelligenceSlider;
    public Slider agilitySlider;
    [Header("Buttons")]
    public Button staminaButtonPlus;
    public Button staminaButtonMinus;

    public Button agilityButtonPlus;
    public Button agilityButtonMinus;

    public Button strengthButtonPlus;
    public Button strengthButtonMinus;

    public Button intelligenceButtonPlus;
    public Button intelligenceButtonMinus;

    GameObject staminaText;
    GameObject strengthText;
    GameObject intelligenceText;
    GameObject agilityText;

    [Header("Text")]
    public TMPro.TMP_Text Text;

    // Use this for initialization
    void Start()
    {
        staminaSlider = GameObject.Find("Stamina").GetComponentInChildren<Slider>();
        intelligenceSlider = GameObject.Find("Intelligence").GetComponentInChildren<Slider>();
        agilitySlider = GameObject.Find("Agility").GetComponentInChildren<Slider>();
        strenghtSlider = GameObject.Find("Strength").GetComponentInChildren<Slider>();
        Text = GameObject.Find("StatpointsliderText").GetComponent<TMPro.TMP_Text>();

        staminaText = GameObject.Find("Stamina");
        strengthText = GameObject.Find("Strength");
        agilityText = GameObject.Find("Agility");
        intelligenceText = GameObject.Find("Intelligence");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        //make TooltipAppear inside a box, at the right of the mouse position
        
    }

    public void SetText(string text)
    {
        // Text for statpoints left frame frame
        Text.text = text;

    }
}