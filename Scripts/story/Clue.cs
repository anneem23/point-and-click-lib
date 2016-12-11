using UnityEngine;
using System.Collections;
using System;
using PointAndClick.Common;
using PointAndClick.Event;

namespace PointAndClick.Story {
	/**
	 * EventArgs class that contains a quest
	 **/
	public class ClueEventArgs : System.EventArgs {
		public Clue Clue { get; set; }
	}

	/**
	 * A clue is an item that is part of a quest
	 * Sends EventArgs of type ClueEventArgs
	 **/
	public class Clue : Item {
		
		public Actionable value;

		public Clue() {
	//		MessageQueue.register<ActionEventArgs<Item>>(new EventHandler(handleDialogueEvent));
		}

		public Actionable Value {
			get {
				return value;
			}
		}

		public void OnMouseUpAsButton() {
			Debug.Log (transform.name + " clicked");
			MessageQueue.dispatchEvent<ClueEventArgs> (this, ToEventArgs ());
		}


		ClueEventArgs ToEventArgs () {
			ClueEventArgs args = new ClueEventArgs ();
			args.Clue = this;
			return args;
		}
	}
}
