// ------------------------------------------------------------------------------
//  Standard implementation of a GraphNode taken from here:
//  http://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace PointAndClick.DataStructure {
	
	public class GraphNode<T> : Node<T> {
		private List<int> costs;
		
		public GraphNode() : base() { }
		public GraphNode(T value) : base(value) { }
		public GraphNode(T value, NodeList<T> neighbors) : base(value, neighbors) { }
		
		new public NodeList<T> Neighbors {
			get {
				if (base.Neighbors == null)
					base.Neighbors = new NodeList<T>();
				
				return base.Neighbors;
			}            
		}
		
		public List<int> Costs {
			get {
				if (costs == null)
					costs = new List<int>();
				
				return costs;
			}
		}
	}
}
