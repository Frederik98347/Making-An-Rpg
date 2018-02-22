using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    [SerializeField] Animator anim;
    [SerializeField] Text text;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        text = anim.GetComponent<Text>();

    }

    public void SetText(string Text)
    {
        //text from ui set to = string text
        text.text = Text;
    }
}
