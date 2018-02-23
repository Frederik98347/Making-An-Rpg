using UnityEngine;
using TMPro;

public class CombatTextManager : MonoBehaviour {

    public GameObject TextPrefabDamage;
    public GameObject TextPrefabHeal;
    public RectTransform canvasTransform;

    public float Speed;
    public Vector3 Dir;

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

    public void CreateText(Vector3 pos, bool isDamage, bool isHeal, string text)
    {
        if (isDamage)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabDamage, pos, Quaternion.identity);
            sct.GetComponent<TMP_Text>().text = text;

            sct.transform.SetParent(canvasTransform);
            sct.GetComponent<RectTransform>().localScale = new Vector3(0.075f, 0.075f, 0.075f);
        }

        if (isHeal)
        {
            GameObject sct = (GameObject)Instantiate(TextPrefabHeal, pos, Quaternion.identity);
            sct.GetComponent<TMP_Text>().text = text;
            sct.transform.SetParent(canvasTransform);
            sct.GetComponent<RectTransform>().localScale = new Vector3(0.075f, 0.075f, 0.075f);

            sct.GetComponent<CombatText>().Initialize(Speed, Dir);
            //sct.GetComponent<TextMesh>().color = colour;
        }
    }
}