using UnityEngine;


namespace PointAndClick.Event {
	
	public class Talkable : Actionable {
		[SerializeField]
		public TextAsset dialogues;

		public override TextAsset Action<TextAsset>() {
			return dialogues as TextAsset;
		}

		public override void Execute() {
			invokeDeferredAction<TextAsset> ();
		}

	}
}