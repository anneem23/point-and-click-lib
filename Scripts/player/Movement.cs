using UnityEngine;
using System.Collections;

namespace PointAndClick.Player {
	
	[System.Serializable]
	public class Movement {
		
		public static int FORWARDS = 0;
		public static int RIGHT = 1;
		public static int BACKWARDS = 2;
		public static int LEFT = 3;
		public static int STOP = -1;

		private Animator animator;
		private MonoBehaviour character;

		public Movement(MonoBehaviour t, Animator animator) {
			this.character = t;
			this.animator = animator;
		}

		public void stop() {
			animation = Movement.STOP;
			this.character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		public int animation { 
			get { return animator.GetInteger("Direction");}
			set { if(animation != value) { animator.SetInteger ("Direction", value); } }
		}



	}
}