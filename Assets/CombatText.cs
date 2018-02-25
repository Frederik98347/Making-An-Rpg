using System.Collections;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour {

    float speed;
    Vector3 dir;
    float fadeTime;

    public float FadeTime
    {
        get
        {
            return fadeTime;
        }

        set
        {
            fadeTime = value;
        }
    }

    public Vector3 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            dir = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Update is called once per frame
    void Update () {
        float translation = Speed * Time.deltaTime;

        transform.Translate(Dir * translation);
	}

    public void Initialize(float speed, Vector3 dir, float fadeTime) 
    {
        this.speed = Speed;
        this.fadeTime = fadeTime;
        this.dir = Dir;

        StartCoroutine (Fadeout());
    }

    private IEnumerator Fadeout()
    {
        float startAlpha = GetComponent<TMP_Text>().color.a;

        float rate = 1.0f / FadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tmpColor = GetComponent<TMP_Text>().color;

            GetComponent<TMP_Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));

            progress += rate * Time.deltaTime;

            yield return null;
        }

        //objectPooling here or something
        Destroy(gameObject);
    }
}