using UnityEngine;
using System.Collections;

namespace PointAndClick.Player {
	
	public static class ActivePlayer {

		public static string PLAYER = "Player";

		public static GameObject get() {
//			foreach (GameObject go in GameObject.FindGameObjectsWithTag (PLAYER)) {
//				PlayerBehaviour player = go.GetComponent<PlayerBehaviour> ();
//				// observe event from messagequeue?
////				if (player != null && player.isActive) {
////					return go;
////				}
//			}
			return null;
		}
	}
}
