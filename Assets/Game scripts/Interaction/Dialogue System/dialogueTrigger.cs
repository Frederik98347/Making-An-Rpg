using UnityEngine;
using System.Collections;

public class dialogueTrigger : MonoBehaviour {
	
	public Transform Player;
	public Dialogue dialogue;

	public void TriggerDialogue() {
		FindObjectOfType<dialogueManager> ().StartDialogue (dialogue);
	}

	void OnMouseEnter() {
		if  (Vector3.Distance (Player.position, this.transform.position) < 3.0f && tag == "Interactable") {
			// Highlight on mouseover

			if (Input.GetMouseButton (0) || Input.GetMouseButtonUp (1)) {
				TriggerDialogue ();

			}
		}	
	}
}
