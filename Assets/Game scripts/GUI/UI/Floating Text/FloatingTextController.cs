using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    static FloatingText popupText;
    static GameObject canvas;

    public static void Init (bool isDamage, bool isHealth)
    {

        if (isDamage == true)
        {
            //specific location here
            canvas = GameObject.Find("Canvas");
            if (!popupText)
            {
                popupText = Resources.Load<FloatingText>("Resources/UI/UI_anim/PopUPtext/Prefabs/PopUpParentDamage");
            }
        }

        if (isHealth == true)
        {
            //specific location here
            canvas = GameObject.Find("Canvas");
            if (!popupText)
            {
                popupText = Resources.Load<FloatingText>("Resources/UI/UI_anim/PopUPtext/Prefabs/PopUpParentHeal");
            }
        }
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        //object pooling here to come
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.5f, .5f), location.position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(text);
    }
}