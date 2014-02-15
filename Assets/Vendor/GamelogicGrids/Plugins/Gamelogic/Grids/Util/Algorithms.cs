//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids
{
	/**
		This class provide generic functions for common grid operations, such as finding a 
		shortest path or conneced shapes.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Utilities
	*/
	public static partial class Algorithms
	{
		#region Graph Algorithms
		public static bool IsConnected<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			IEnumerable<TPoint> collection,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : IGridPoint<TPoint>
		{
			//What if collection is empty?

			var openSet = new HashSet<TPoint>(new PointComparer<TPoint>());
			var closedSet = new HashSet<TPoint>(new PointComparer<TPoint>());

			openSet.Add(collection.First());

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				openSet.Remove(current);
				closedSet.Add(current);

				//Adds all connected neighbors that 
				//are in the grid and in the collection
				var connectedNeighbors = from neighbor in grid.GetNeighbors(current)
										 where !closedSet.Contains(neighbor)
											 && isNeighborsConnected(current, neighbor)
											 && collection.Contains(neighbor)
										 select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return collection.All(closedSet.Contains);
		}

		public static bool IsConnected<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			TPoint point1,
			TPoint point2,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : IGridPoint<TPoint>
		{
			var openSet = new HashSet<TPoint>(new PointComparer<TPoint>()) { point1 };
			var closedSet = new HashSet<TPoint>(new PointComparer<TPoint>());

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				if (current.Equals(point2))
				{
					return true;
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var connectedNeighbors =
					from neighbor in grid.GetNeighbors(current)
					where !closedSet.Contains(neighbor) && isNeighborsConnected(current, neighbor)
					select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return false;
		}

		public static HashSet<TPoint> GetConnectedSet<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			TPoint point,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : IGridPoint<TPoint>
		{
			var openSet = new HashSet<TPoint>(new PointComparer<TPoint>()) { point };
			var closedSet = new HashSet<TPoint>(new PointComparer<TPoint>());

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				openSet.Remove(current);
				closedSet.Add(current);

				var connectedNeighbors =
					from neighbor in grid.GetNeighbors(current)
					where !closedSet.Contains(neighbor) && isNeighborsConnected(current, neighbor)
					select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return closedSet;
		}

		/**
			Find the shortest path between a start and goal node.
		
			This method uses the distance function as the cost 
			heuristic, and it is assumed that all cells in the 
			grid are accessible.
		
			\param HeuristicCostEstimate A function that returns
			an heuristic cost of reaching one node from another.
		
			@returns The list of nodes on the path in order. If no 
			path is possible, null is returned. 
		*/
		public static IEnumerable<TPoint> AStar<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			TPoint start,
			TPoint goal)

			where TPoint : IGridPoint<TPoint>
		{
			return AStar(grid, start, goal, (p1, p2) => p1.DistanceFrom(p2), x => true);
		}

		/** 
			Find the shortest path between a start and goal node.
		
			The distance between nodes are used as the actual cost 
			between nodes.
			
			\param HeuristicCostEstimate A function that returns
			an heuristic cost of reaching one node from another.
			The heuristic cost estimate must be monotonic.
		
			@returns The list of nodes on the path in order. If no 
			path is possible, null is returned. 	
		*/

		public static IEnumerable<TPoint> AStar<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			TPoint start,
			TPoint goal,
			Func<TPoint, TPoint, float> heuristicCostEstimate,
			Func<TCell, bool> isAccessible)

			where TPoint : IGridPoint<TPoint>
		{
			return AStar(
				grid,
				start,
				goal,
				heuristicCostEstimate,
				isAccessible,
				(p, q) => p.DistanceFrom(q));
		}

		/** 
			Find the shortest path between a start and goal node.
		
			\param heuristicCostEstimate A function that returns
			an heuristic cost of reaching one node from another.
			The heuristic cost estimate must be monotonic.

			\param cost The actual cost of reaching one node from 
			another.
		
			@returns The list of nodes on the path in order. If no 
			path is possible, null is returned. 	

			@since 1.7
		*/
		public static IEnumerable<TPoint> AStar<TCell, TPoint>(
			IGrid<TCell, TPoint> grid,
			TPoint start,
			TPoint goal,
			Func<TPoint, TPoint, float> heuristicCostEstimate,
			Func<TCell, bool> isAccessible,
			Func<TPoint, TPoint, float> cost)

			where TPoint : IGridPoint<TPoint>
		{
			var closedSet = new PointList<TPoint>();

			// The set of tentative nodes to be evaluated
			var openSet = new PointList<TPoint> { start };

			// The map of navigated nodes.
			var cameFrom = new Dictionary<TPoint, TPoint>(new PointComparer<TPoint>());

			// Cost from start along best known path.
			var gScore = new Dictionary<TPoint, float>(new PointComparer<TPoint>());
			gScore[start] = 0;

			// Estimated total cost from start to goal through y.
			var fScore = new Dictionary<TPoint, float>(new PointComparer<TPoint>());
			fScore[start] = gScore[start] + heuristicCostEstimate(start, goal);

			while (!openSet.IsEmpty())
			{
				var current = FindNodeWithLowestScore(openSet, fScore);

				if (current.Equals(goal))
				{
					return ReconstructPath(cameFrom, goal);
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var currentNodeNeighbors = grid.GetNeighbors(current);

				var accessibleNeighbors = from neighbor in currentNodeNeighbors
										  where isAccessible(grid[neighbor])
										  select neighbor;

				foreach (var neighbor in accessibleNeighbors)
				{
					var tentativeGScore = gScore[current] + cost(current, neighbor);

					if (closedSet.Contains(neighbor))
					{
						if (tentativeGScore >= gScore[neighbor])
						{
							continue;
						}
					}

					if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
					{
						cameFrom[neighbor] = current;
						gScore[neighbor] = tentativeGScore;
						fScore[neighbor] = gScore[neighbor] + heuristicCostEstimate(neighbor, goal);

						if (!openSet.Contains(neighbor))
						{
							openSet.Add(neighbor);
						}
					}
				}
			}

			return null;
		}


		#endregion

		#region Lines

		/**
		Returns a list containing lines connected to the given points. A line is a list of points.
		Only returns correct results for square or hex grids.

	 	\param IsNeighborsConnected a functions that returns true or false, depending on whether
	 	two points can be considered connected when they are neighbors. For example, if you want
	 	rays of points that refer to cells of the same color, you can pass in a functions that
	 	compares the colours of cells.

	 	@code
	 	private bool IsSameColour(point1, point2)
	 	{
	 	   return grid[point1].Color == grid[point2].Color;
	 	}

	 	private SomeMethod()
	 	{
	 		...
	 		var rays = GetConnectedRays<ColourCell, PointyHexPoint, PointyHexNeighborIndex>(
				grid, point, IsSameColour);
			...
		}
	 	@endcode

	 	You can of course also use a lambda expression, like this:

		@code
	 	//The following code returns all lines that radiate from the given point,

		GetConnectedRays<ColourCell, PointyHexPoint, PointyHexNeighborIndex>(
			grid, point, (x, y) => grid[x].Color == grid[y].Color);

		@endcode
	*/
		public static IEnumerable<IEnumerable<TPoint>> GetConnectedRays<TCell, TPoint>(
			AbstractUniformGrid<TCell, TPoint> grid,
			TPoint point,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : IVectorPoint<TPoint>, IGridPoint<TPoint>
		{
			var lines = new List<IEnumerable<TPoint>>();

			foreach (var direction in grid.GetNeighborDirections())
			{
				var line = new PointList<TPoint>();

				var edge = point;

				while (grid.Contains(edge) && isNeighborsConnected(point, edge))
				{
					line.Add(edge);
					edge = edge.MoveBy(direction);
				}

				if (line.Count > 1)
				{
					lines.Add(line);
				}
			}

			return lines;
		}

		/**
			Gets the longest of the rays connected to this cell.

			@see GetConnectedRays
		*/
		public static IEnumerable<TPoint> GetLongestConnectedRay<TCell, TPoint>(
			AbstractUniformGrid<TCell, TPoint> grid,
			TPoint point,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : IVectorPoint<TPoint>, IGridPoint<TPoint>
		{
			var rays = GetConnectedRays(grid, point, isNeighborsConnected);

			return GetBiggestShape(rays);
		}

		/**
			Gets the longest line of connected points that contains this point.

			@see GetConnectedRays
		*/
		public static IEnumerable<IEnumerable<TPoint>> GetConnectedLines<TCell, TPoint, TBasePoint>(
			IEvenGrid<TCell, TPoint, TBasePoint> grid,
			TPoint point,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : ISplicedVectorPoint<TPoint, TBasePoint>, IGridPoint<TPoint>
			where TBasePoint : IVectorPoint<TBasePoint>, IGridPoint<TBasePoint>
		{
			var lines = new List<IEnumerable<TPoint>>();

			foreach (var direction in grid.GetPrincipleNeighborDirections())
			{
				var line = new PointList<TPoint>();
				var edge = point;

				//go forwards
				while (grid.Contains(edge) && isNeighborsConnected(point, edge))
				{
					edge = edge.MoveBy(direction);
				}

				var oppositeDirection = direction.Negate();
				//TPoint oppositeNeighbor = point.MoveBy(direction.Negate());
				edge = edge.MoveBy(oppositeDirection);

				//go backwards
				while (grid.Contains(edge) && isNeighborsConnected(point, edge))
				{
					line.Add(edge);
					edge = edge.MoveBy(oppositeDirection);
				}

				if (line.Count > 1)
				{
					lines.Add(line);
				}
			}

			return lines;
		}

		/**
		
		*/
		public static IEnumerable<TPoint> GetLongestConnected<TCell, TPoint, TBasePoint>(
			IEvenGrid<TCell, TPoint, TBasePoint> grid,
			TPoint point,
			Func<TPoint, TPoint, bool> isNeighborsConnected)

			where TPoint : ISplicedVectorPoint<TPoint, TBasePoint>, IGridPoint<TPoint>
			where TBasePoint : IVectorPoint<TBasePoint>, IGridPoint<TBasePoint>
		{
			var lines = GetConnectedLines(grid, point, isNeighborsConnected);

			return GetBiggestShape(lines);
		}
		#endregion

		#region Shapes (Collections of points)
		public static IEnumerable<TPoint> GetBiggestShape<TPoint>(
			IEnumerable<IEnumerable<TPoint>> shapes)

			where TPoint : IGridPoint<TPoint>
		{
			return shapes.MaxBy(x => x.Count());
		}

		public static bool Contains<TPoint>(IEnumerable<TPoint> bigShape, IEnumerable<TPoint> smallShape)
			where TPoint : IGridPoint<TPoint>
		{
			return smallShape.All(bigShape.Contains);
		}

		public static bool IsEquivalent<TPoint>(IEnumerable<TPoint> shape1, IEnumerable<TPoint> shape2)
			where TPoint : IGridPoint<TPoint>
		{
			if (ReferenceEquals(shape1, shape2))
			{
				return true;
			}

			return Contains(shape1, shape2) && Contains(shape2, shape1);
		}

		public static IEnumerable<TPoint> TransformShape<TPoint>(
			IEnumerable<TPoint> shape,
			Func<TPoint, TPoint> pointTransformation)

			where TPoint : IGridPoint<TPoint>
		{
			return from point in shape
				   select pointTransformation(point);
		}

		public static bool IsEquivalentUnderTranslation<TPoint>(
			IEnumerable<TPoint> shape1,
			IEnumerable<TPoint> shape2,
			Func<IEnumerable<TPoint>, IEnumerable<TPoint>> toCanonicalPosition)

			where TPoint : IGridPoint<TPoint>
		{
			return IsEquivalent(
				toCanonicalPosition(shape1),
				toCanonicalPosition(shape2));
		}

		public static bool IsEquivalentUnderTransformsAndTranslation<TPoint>(
			IEnumerable<TPoint> shape1,
			IEnumerable<TPoint> shape2,
			IEnumerable<Func<TPoint, TPoint>> pointTransformations,
			Func<IEnumerable<TPoint>, IEnumerable<TPoint>> toCanonicalPosition)

			where TPoint : IGridPoint<TPoint>
		{
			var cannicalShape1 = toCanonicalPosition(shape1);
			var cannicalShape2 = toCanonicalPosition(shape2);

			if (IsEquivalent<TPoint>(cannicalShape1, cannicalShape2))
			{
				return true;
			}

			foreach (var PointTransformation in pointTransformations)
			{
				cannicalShape2 = toCanonicalPosition(TransformShape<TPoint>(shape2, PointTransformation));

				if (IsEquivalent<TPoint>(cannicalShape1, cannicalShape2))
				{
					return true;
				}
			}

			return false;
		}		
		#endregion		

		#region FilterType

		public static TResultGrid
			AggregateNeighborhood<TCell, TPoint, TResultGrid, TResultCell>(
				IGrid<TCell, TPoint> grid,
				Func<TPoint, IEnumerable<TPoint>, TResultCell> aggregator)

			where TPoint : IGridPoint<TPoint>
			where TResultGrid : IGrid<TResultCell, TPoint>
		{
			var newGrid = (TResultGrid)grid.CloneStructure<TResultCell>();

			foreach (var point in newGrid)
			{
				newGrid[point] = aggregator(point, newGrid.GetNeighbors(point));
			}

			return newGrid;
		}
		
		public static void
			AggregateNeighborhood<TCell, TPoint>(
				IGrid<TCell, TPoint> grid,
				Func<TPoint, IEnumerable<TPoint>, TCell> aggregator)

			where TPoint : IGridPoint<TPoint>
		{
			var newGrid = grid.CloneStructure<TCell>();

			foreach (var point in newGrid)
			{
				newGrid[point] = aggregator(point, grid.GetNeighbors(point));
			}
			
			foreach (var point in grid)
			{
				grid[point] = newGrid[point];
			}
		}

		#endregion

		#region Helpers
		private static TPoint FindNodeWithLowestScore<TPoint>(IEnumerable<TPoint> list, IDictionary<TPoint, float> score)
		{
			return list.Aggregate((item, minObject) => score[item] < score[minObject] ? item : minObject);
		}
		
		//TODO remove construcctions of list!
		private static IList<TPoint> ReconstructPath<TPoint>(
			Dictionary<TPoint, TPoint> cameFrom,
			TPoint currentNode)

			where TPoint : IGridPoint<TPoint>
		{
			IList<TPoint> path = new PointList<TPoint>();

			ReconstructPath(cameFrom, currentNode, path);

			return path;
		}

		private static void ReconstructPath<TPoint>(Dictionary<TPoint, TPoint> cameFrom, TPoint currentNode, IList<TPoint> path)

			where TPoint : IGridPoint<TPoint>
		{
			if (cameFrom.ContainsKey(currentNode))
			{
				ReconstructPath(cameFrom, cameFrom[currentNode], path);
			}

			path.Add(currentNode);

			return;
		}
		#endregion
	}
}