using UnityEngine.UI;
using UnityEngine;

public class FamiliyTraits : MonoBehaviour {
    //Stat tooltips
    public string StaminaTooltip;
    public string StrengthTooltip;
    public string AgilityTooltip;
    public string IntelligenceTooltip;


    public Slider staminaSlider;
    public Slider strenghtSlider;
    public Slider intelligenceSlider;
    public Slider agilitySlider;

    public TMPro.TMP_Text Text;

    // Use this for initialization
    void Start()
    {
        staminaSlider = GameObject.Find("Stamina").GetComponentInChildren<Slider>();
        intelligenceSlider = GameObject.Find("Intelligence").GetComponentInChildren<Slider>();
        agilitySlider = GameObject.Find("Agility").GetComponentInChildren<Slider>();
        strenghtSlider = GameObject.Find("Strength").GetComponentInChildren<Slider>();
        Text = GameObject.Find("StatpointsliderText").GetComponent<TMPro.TMP_Text>();
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