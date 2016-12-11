using UnityEngine;
using System.Collections;
using System;
using PointAndClick.Common;
using PointAndClick.Inventory;
using PointAndClick.Event;

namespace PointAndClick.Player {
	
	public class PlayerEventArgs : System.EventArgs {
		public PlayerBehaviour ActivePlayer { get; set; }
	}

	public class PlayerBehaviour : MonoBehaviour {

		public float speed;
		public InventoryBehaviour inventory;
		public Texture2D image;
		public int cash;
		public Texture2D cursorTexture;
		public int externallyControlled = 0;

		private ActionIcon cursor;
		public Movement movement;
		
		// Update is called once per frame
		void Start () {
			movement = new Movement (this, GetComponent<Animator> ());
			movement.stop ();
		}

		void Update () {
	    }

//		public bool isActive {
//			get { return GetComponentInParent<PlayerController> ().activePlayer == this; }
//		}

		public Vector3 position {
			get { 
				return transform.position;
			}
		}
		
		public Vector3 MouseToPlayerPosition(Vector3 mouse) {
			return new Vector3 (mouse.x, position.y, 0);
		}
		
		public int direction {
			get { return movement.animation; }
			set { movement.animation = value; }
		}
		
		public int VectorToDirection(Vector3 v) {
			var v2d = new Vector2 (-v.x, -v.y);
			if (v2d == Vector2.zero) {
				return Movement.STOP;
			}
			// get angle from slope (with arcus tangens)
			var angle = Mathf.Atan2 (v2d.y, v2d.x);
			if (angle < 0) {
				angle += Mathf.PI * 2;
			}
			
			var quadrant = (int)((angle / (Mathf.PI * 2)) * 7);
			
			int[] lut = new int[8] {
				Movement.RIGHT,
				Movement.BACKWARDS,
				Movement.BACKWARDS,
				Movement.LEFT,
				Movement.LEFT,
				Movement.FORWARDS,
				Movement.FORWARDS,
				Movement.RIGHT};
			return lut [quadrant];
		}
		
		void OnCollisionEnter2D(Collision2D otherObj) {
			movement.stop ();
		}

		public virtual void dispatch()    // the Trigger method, called to raise the event
		{
			Debug.Log ("Dispatching player event");
			PlayerEventArgs args = new PlayerEventArgs ();
			args.ActivePlayer = this;
			MessageQueue.dispatchEvent<PlayerEventArgs>(this, args); 
		}

		public void OnMouseUpAsButton() {
			dispatch ();
		}

	}
}