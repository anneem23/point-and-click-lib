using System.Collections.Generic;
using System.Collections;
using System;

namespace PointAndClick.Dialogue {

	// TODO replace DialogEntry with GraphNode
	[Serializable]
	public class DialogueEntry : System.Collections.Generic.IEnumerable<DialogueEntry> {
		public string id;
		public string question;
		public string answer;

		public DialogueEntry[] children;

		public Boolean hasChildren() {
			return !(children != null || children.Length == 0);
		}

		public IEnumerable<DialogueEntry> Children {
			get { return children; }
		}


	//	public IEnumerator GetEnumerator() {
	//		return GetEnumerator ();
	//	}

		public IEnumerator<DialogueEntry> GetEnumerator() {
			yield return this;

			foreach (DialogueEntry node1 in children) {
				foreach (DialogueEntry node2 in node1)
					yield return node2;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}