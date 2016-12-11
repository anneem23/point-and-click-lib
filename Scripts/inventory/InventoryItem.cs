using UnityEngine;
using System.Collections;

/**
 * TODO: Keep?
 * 
 * Representation of an Item in the inventory
 **/
namespace PointAndClick.Inventory {
	public class InventoryItem : MonoBehaviour {

		public Texture2D image;

		public InventoryItem (Texture2D img) {
			this.image = img;
		}

		public InventoryItem() {}

		// Use this for initialization
		//void Start () {}
		
		// Update is called once per frame
		//void Update () {}
		

	}
}