using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecToggleGroup : MonoBehaviour {

    public Toggle toggleWarrior;
    public Toggle toggleMage;
    public Toggle toggleRogue;

    [SerializeField] TMP_Text textWarrior;
    [SerializeField] TMP_Text textMage;
    [SerializeField] TMP_Text textRogue;

    // Use this for initialization
    void Start () {
        if (textWarrior == null)
        {
            textWarrior = GameObject.Find("Wardescription").GetComponent<TMP_Text>();
        } else if (textMage == null)
        {
            textMage = GameObject.Find("Magedescription").GetComponent<TMP_Text>();
        } else if (textRogue == null)
        {
            textRogue = GameObject.Find("Rogdescription").GetComponent<TMP_Text>();
        }


        if (toggleWarrior.isOn == true)
        {
            // find description
            textWarrior.gameObject.SetActive(true);
            textMage.gameObject.SetActive(false);
            textRogue.gameObject.SetActive(false);
        } else if (toggleMage.isOn == true)
        {
            textWarrior.gameObject.SetActive(false);
            textMage.gameObject.SetActive(true);
            textRogue.gameObject.SetActive(false);
        } else if (toggleRogue.isOn == true)
        {
            textWarrior.gameObject.SetActive(false);
            textMage.gameObject.SetActive(false);
            textRogue.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (toggleWarrior.isOn == true)
        {
            // find description
            textWarrior.gameObject.SetActive(true);
            textMage.gameObject.SetActive(false);
            textRogue.gameObject.SetActive(false);
        }
        else if (toggleMage.isOn == true)
        {
            textWarrior.gameObject.SetActive(false);
            textMage.gameObject.SetActive(true);
            textRogue.gameObject.SetActive(false);
        }
        else if (toggleRogue.isOn == true)
        {
            textWarrior.gameObject.SetActive(false);
            textMage.gameObject.SetActive(false);
            textRogue.gameObject.SetActive(true);
        }
    }
}