using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PointAndClick.Player;

namespace PointAndClick.Pathfinding {
	
	public class PathfindingBehaviour : MonoBehaviour {
		
		public delegate void OnComplete();

		public GameObject background;

		private Landscape world;
		private GameObject[] obstacles;

		Sequence pathSeq;
		OnComplete onComplete;

		void Start () {
			world = new Landscape (background.GetComponent<Renderer>().bounds);
			obstacles = GameObject.FindGameObjectsWithTag ("Ground");
			foreach (GameObject obstacle in obstacles) {
				BoxCollider2D boxCollider = obstacle.GetComponent<BoxCollider2D>();
				Vector3 oPos = obstacle.transform.position;
				for (float yPos = oPos.y; yPos <= oPos.y + boxCollider.size.y * background.transform.localScale.x; yPos++) {
					for (float xPos = oPos.x; xPos <= oPos.x + boxCollider.size.x; xPos++) {
						Vector3 obstaclePosition = new Vector3((int) (xPos), (int) (yPos));
						world.AddObstacle(obstaclePosition);
					}
				}
			}
		}

		public void StopPlayer() {
			pathSeq.Kill();
			onComplete ();
		}

		// Update is called once per frame
		public void WalkPlayer(PlayerBehaviour player, OnComplete onComplete) {
			//DrawObstacles ();
			//if (Input.GetMouseButtonDown(0)) {
				this.onComplete = onComplete;
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				mousePosition.z = 0.0f;

				if(pathSeq != null) {
					pathSeq.Kill();
					pathSeq = null;
				}

				Node step = new AStarPathfinding(
					this.world, 
					new ManhattanDistance()).findPath(
						player.position,
				        player.MouseToPlayerPosition(mousePosition)
				);

				var path = new List<Node>();
				var lengths = new List<float>();
				var directions = new List<int>();

				while(step.Parent () != null) {
					path.Add(step.Parent ());
					var d = step.position - step.Parent ().position;
					lengths.Add (d.magnitude);
					directions.Add (player.VectorToDirection(d));
					step = step.Parent();
				}

				switch(path.Count) {
				case 0:
						break;
				case 1:
					pathSeq = DOTween.Sequence();
					SetupSequence(pathSeq, player);
					pathSeq.Append(DOTween.To(
						()=>player.direction, 
						d=>player.direction = d, 
						directions[0],
						0));
					pathSeq.Append(player.transform.DOMove(
							path[0].position, 
							lengths[0] // speed
						).SetEase (Ease.Linear)
					);
					FinishSequence(pathSeq, player);
					break;
				default:
					pathSeq = DOTween.Sequence();    
					SetupSequence(pathSeq, player);
	                for(int i = 0; i < path.Count; i++) {
						
						Ease ease = Ease.Linear;
						if(i == 0) {
							ease = Ease.InSine;
						} else if(i == path.Count - 1) {
							ease = Ease.Linear;
						}
						pathSeq.Append(DOTween.To(
							()=>player.direction, 
							d=>player.direction = d, 
							directions[i],
							0));
						pathSeq.Append(player.transform.DOMove(
							path[i].position, 
							lengths[i]
							).SetEase (ease)
						);
					}
					FinishSequence(pathSeq, player);
					break;
				}
			//}
			pathSeq.OnComplete (() => onComplete());
		}

		private void SetupSequence(Sequence seq, PlayerBehaviour player) {
			seq.Append(DOTween.To(
				()=>player.externallyControlled, 
				d=>player.externallyControlled = d, 
				1,
				0));
		}

		private void FinishSequence(Sequence seq, PlayerBehaviour player) {
			seq.Append(DOTween.To(
				()=>player.direction, 
				d=>player.direction = d, 
				Movement.STOP,
				0));
			seq.Append(DOTween.To(
				()=>player.externallyControlled, 
				d=>player.externallyControlled = d, 
				0,
				0));

		}
	}

}
