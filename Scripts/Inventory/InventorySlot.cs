using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
	Item item;

	// Use this for initialization
	public void AddItem (Item newItem) {
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}
	
	// Update is called once per frame
	public void ClearSlot () {
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void onRemoveButton() {
		Inventory.instance.Remove (item);
	}

	public void UseItem() {
		if (item != null) {
			item.Use ();
		}
	}
}
