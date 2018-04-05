using UnityEngine;

public class PositionasParent : MonoBehaviour {

    public Transform targetPosition;
    [Range(0.1f, 1.5f)]
    [SerializeField] float offsetX = 0.1f;

    private void Start()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetPosition.position);
        transform.position = new Vector2(targetPosition.position.x + (Random.Range(-offsetX, offsetX)), targetPosition.position.y);
    }
}