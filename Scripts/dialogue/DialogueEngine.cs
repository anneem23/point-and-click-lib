using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PointAndClick.Dialogue {
	
	public class DialogueEngine {
		private Dialogue dialogue;
		private DialogueEntry currentEntry;

		public DialogueEngine (string text) : this (JsonUtility.FromJson<Dialogue> (text)) {
		}

		public DialogueEngine (Dialogue dialogue)
		{
			this.dialogue = dialogue;
			this.currentEntry = dialogue.dialogue;
		}

		public DialogueEntry find(String id) {
			if (id == null)
				return null;
			
			return find (id, dialogue.dialogue);
		}

		private DialogueEntry find(String id, DialogueEntry dialogue) {
			foreach (DialogueEntry entry in dialogue) {
				if (id.Equals (entry.id))
					return entry;
			}
			return null;
		}

		private DialogueEntry parent() {
			foreach (DialogueEntry entry in dialogue.dialogue.children) {
				if (find (currentEntry.id, currentEntry) != null)
					return entry;
			}
			return null;
		}

		public Dialogue Dialogue() {
			return dialogue;
		}


	}

}
