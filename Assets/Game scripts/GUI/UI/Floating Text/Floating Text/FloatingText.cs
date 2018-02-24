using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {

    //[SerializeField] Animator anim;
    public string floatingText;
    public TMP_Text Text;

    /*  void OnEnable()
      {
          AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
          Debug.Log(clipInfo.Length);
          Destroy(gameObject, clipInfo[0].clip.length);
          text = anim.GetComponent<Text>();
      }
      */

    private void Start()
    {
        Text.transform.eulerAngles = Camera.main.transform.eulerAngles;
        Text.transform.eulerAngles = Camera.main.transform.eulerAngles;
    }

    public void CreateText(string text)
    {
        Text.text = floatingText;
    }
}