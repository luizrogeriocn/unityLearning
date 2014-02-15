//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		The base class of all types of grids. 
	
		Grids are similar to 2D arrays. Elements in the grid are called _cells_, and are accessed using points.
	
			Cell cell = squareGrid[squarePoint];
	
		General algorithms are provided in Algorithms.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Interface	
	*/
	public interface IGrid<TCell, TPoint> : IGridSpace<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/**
			Accesses a cell in the given point.
		*/
		TCell this[TPoint point]
		{
			get;
			set;
		}

		/**
			Returns a grid with exactly the same structure, but potentially holding
			lements of a different type.
		
		*/
		IGrid<TNewCell, TPoint> CloneStructure<TNewCell>();

		/**
			Returns the neighbors of this point, 
			regardless of whether they are in the grid or not.
		*/
		IEnumerable<TPoint> GetAllNeighbors(TPoint point);

		/**
			A enumarable containing all the values of this grid.

			For example, the following two pieces of code do the same:

			@code
			foreach(var point in grid)
			{
				Debug.Log(grid[point]);
			}

			foreach(var value in grid.Values)
			{
				Debug.Log(value);
			}
			@endcode
		*/
		IEnumerable<TCell> Values
		{
			get;
		}

		/**
			This functions returns a large number of points around the origin.

			This is useful (when used with big enough n) to determine 
			whether a grid that is missing points is doing so becuase of
			an incorrect test function, or an incorrect storage rectangle.

			Use for debugging.

			@since 1.1
		*/
		IEnumerable<TPoint> GetLargeSet(int n);

		/**
			This method returns all points that can be contained by
			the storage rectangle for this grid.

			This is useful for debuggong shape functions.

			@since 1.1
		*/
		IEnumerable<TPoint> GetStoragePoints();
	}
}