using UnityEngine;
using System.Collections.Generic;
using System;
using PointAndClick.DataStructure;

namespace PointAndClick.Pathfinding {

	public class AStarPathfinding {

		// Prioritätenwarteschlange
		private MinPQ<Node> openlist;
		private HashSet<Node> closedList;
		private Heuristic<Vector3> heuristic;
		private Landscape landscape;

		public AStarPathfinding(Landscape landscape, Heuristic<Vector3> heuristic) {
			this.openlist = new MinPQ<Node> ();
			this.closedList = new HashSet<Node>();
			this.heuristic = heuristic;
			this.landscape = landscape;
		}

		public Node findPath(Vector3 start, Vector3 end) {
			// Begin at the starting point A and add it to an “open list” of squares to be considered.
			openlist.insert (new Node(end, 0, 0, null));

			while (!openlist.isEmpty()) {
				Node node = openlist.delMin();
				//Console.WriteLine(end.Equals(node.position));
				if (samePosition(start, node.position))
					return node;

				this.closedList.Add(node);
				expandNode (node, start);
				//Console.WriteLine (this.openlist.min());
			}
			return null;
		}

		private bool samePosition(Vector3 p1, Vector3 p2){
			int x1 = (int) p1.x;
			int x2 = (int) p2.x;
			int y1 = (int) p1.y;
			int y2 = (int) p2.y;

			return (x1 == x2) && (y1 == y2);
		}

		// überprüft alle Nachfolgeknoten und fügt sie der Open List hinzu, wenn entweder
		// - der Nachfolgeknoten zum ersten Mal gefunden wird oder
		// - ein besserer Weg zu diesem Knoten gefunden wird
		private void expandNode(Node currentNode, Vector3 destination) {
			//this.openlist = new MinPQ<Node> ();
			// Look at all the reachable or walkable squares adjacent to the starting point, 
			// ignoring squares with walls, water, or other illegal terrain. Add them to the open list, too. 
			foreach (Vector3 v in getAdjacent()) {
				// F = G + H 
				// G = the movement cost to move from the starting point A to a given square on the grid, 
				//     following the path generated to get there. 
				// H = estimated movement cost to move from that given square on the grid to the final 
				//     destination, point B
				int cost = c (v);
				int h = this.heuristic.EstimateResult((currentNode.position + v), destination); //ManhattanDistance ((currentNode.position + v), destination);
				// g Wert für den neuen Weg berechnen: g Wert des Vorgängers plus
				// die Kosten der gerade benutzten Kante
				int tentative_g = currentNode.g () + cost;
				Vector3 tentative_pos = currentNode.position + v;
				Node successor = new Node (tentative_pos, tentative_g, h, currentNode);

				if (closedList.Contains(successor) || landscape.isBlocked ((tentative_pos)))
					continue;

				if (openlist.contains (successor) && tentative_g >= cost) 
					continue;
				
				this.openlist.insert (successor);	
			}
		}

		private Vector3[] getAdjacent() {
			return new Vector3[] {
								  new Vector3( 0, -1, 0),  // top
								  new Vector3(-1, 0, 0), // left
								  //new Vector3( -1, -1, 0), // top left 
								  //new Vector3( 1, -1, 0), // top right 
								  new Vector3(0, 1, 0), // bottom
								  new Vector3(1, 0, 0), // right 
								  //new Vector3( 1, 1, 0), // bottom right
								  //new Vector3( -1, 1, 0) // bottom left
								};
		}
		
		public int c(Vector3 v) {
			// movement cost to move from the starting point A to a given square on the grid, 
			// following the path generated to get there. 
			return (int) Mathf.Abs(v.x) + (int) Mathf.Abs(v.y);
		}
		
		public int ManhattanDistance(Vector3 v, Vector3 destination) {
			return Mathf.Abs ((int) (destination.x - v.x)) + Mathf.Abs ((int) (destination.y - v.y));
		}

												
	}
}