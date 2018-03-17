using UnityEngine.UI;
using UnityEngine;

public class sliderValueCheck : MonoBehaviour {

    [Header("Slider")]
    [SerializeField] Slider Slider;
    [SerializeField] GameObject Loader;
    [SerializeField] string loadingObject = "Loading";

    [Header("Buttons")]
    public GameObject NextButton;
    public GameObject BackButton;

	// Use this for initialization
	void Start () {
        if (Loader == null)
        {
            Loader = GameObject.Find(loadingObject);
        }
        if (Slider == null)
        {
            Slider = Loader.GetComponentInChildren<Slider>();
        }

        NextButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        SetButtonsActive();
	}

    void SetButtonsActive ()
    {
        if (Slider.value >= 1)
        {
            NextButton.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(true);
        }
    }
}
