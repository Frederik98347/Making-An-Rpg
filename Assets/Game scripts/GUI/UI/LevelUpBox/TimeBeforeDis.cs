using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBeforeDis : MonoBehaviour {

    [SerializeField] float timeBeforeExpire = 5f;
    [SerializeField] GameObject prefab;
    Fade fade;

    void Start()
    {
        prefab.SetActive(false);
    }

    void Hide()
    {
        fade.FadeOut();
        StartCoroutine(HideCoroutine());
        prefab.SetActive(false);
    }

    public void Show()
    {
        fade.FadeIn();
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeExpire);
        prefab.SetActive(true);
    }

    IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeExpire);
        prefab.SetActive(false);
    }
}