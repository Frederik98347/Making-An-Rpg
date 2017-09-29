using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractableChest : MonoBehaviour {
	
	public enum State
	{
		OPEN,
		CLOSE,
		INBETWEEN
	}

	float interactRange = 3.0f;
	public Transform Player;
	public State state;
	public AudioClip openSound;
	public AudioClip closeSound;
	AudioSource Audio;

	void Start() {
		Audio = GetComponent<AudioSource> ();
		state = InteractableChest.State.CLOSE;
	}

	public void OnMouseEnter() {
		Debug.Log("Enter");
	}
	public void OnMouseExit() {
		Debug.Log("Exit");
	}
	public void OnMouseUp() {
		if  (Vector3.Distance (Player.position, transform.position) < interactRange) {
			Debug.Log ("Up");
			switch (state) {
			case State.OPEN:
				state = InteractableChest.State.INBETWEEN;
				StartCoroutine ("Close");
				break;
			case State.CLOSE:
				state = InteractableChest.State.INBETWEEN;
				StartCoroutine ("Open");
				break;
			}
		}
	}

	public IEnumerator Open(){

		if (Vector3.Distance (Player.position, transform.position) < interactRange) {
			GetComponent<Animation> ().Play ("open");
			Audio.PlayOneShot (openSound);
			yield return new WaitForSeconds (GetComponent<Animation>()["open"].length);
			state = InteractableChest.State.OPEN;
		}
	}
	private IEnumerator Close(){
		if (Vector3.Distance (Player.position, transform.position) < interactRange) {
			GetComponent<Animation> ().Play ("close");
			Audio.PlayOneShot (closeSound);
			yield return new WaitForSeconds (GetComponent<Animation> () ["close"].length);
			state = InteractableChest.State.CLOSE;
		}
	}
}