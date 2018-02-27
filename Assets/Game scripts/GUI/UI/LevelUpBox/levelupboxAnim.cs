using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelupboxAnim : MonoBehaviour {
    [Tooltip("How long it takes to fadeout the object")] public float fadeTime = 1f;
    [Range(0,8)]
    public float timeBeforeFade = 3.5f;
    public float fadeInTime = 3f;
    [Tooltip("The Canvas object you want to fadeout")] [SerializeField] CanvasGroup canvas;

   
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

   public void StartAnim()
   {
        gameObject.SetActive(true);
        Fade();
   }

    void Fade()
    {
        //float translation = fadeSpeed * Time.deltaTime;

        StartCoroutine(FadeIn());
    }

    private IEnumerator Fadeout()
    {

        while (canvas.alpha > 0)
        {
            if (timeBeforeFade > 0f)
            {
                timeBeforeFade -= Time.deltaTime;
            } else
            {
                canvas.alpha -= Time.deltaTime / fadeTime;
            }

            yield return null;
        }

        canvas.interactable = false;
        gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator FadeIn()
    {

        gameObject.SetActive(true);
        canvas.alpha = 0;
        while (canvas.alpha < 1)
        {
            canvas.alpha += Time.deltaTime / fadeTime;
           

            yield return null;
        }

        canvas.interactable = false;
        StartCoroutine(Fadeout());
        yield return null;
    }
}