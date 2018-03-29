using UnityEngine;

public class Hover : MonoBehaviour {

    public Texture2D defaultTexture;
    public Texture2D FriendlyTexture;
    public Texture2D BattleTexture;
    public Texture2D VendorTexture;
    public Texture2D QuestTexture;

    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject Target;

    // Use this for initialization
    void Start () {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }

    private void OnMouseEnter()
    {
        if (Target.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Cursor.SetCursor(BattleTexture, hotSpot, curMode);
        } else if (Target.tag == "Npc")
        {
            Cursor.SetCursor(FriendlyTexture, hotSpot, curMode);
        }

        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }
}