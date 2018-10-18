﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	public static EquipmentManager instance;

	void Awake() 
	{
		if (instance != null) {
			Debug.LogWarning ("There's more than one instance of the singleton EquipmentManager");
			return;
		}
		instance = this;
	}

	public Equipment[] defaultItems;
	public SkinnedMeshRenderer targetMesh;
	Equipment[] currentEquipment;
	SkinnedMeshRenderer[] currentMeshes;

	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;

	Inventory inventory;

	void Start() 
	{
		inventory = Inventory.instance;
		int numSlots = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
		currentMeshes = new SkinnedMeshRenderer[numSlots];

		EquipDefaultItems ();
	}

	public void Equip(Equipment newItem) 
	{
		int slotIndex = (int)newItem.equipSlot;
		Unequip (slotIndex);
		Equipment oldItem = null;

		if (onEquipmentChanged != null) {
			onEquipmentChanged.Invoke (newItem, oldItem);
		}

		SetEquipmentBlendShapes (newItem, 100);

		currentEquipment [slotIndex] = newItem;
		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer> (newItem.mesh);
		newMesh.transform.parent = targetMesh.transform;

		newMesh.bones = targetMesh.bones;
		newMesh.rootBone = targetMesh.rootBone;

		currentMeshes [slotIndex] = newMesh;
	}

	public void Unequip(int slotIndex)
	{
		if (currentEquipment [slotIndex] != null) {

			if (currentMeshes [slotIndex] != null) {
				Destroy (currentMeshes [slotIndex].gameObject);
			}

			Equipment oldItem = currentEquipment [slotIndex];
			SetEquipmentBlendShapes (oldItem, 100);
			inventory.Add (oldItem);

			currentEquipment [slotIndex] = null;

			if (onEquipmentChanged != null) {
				onEquipmentChanged.Invoke (null, oldItem);
			}
		}
	}

	public void UnequipAll() 
	{
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}
		EquipDefaultItems ();
	}

	void SetEquipmentBlendShapes(Equipment item, int weight) 
	{
		foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
			targetMesh.SetBlendShapeWeight ((int)blendShape, weight);
		}
	}

	void EquipDefaultItems()
	{
		foreach (Equipment item in defaultItems) {
			Equip (item);
		}
	}

	void Update() 
	{
		if (Input.GetKeyDown (KeyCode.U)) {
			UnequipAll ();
		}
	}
}