using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour {

    static FloatingText popupText;
    static GameObject canvas;

    public static void Init ()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        //specific location here
        if (popupText)
        {
            popupText = Resources.Load<FloatingText>("UI / UI_anim / PopUPtext / Prefabs / PopUpParentDamage");
        }
    }


    public static void CreateFloatingText(string text, Transform location)
    {
        //object pooling here to come
        //InstanceCanvas = GameObject.Find("MainCanvas");
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        
        //setting up text
        instance.SetText(text);
    }
}