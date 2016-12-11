using UnityEngine;
using System.Collections;

/**
 * TODO: refactor me
 * 
 * show an icon next to the cursor
 **/
namespace PointAndClick.Common {
	
	public class ActionIcon : MonoBehaviour 
	{
		public Texture2D cursor;
		public bool hideCursor = false;

		void OnMouseEnter(){
			hideCursor = true;
			Cursor.visible = false;
		}
		
		void OnMouseExit(){
			hideCursor = false;
			Cursor.visible = true;
		}
		
		void OnGUI(){
			if (hideCursor) {
				GUI.DrawTexture (new Rect (Input.mousePosition.x + cursor.width / 4, 
				                           (Screen.height - Input.mousePosition.y) - cursor.height, 
				                           cursor.width, 
				                           cursor.height), cursor);
			}
		}
	}
}