using UnityEngine;

public class Interactable : MonoBehaviour {

    // interaction radius
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact ()
    {
        // meant to be overwritten
        Debug.Log("interacting with!: " + transform.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (isFocus && !hasInteracted)
            {
                float distance = Vector3.Distance(player.transform.position, interactionTransform.position);
                if (distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }
    }
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    public void OnDefocused ()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}