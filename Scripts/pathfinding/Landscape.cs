using System;
using UnityEngine;

namespace PointAndClick.Pathfinding {

	public class Landscape {
		bool[,] terrain;
		Bounds bounds;
		double granularity;

		public Landscape (Bounds bounds, double granularity=1.0)
		{
			int width = (int)Math.Ceiling (bounds.size.x / granularity);
			int height = (int)Math.Ceiling (bounds.size.y / granularity);
			terrain = new bool[width, height];
			this.bounds = bounds;
			this.granularity = granularity;
		}

		public void AddObstacle(Vector3 at) {
			int x = xComponent (at);
			int y = yComponent (at);
			terrain [x, y ] = true;
		}

		int xComponent(Vector3 pos) {
			Vector3 relative = pos - bounds.min;
			return (int)Math.Round(relative.x / granularity);
		}

		int yComponent(Vector3 pos) {
			Vector3 relative = pos - bounds.min;
			return (int)Math.Round(relative.y / granularity);
		}

		public bool isBlocked(Vector3 pos) {
			int x = xComponent (pos);
			int y = yComponent (pos);
			if (x < 0 || x >= terrain.GetLength (0) ||
				y < 0 || y >= terrain.GetLength (1)) {
				return true;
			}
			return terrain [x, y];
		}

		public void DebugDraw(Color color, float duration=5.0f) {
			float g = (float)granularity;
			for(int x = 0; x < terrain.GetLength(0); x++) {
				for(int y = 0; y < terrain.GetLength(1); y++) {
					Vector3 center = new Vector3((float)(x * g), (float)(y * g), 0.0f) + bounds.min;
					Debug.DrawLine (center + new Vector3(-g / 2, g / 2, 0),
					                center + new Vector3(g / 2, g / 2, 0),
					                color, duration);
					Debug.DrawLine (center + new Vector3(-g / 2, -g / 2, 0),
					                center + new Vector3(g / 2, -g / 2, 0),
					                color, duration);
					Debug.DrawLine (center + new Vector3(-g / 2, g / 2, 0),
					                center + new Vector3(-g / 2, -g / 2, 0),
					                color, duration);
					Debug.DrawLine (center + new Vector3(g / 2, g / 2, 0),
					                center + new Vector3(g / 2, -g / 2, 0),
					                color, duration);
					if(isBlocked(center)) {
						Debug.DrawLine (center + new Vector3(-g / 2, -g / 2, 0),
						                center + new Vector3(g / 2, g / 2, 0),
						                color, duration);
						Debug.DrawLine (center + new Vector3(-g / 2, g / 2, 0),
						                center + new Vector3(g / 2, -g / 2, 0),
						                color, duration);

					}
				}
			}
		}
	}

}