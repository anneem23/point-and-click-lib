using System;


namespace PointAndClick.Pathfinding {

	public interface Heuristic<T> {
		int EstimateResult(T t1, T t2);
	}
}
