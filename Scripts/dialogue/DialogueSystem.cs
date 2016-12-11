using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using PointAndClick.Event;

namespace PointAndClick.Dialogue {

	[Serializable]
	public class Dialogue {
		public DialogueEntry dialogue;
	}
	// TODO think about a graph instead of dialogue data structure
	public class DialogueSystem : MonoBehaviour {
		[SerializeField]
		public SpeechBubble speechBubble;

		private DialogueEngine dialogueEngine;

		void Start () {
			MessageQueue.register<ActionEventArgs<TextAsset>>(new EventHandler(handleDialogueEvent));
			MessageQueue.register<ActionEventArgs<GameObject>>(new EventHandler(handleQuestion));
		}
			
		public void handleDialogueEvent(object sender, EventArgs args) {
			dialogueEngine = new DialogueEngine (((ActionEventArgs<TextAsset>) args).Value.text);
			speechBubble.Show (((Talkable)sender).gameObject);
			StartCoroutine(displayQuestions (dialogueEngine.Dialogue().dialogue));
		}

		public void handleQuestion(object sender, EventArgs args) {
			GameObject g = ((ActionEventArgs<GameObject>)args).Value;
			DialogueEntry result = dialogueEngine.find(g.name);
			clearActiveDialogues ();
			StartCoroutine (addDialogue (result));
			StartCoroutine(createResponse (g, result));
		}

		IEnumerator createResponse(GameObject g, DialogueEntry result) {
			yield return new WaitForSeconds(15);
			speechBubble.ClearContent ();
			displayQuestions (result);
		}


		IEnumerator displayQuestions(DialogueEntry entry) {
			foreach (DialogueEntry de in entry.children) {
				yield return addDialogue (de);
			}
		}

		IEnumerator addDialogue(DialogueEntry de) {
			yield return speechBubble.AddContent(de);
		}

		void clearActiveDialogues() {
			speechBubble.ClearContent ();
		}

	}
}