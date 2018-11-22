using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable {

	public Dialogue dialogue;
	public GameObject dialogueUI;

	public bool isAlwaysInteractable = true;

	// This is use for invisible colliders that can cause an automatic
	// display of dialogue
	public bool isAutoTriggered = false;

	// This checks if the trigger was already triggered
	bool hasTriggered = false;

	public void TriggerDialogue() {
		
		if (dialogueUI != null) {
			dialogueUI.SetActive (true);
		}

		DialogueManager.instance.StartDialogue (dialogue);
	}

	public override void Interact ()
	{
		base.Interact ();

		if (isAlwaysInteractable || !hasTriggered) {
			TriggerDialogue();
			hasTriggered = true;
		}

	}

	void OnTriggerEnter (Collider collider) {
		Debug.Log ("Should display dialogue");
		if (isAutoTriggered) {
			Interact ();
		}
	}

}