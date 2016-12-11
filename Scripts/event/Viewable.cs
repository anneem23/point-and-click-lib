using System;
using UnityEngine;
using PointAndClick.Common;

namespace PointAndClick.Event {
	
	public class Viewable : Actionable {
		[SerializeField]
		public Item item;

		public override Item Action<Item>() {
			return item as Item;
		}

		public override void Execute() {
			invokeDeferredAction<Item> ();
		}

	}
}
