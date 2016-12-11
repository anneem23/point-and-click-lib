using System;
using UnityEngine;

namespace PointAndClick.Pathfinding {

	public class ManhattanDistance : Heuristic<Vector3> {
		public int EstimateResult(Vector3 from, Vector3 to) {
			return Mathf.Abs ((int) (to.x - from.x)) + Mathf.Abs ((int) (to.y - from.y));
		}
	}
}
