using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
	public float AttackBonus;
	public float defenceBonus;
	public void Reset()
	{
		Type = ItemType.Equipment; 
		description = "This can be a tool or a weapon";
	}
	
}
