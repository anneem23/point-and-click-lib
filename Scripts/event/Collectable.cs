using System;
using UnityEngine;
using PointAndClick.Inventory;

namespace PointAndClick.Event {
	public class Collectable : Actionable {
		[SerializeField]
		public InventoryItem item;

		public override InventoryItem Action<InventoryItem>() {
			return item as InventoryItem;
		}

		public override void Execute() {
			invokeDeferredAction<InventoryItem> ();
		}
	}
}
