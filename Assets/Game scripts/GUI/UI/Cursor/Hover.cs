using UnityEngine;

namespace RpgTools.UI {
    public class Hover : MonoBehaviour {

        [SerializeField] Texture2D defaultTexture;
        [SerializeField] Texture2D FriendlyTexture;
        [SerializeField] Texture2D AggresiveTexture;
        [SerializeField] Texture2D VendorTexture;
        [SerializeField] Texture2D QuestTexture;
        [SerializeField] Texture2D BattleTexture;

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
            var player = FindObjectOfType<PlayerClass.Player>().GetComponent<PlayerClass.Player>();

            if (Physics.Raycast(ray, out hit, 500f, mask.value)) {
                if (hit.collider.gameObject.tag == "enemy")
                {
                    if (hit.collider.gameObject.GetComponent<Enemy.Enemy>().agg == Enemy.Enemy.Aggresiveness.NONAGGRESIVE && player.autoAttacking != true)
                    {
                        Cursor.SetCursor(FriendlyTexture, hotSpot, curMode);
                    } else
                    {
                        if (player.autoAttacking != true)
                        {
                            Cursor.SetCursor(AggresiveTexture, hotSpot, curMode);
                        }
                    }

                    if (player.autoAttacking == true || hit.collider.gameObject.GetComponent<Enemy.Enemy>().state == Enemy.Enemy.State.Attacking)
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