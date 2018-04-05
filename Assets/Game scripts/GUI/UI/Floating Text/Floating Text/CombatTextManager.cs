﻿using UnityEngine;
using TMPro;

public class CombatTextManager : MonoBehaviour {
    [Header("TextPrefabs")]
    public GameObject TextPrefabDamage;
    public GameObject TextPrefabHeal;
    public GameObject TextPrefabAuto;
    public GameObject TextPrefabAbility;
    [Header("Canvas the prefabs should instanciate at")]
    public Transform canvasTransform;
    [Header("Offset to Text position")]
    [Range(-5, 5)]
    public float offsetX;
    [Range(-5, 5)]
    public float offsetY;

    //SingleTon Pattern
    #region SingleTon
    static CombatTextManager instance;

    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatTextManager>();
            }

            return instance;
        }
    }
    #endregion

    public void CreateText(Vector3 pos, bool isDamage, bool isHeal, bool isAuto, bool isAbility, string text)
    {
        if (isDamage && TextPrefabDamage != null)
        {
            GameObject sct = Instantiate(TextPrefabDamage, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TextMeshPro>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp(sct);
        }

        if (isHeal && TextPrefabHeal != null)
        {
            GameObject sct = Instantiate(TextPrefabHeal, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TextMeshPro>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp(sct);
            return;
        }

        if (isAuto && TextPrefabAuto != null)
        {
            GameObject sct = Instantiate(TextPrefabAuto, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TextMeshPro>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp(sct);
            return;
        }

        if (isAbility && TextPrefabAbility != null)
        {
            GameObject sct = Instantiate(TextPrefabAbility, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TextMeshPro>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp(sct);
            return;
        }
    }

    private void SetUp(GameObject sct)
    {
        sct.transform.eulerAngles = Camera.main.transform.eulerAngles;
        var transform = canvasTransform.GetComponent<PositionasParent>();
        sct.transform.position = transform.targetPosition.position;
    }
}