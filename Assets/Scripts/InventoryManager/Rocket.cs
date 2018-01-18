using UnityEngine;
using System.Collections;

[CreateAssetMenu()]                                                           //  Our Representation of an InventoryItem
public class Rocket : ScriptableObject
{
	public string name = "New Item";                                      //  What the item will be called in the inventory
	public Sprite left = null, right = null;                                           //  What the item will look like in the inventory
	public Rigidbody itemObject = null;                                         //  Optional slot for a PreFab to instantiate when discarding
	public bool isUnique = false;                                               //  Optional checkbox to indicate that there should only be one of these items per game
	public bool isIndestructible = false;                                       //  Optional checkbox to prevent an item from being destroyed by the player (unimplemented)
	public bool isQuestItem = false;                                            //  Examples of additional information that could be held in InventoryItem
	public bool isStackable = false;                                            //  Examples of additional information that could be held in InventoryItem
	public bool destroyOnUse = false;                                           //  Examples of additional information that could be held in InventoryItem
	public float encumbranceValue = 0;                                          //  Examples of additional information that could be held in InventoryItem  !!!
}