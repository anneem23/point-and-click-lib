using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.EventSystems;


namespace PointAndClick.Common {
	
	public class Item : MonoBehaviour {
		
		public Texture2D cursorIcon;
		private ActionIcon cursor;

		void Start () {
			cursor = gameObject.AddComponent<ActionIcon> () as ActionIcon;
			cursor.cursor = cursorIcon;
		}

	}
}
