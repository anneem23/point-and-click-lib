using UnityEngine;
using System.Collections;
using PointAndClick.Inventory;
using PointAndClick.Player;

/**
 * TODO: Hide after a few seconds and show on click or 
 * when item was collected. 
 * 
 * Container for the inventory
 **/
namespace PointAndClick.Inventory {

	public class InventoryContainer : MonoBehaviour {

		public Slot bgContainer;
		public Slot[] inventoryContainer;
		public Slot playerProfileContainer;
		public string playerCash;

		public Rect slotDimensions;
		public Rect playerProfileDimensions;
		public float itemDistance;

		public GUIStyle labelStyle;
		public GUIStyle statisticsStyle;
		public GUIStyle buttonStyle;

		public Texture2D playerImageBtn;
		
		void Start() {
			init ();
		}

		void init() {
			GameObject go = ActivePlayer.get();
			if (go != null) {
				PlayerBehaviour player = go.GetComponent<PlayerBehaviour>();
				this.playerImageBtn = player.image;
				InventoryBehaviour inventory = player.GetComponent<InventoryBehaviour>();
				playerProfileContainer = new Slot(player.image, playerProfileDimensions, labelStyle);
				playerProfileContainer.label = player.name;
				//Debug.Log ("Inventory found: " +  inventory);
				if (inventory.size() > 0) {
					inventoryContainer = new Slot[inventory.size()];
					for (int i = 0; i < inventory.size(); i++) {
						inventoryContainer[i] = new Slot(inventory.inventoryItems[i].image, 
						                                 createRect(i),
						                                 labelStyle);	
					}
				}
				playerCash = "Money: " + player.cash.ToString();
			}

			bgContainer.visible = false;
			playerProfileContainer.visible = false;
		}
		
		void Update() {
			init ();
		}

		void OnGUI() {
			bgContainer.Draw ();
			drawInventoryItems ();
			playerProfileContainer.Draw();
			drawStatistics ();

			if (GUI.Button (new Rect (10,350,100,100), this.playerImageBtn, this.buttonStyle)) { 
				bgContainer.visible = true;
				playerProfileContainer.visible = true;
			}
		} 

		private void drawStatistics() {
			GUI.Label (new Rect (Screen.width * (bgContainer.position.x + bgContainer.position.width - (playerProfileDimensions.width * 2) - itemDistance), 
			                     Screen.height * playerProfileDimensions.y,  
			                     Screen.width * playerProfileDimensions.width, 
			                     Screen.height * playerProfileDimensions.height / 4), 
			           playerCash, statisticsStyle);
		}

		private void drawInventoryItems() {
			for (int count = 0; count < inventoryContainer.Length; count++) {
				inventoryContainer[count].Draw();
			}
		}

		private Rect createRect(int index){
			return new Rect (slotDimensions.x + (index * (slotDimensions.width + itemDistance)), 
			                slotDimensions.y, 
			                slotDimensions.width, 
			                slotDimensions.height);
		}
	}
}