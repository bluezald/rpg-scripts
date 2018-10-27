using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	void Start () 
	{
		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;	
	}

	void OnEquipmentChanged (Equipment newItem, Equipment oldItem) 
	{
		if (newItem != null) {
			armor.AddModifier (newItem.armorModifier);
			damage.AddModifier (newItem.damageModifier);
		}

		if (oldItem != null) {
			armor.RemoveModifier (newItem.armorModifier);
			damage.RemoveModifier (newItem.damageModifier);
		}
	}
}
