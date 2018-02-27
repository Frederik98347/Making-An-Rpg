using UnityEngine;

public class Hp_barPos : MonoBehaviour
{

    public float distY;
    public float distZ;
    public Transform owner;
    public bool IsEnemy;
    public float fadeRange;
    [SerializeField] CharacterHealthsytem healthSystem;
    [SerializeField] Transform player;
    [Range(1f, 10f)]
    public float HpbarFadeRange = 8f;
    [Tooltip("Canvas to disable")]
    [SerializeField] Canvas canvas;

    private void Start()
    {
        canvas.gameObject.SetActive(false);

        if (healthSystem != null)
        {
            healthSystem.healthBar.value = healthSystem.CalculateHealth();
        }
        else
        {
            Debug.LogWarning("Couldnt Find CharacterHealthsystem!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
        if (owner != null)
        {
            Vector3 pos = new Vector3(owner.position.x, owner.position.y + distY, owner.position.z + distZ);
            transform.position = pos;
        }

        HpDistDisappear(HpbarFadeRange);
    }

    void HpDistDisappear(float fadeRange)
    {
        if (IsEnemy == true)
        {
            if (Vector3.Distance(player.position, owner.position) <= fadeRange)
            {
                canvas.gameObject.SetActive(true);
            }
            if (Vector3.Distance(player.position, owner.position) >= fadeRange) 
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}