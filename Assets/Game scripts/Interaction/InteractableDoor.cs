using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractableDoor : MonoBehaviour {

	public enum State
	{
		OPEN,
		CLOSE,
		INBETWEEN
	}

	public Transform Player;
	public State state;
	public AudioClip openSound;
	public AudioClip closeSound;
	AudioSource Audio;

	void Start() {
		Audio = GetComponent<AudioSource> ();
		state = InteractableDoor.State.CLOSE;

		if (Player == null) {
			Debug.LogError ("Missing player Transform");
		}
	}

	public void OnMouseEnter() {
		Debug.Log("Enter");
	}
	public void OnMouseExit() {
		Debug.Log("Exit");
	}
	public void OnMouseUp() {
		if  (Vector3.Distance (Player.position, this.transform.position) < 3.0f && tag == "Interactable") {
			Debug.Log("Up");
			switch (state) {
			case State.OPEN:
				state = InteractableDoor.State.INBETWEEN;
				StartCoroutine ("Close");
				break;
			case State.CLOSE:
				state = InteractableDoor.State.INBETWEEN;
				StartCoroutine ("Open");
				break;
			}
		}
	}

	public IEnumerator Open(){
		if (Vector3.Distance (Player.position, this.transform.position) < 3.0f && tag == "Interactable") {
			GetComponent<Animation> ().Play ("open");
			Audio.PlayOneShot (openSound);
			yield return new WaitForSeconds (GetComponent<Animation> () ["open"].length);
			state = InteractableDoor.State.OPEN;
		}
	}

	public IEnumerator Close(){
		if (Vector3.Distance (Player.position, this.transform.position) < 3.0f && tag == "Interactable") {
			GetComponent<Animation> ().Play ("close");
			Audio.PlayOneShot (closeSound);
			yield return new WaitForSeconds (GetComponent<Animation> () ["close"].length);
			state = InteractableDoor.State.CLOSE;
		}
	}
}