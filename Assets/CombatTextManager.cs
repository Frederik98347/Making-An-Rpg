using UnityEngine;
using TMPro;

public class CombatTextManager : MonoBehaviour {
    [Header("TextPrefabs")]
    public GameObject TextPrefabDamage;
    public GameObject TextPrefabHeal;
    public GameObject TextPrefabAuto;
    public GameObject TextPrefabAbility;
    [Header("Canvas the prefabs should instanciate at")]
    public RectTransform canvasTransform;
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
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }

            return instance;
        }
    }
    #endregion

    public void CreateText(Vector3 pos, bool isDamage, bool isHeal, bool isAuto, bool isAbility, string text)
    {
        if (isDamage)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabDamage, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TMP_Text>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp();
        }

        if (isHeal)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabHeal, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TMP_Text>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp();
        }

        if (isAuto)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabAuto, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TMP_Text>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp();
        }

        if (isAbility)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabAbility, new Vector3(pos.x + (Random.Range(-offsetX, offsetX)), pos.y + offsetY, pos.z), Quaternion.identity) as GameObject;
            sct.GetComponent<TMP_Text>().text = text;

            sct.transform.SetParent(canvasTransform, false);
            SetUp();
        }
    }

    private void SetUp()
    {
        Transform Text = GetComponent<TMP_Text>().transform;
        Text.eulerAngles = Camera.main.transform.eulerAngles;
    }
}