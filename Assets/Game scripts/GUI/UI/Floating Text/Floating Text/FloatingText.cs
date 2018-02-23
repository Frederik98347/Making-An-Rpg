using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    [SerializeField] Animator anim;
    Text text;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(clipInfo.Length);
        Destroy(gameObject, clipInfo[0].clip.length);
        text = anim.GetComponent<Text>();
    }

    public void SetText(string Text)
    {
        //text from ui set to = string text
        text.text = Text;
    }
}