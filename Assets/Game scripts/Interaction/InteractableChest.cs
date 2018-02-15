using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animation))]
public class InteractableChest : MonoBehaviour{
	
	public enum State
	{
		OPEN,
		CLOSE,
		INBETWEEN
	}

	public State state;
	public Transform Player;
	public AudioClip openSound;
	public AudioClip closeSound;
	AudioSource Audio;
	Animation anim;
	public AnimationClip close;
	public AnimationClip open;

	void Start() {
		Audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animation> ();
		state = InteractableChest.State.CLOSE;
	}

	public void OnMouseEnter() {
		Debug.Log("Enter");
	}
	public void OnMouseExit() {
		Debug.Log("Exit");
	}
	public void OnMouseUp() {
		if (Vector3.Distance (Player.position, this.transform.position) < 3.0f && tag == "Interactable") {
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

		if (Vector3.Distance (Player.position, this.transform.position) < 3.0f) {
			anim.clip = open;
			anim.Play();
			Audio.PlayOneShot (openSound);
			yield return new WaitForSeconds (anim.clip.length);
			state = InteractableChest.State.OPEN;
		}
	}
	private IEnumerator Close(){
		if (Vector3.Distance (Player.position, this.transform.position) < 3.0f) {
			anim.clip = close;
			anim.Play();
			Audio.PlayOneShot (closeSound);
			yield return new WaitForSeconds (anim.clip.length);
			state = InteractableChest.State.CLOSE;
		}
	}
}