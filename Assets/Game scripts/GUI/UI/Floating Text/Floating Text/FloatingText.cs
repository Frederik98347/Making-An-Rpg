using UnityEngine;

public class FloatingText : MonoBehaviour {

    [Tooltip("Animation for Text")]public Animator anim;
    [Tooltip("How much you wanna delay the text destruction")]
    public float fadeTimeOffset;

      void OnEnable()
      {
          AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //object pooling
          Destroy(gameObject, clipInfo[0].clip.length + fadeTimeOffset);
    }
}