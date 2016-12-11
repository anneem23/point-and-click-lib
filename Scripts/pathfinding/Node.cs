using UnityEngine;
using System;
using System.Collections;

namespace PointAndClick.Pathfinding {

	public class Node : IComparable<Node> {

		private Node parent;
		public Vector3 position;

		private int G;
		private int H;
		private int pathcost;

		public Node(Vector3 position, int g, int h, Node parent) {
			this.position = position;
			this.G = g;
			this.H = h;
			this.pathcost = G + H;
			this.parent = parent;
		}

		public Node Parent() {
			return this.parent;
		}

		public int g() {
			return this.G;
		}

		public int PathCost() {
			return this.pathcost;
		}



		public int CompareTo(object o2)
		{
			if (o2 == null) 
				return 1;

			int retval = 0;
			if (o2 is Node)
			{
				Node that = (Node) o2;
				if (this.PathCost() < that.PathCost()) retval = -1;
				if (this.PathCost() > that.PathCost()) retval = 1;
			}
			else
			{
				throw new Exception("ValueComparator: Illegal arguments!");
			}
			return (retval);
		}

		 public int CompareTo(Node that)
		{
			int retval = 0;
			if (that == null) retval = 1;
			if (this.PathCost() < that.PathCost()) retval = -1;
			if (this.PathCost() > that.PathCost()) retval = 1;
			return retval;
		}

		
		public override string ToString()
		{
			return "Node[position=" + position + ", pathcost=" + PathCost() + ", parent=" + parent + "]";
		}



	}
}