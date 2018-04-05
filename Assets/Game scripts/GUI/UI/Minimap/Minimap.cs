using UnityEngine;
using RpgTools.PlayerClass;

public class Minimap : MonoBehaviour {

	public Transform player;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void LateUpdate () {
		Vector3 newPosition = player.position;
		newPosition.y = player.position.y +50f; 
		transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

	}
}
