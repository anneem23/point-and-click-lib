using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PointAndClick.Inventory {
	public class InventoryBehaviour : MonoBehaviour {

		public List<InventoryItem> inventoryItems;
		public int maxItems = 7;
		private int idx = 0;

		void Start () {
			inventoryItems = new List<InventoryItem> ();
		}

		void OnGUI() {

		}

		public int size() {
			return idx;
		}

		public void addToInventory(InventoryItem item) {
			if (idx >= maxItems) {
				throw new IndexOutOfRangeException("The maximum number of items a player can carry in its inventory is exceeded." +
					"Please remove other items from your inventory first.");		
			}

			this.inventoryItems [idx++] = item;
		}
	}
}
