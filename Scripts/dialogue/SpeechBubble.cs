using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PointAndClick.Dialogue {

	public class SpeechBubble : MonoBehaviour {
		
		[SerializeField]
		public GameObject bubble;

		private float distance;

		public void Show(GameObject go) {
			float y = go.transform.position.y + (go.GetComponent<BoxCollider2D> ().size.y * go.transform.localScale.y) + 0.2f;
		
			bubble = (GameObject) Instantiate(bubble, new Vector3 (go.transform.position.x, y, go.transform.position.z), Quaternion.identity);
			bubble.transform.SetParent(this.transform);
			distance = bubble.transform.position.y + .9f;
		}

		public void Hide() {
			Destroy (bubble);
		}


		public void ClearContent() {
			foreach (GameObject go in bubble.GetComponents<GameObject>()) {
				if (!"txtTemplate".Equals (go.name) && !"bg".Equals (go.name)) {
					Destroy (go);
				}
			}
		}


		public IEnumerator AddContent(DialogueEntry entry) {
			GameObject textTemplate = GameObject.Find ("txtTemplate");

			GameObject d = (GameObject)Instantiate (textTemplate, 
													new Vector3 (this.bubble.transform.position.x, distance, 0), 
				              						Quaternion.identity);
			d.name = entry.id;
			d.transform.SetParent (this.bubble.transform);

			yield return TypeText(entry.question, d.GetComponent<Text> ());

			distance = d.transform.position.y - GetLocalHeight(d.GetComponent<RectTransform>()) - .1f;
		}

		private float GetLocalHeight(RectTransform rectTransform) {
			return rectTransform == null ? 0f : (rectTransform.sizeDelta.y * rectTransform.localScale.y);
		}

		IEnumerator TypeText (string message, Text textComp) {
			foreach (char letter in message.ToCharArray()) {
				textComp.text += letter;
				//			if (typeSound1 && typeSound2)
				//				SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
				yield return 0;
				yield return new WaitForSeconds (0.2f);
			}
		}
	}
}