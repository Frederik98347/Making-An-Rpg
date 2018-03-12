using UnityEngine;
using TMPro;

public class FamilyStatus : MonoBehaviour {

    public string WealthTooltip;
    public string SocialTooltip;
    float socialRankMath;
    float wealthRankMath;

    [SerializeField] TMP_Dropdown wealthDropDownPick;
    [SerializeField] TMP_Dropdown socialDropDownPick;

    // Use this for initialization
    void Start () {
        wealthDropDownPick = GameObject.Find("Wealth").GetComponentInChildren<TMP_Dropdown>();
        socialDropDownPick = GameObject.Find("Social").GetComponentInChildren<TMP_Dropdown>();

        WealthTooltip = "The more wealth, the more money you have at the start of the game according to your social rank. But in return exp gained is reduced acordingly";
        SocialTooltip = "The lower social status, the more attributes points on level up, but you lose talents and bonuses acordingly from startup";

        // check what the result of the dropdowns was
        if (wealthDropDownPick.value == 0)
        {
            // poor
        } else if (wealthDropDownPick.value == 1)
        {
            //average
            Debug.Log("Average joe");
        } else
        {
            //rich
        }

        if (socialDropDownPick.value == 0)
        {
            // peasant
            Debug.Log("Peasant");
        }
        else if (wealthDropDownPick.value == 1)
        {
            //farmer
        }
        else
        {
            //knight
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        //make TooltipAppear inside a box, at the right of the mouse position
    }
}
