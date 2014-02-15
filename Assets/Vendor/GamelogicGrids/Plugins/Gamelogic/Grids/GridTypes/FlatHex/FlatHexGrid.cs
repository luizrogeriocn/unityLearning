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
		A grid for flat hexagons, that is, hexagons with two horizontal edges.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Grids
	*/
	[Serializable]
	public partial class FlatHexGrid<TCell> : AbstractUniformGrid<TCell, FlatHexPoint>, IEvenGrid<TCell, FlatHexPoint, FlatHexPoint>
	{
		#region Neighbors
		protected override void InitNeighbors()
		{
			//Default nieghbor setup
			neighborDirections = new PointList<FlatHexPoint>
			{
				FlatHexPoint.NorthEast,
				FlatHexPoint.North,
				FlatHexPoint.NorthWest,
				FlatHexPoint.SouthWest,
				FlatHexPoint.South,
				FlatHexPoint.SouthEast
			};
		}		
		#endregion

		#region Storage
		override protected FlatHexPoint PointFromArrayPoint(int aX, int aY)
		{
			return GridPointFromArrayPoint(new ArrayPoint(aX, aY));
		}

		override protected ArrayPoint ArrayPointFromPoint(int hX, int hY)
		{
			return ArrayPointFromGridPoint(new FlatHexPoint(hX, hY));
		}

		override protected ArrayPoint ArrayPointFromPoint(FlatHexPoint hexPoint)
		{
			return ArrayPointFromGridPoint(hexPoint);
		}

		public static FlatHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new FlatHexPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(FlatHexPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public IEnumerable<FlatHexPoint> GetPrincipleNeighborDirections()
		{
			return neighborDirections.Take(neighborDirections.Count() / 2);
		}
		#endregion

		#region Wrapped Grids
		/**
			Returns a grid wrapped horizontally along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, FlatHexPoint> HorizontallyWrappedRectangle(int width, int height)
		{
			var grid = Rectangle(width, height);
			var wrapper = new FlatHexHorizontalWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, FlatHexPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
}
