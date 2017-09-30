using UnityEngine;
using System.Collections;

public class dialogueTrigger : MonoBehaviour {
	
	public Interactable interactable;
	public Transform player;
	public Dialogue dialogue;

	public void TriggerDialogue() {
		FindObjectOfType<dialogueManager> ().StartDialogue (dialogue);
	}

	void OnMouseEnter() {
		if  (Vector3.Distance (player.transform.position, this.transform.position) < interactable.interactRange && tag == "Interactable") {
			// Highlight on mouseover

			if (Input.GetMouseButton (0) || Input.GetMouseButtonUp (1)) {
				TriggerDialogue ();

			}
		}	
	}
}
