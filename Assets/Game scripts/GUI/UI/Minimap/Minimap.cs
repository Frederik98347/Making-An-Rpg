using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform player;

	void LateUpdate () {
		Vector3 newPosition = player.position;
		newPosition.y = player.position.y;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler (90f, player.eulerAngles.y, 0f);
	}
}
