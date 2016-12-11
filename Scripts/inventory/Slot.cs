using UnityEngine;
using System.Collections;

namespace PointAndClick.Inventory {

	[System.Serializable]
	public class Slot {

		public Texture2D image;
		public string label;
		public Rect position;
		public GUIStyle style;

		public bool visible;

		public Slot (Texture2D image, Rect position, GUIStyle style) {
			this.image = image;
			this.position = position;
			this.style = style;
			this.visible = true;
		}

		public void Draw() {
			if (visible) {
				Rect imgPosition = getScreenRect (position);

				if (image.width < imgPosition.width) {
						Rect tmp = new Rect (imgPosition.x + ((imgPosition.width - image.width) / 2),
	                     imgPosition.y + ((imgPosition.height - image.height) / 2),
	                     image.width,
	                     image.height);
						GUI.DrawTexture (tmp, image);
						if (label != null) {
								Rect labelPos = new Rect (tmp.x, tmp.y + tmp.height + 0.11f, tmp.width, tmp.height);
								GUI.Label (labelPos, 
		          label, style);
						}
				} else {
						GUI.DrawTexture (imgPosition, image);
						if (label != null) {
								Rect labelPos = new Rect (imgPosition.x, imgPosition.y + 0.11f, imgPosition.width, imgPosition.height);
								GUI.Label (labelPos, 
		          label, style);
						}
				}	
			}
		}

		Rect getScreenRect(Rect pos) {
			return new Rect(Screen.width * pos.x, 
			                Screen.height * pos.y, 
			                Screen.width * pos.width, 
			                Screen.height * pos.height);
		}
	}

}