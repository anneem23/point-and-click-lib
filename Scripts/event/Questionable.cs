using System;
using UnityEngine;


namespace PointAndClick.Event {
	public class Questionable : Actionable {
		
		public override GameObject Action<GameObject>() {
			return this.gameObject as GameObject;
		}

		public override void Execute() {
			invokeAction<GameObject> ();
		}

	}
}