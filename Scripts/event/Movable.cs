using System;
using UnityEngine;


namespace PointAndClick.Event {
	
	public class Movable : Actionable {

		// dummy implementation
		public override GameObject Action<GameObject>() {
			return null;
		}

		public override void Execute() {
			MoveToTargetEventArgs mttArgs = new MoveToTargetEventArgs();
			MessageQueue.dispatchEvent (this, mttArgs);
		}

	}
}