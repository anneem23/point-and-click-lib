using System;
using UnityEngine;


namespace PointAndClick.Pathfinding {

	public class EuclidianDistance : Heuristic<Vector3> {

		public int EstimateResult (Vector3 from, Vector3 to)
		{
			return (int) Mathf.Abs(Mathf.Sqrt (Mathf.Pow((to.x - from.x), 2) + Mathf.Pow((to.y - from.y), 2)));
		}
	}

}
