using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

    [Tooltip("Animation for Text")]public Animator anim;
    [Tooltip("How much you wanna delay the text destruction")]
    public float fadeTimeOffset = .5f;
    public float timeBeforeFade = .2f;
    public float fadeTime = .8f;

    void OnEnable()
    {
        StartCoroutine(FadeIn());
        TMPro.TextMeshPro text = GetComponent<TMPro.TextMeshPro>();

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //object pooling
        Destroy(gameObject, clipInfo[0].clip.length + fadeTimeOffset*Time.deltaTime);
    }

    private IEnumerator Fadeout()
    {

        while (GetComponent<TMPro.TextMeshPro>().alpha > 0)
        {
            if (timeBeforeFade > 0f)
            {
                timeBeforeFade -= Time.deltaTime;
            }
            else
            {
                GetComponent<TMPro.TextMeshPro>().alpha -= Time.deltaTime / fadeTime;
            }
            yield return null;
        }

        gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator FadeIn()
    {

        gameObject.SetActive(true);
        GetComponent<TMPro.TextMeshPro>().alpha = 0;
        while (GetComponent<TMPro.TextMeshPro>().alpha < 1)
        {
            GetComponent<TMPro.TextMeshPro>().alpha += Time.deltaTime / fadeTime;


            yield return null;
        }

        StartCoroutine(Fadeout());
        yield return null;
    }
}