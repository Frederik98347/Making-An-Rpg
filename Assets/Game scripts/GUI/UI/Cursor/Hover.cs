using UnityEngine;

namespace RpgTools.UI {
    public class Hover : MonoBehaviour {

        public Texture2D defaultTexture;
        public Texture2D FriendlyTexture;
        public Texture2D BattleTexture;
        public Texture2D VendorTexture;
        public Texture2D QuestTexture;

        public CursorMode curMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;
        [SerializeField] LayerMask mask;

        // Use this for initialization
        void Start() {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
            mask = 1;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, mask.value)) {
                if (hit.collider.gameObject.tag == "enemy")
                {
                    if (hit.collider.gameObject.GetComponent<Enemy.Enemy>().agg == Enemy.Enemy.Aggresiveness.NONAGGRESIVE)
                    {
                        Cursor.SetCursor(FriendlyTexture, hotSpot, curMode);
                    } else
                    {
                        Cursor.SetCursor(BattleTexture, hotSpot, curMode);
                    }

                } else if (hit.collider.gameObject.tag == "Npc")
                {
                    Cursor.SetCursor(FriendlyTexture, hotSpot, curMode);
                } else
                {
                    Cursor.SetCursor(defaultTexture, hotSpot, curMode);
                }
            }
        }
    }
}