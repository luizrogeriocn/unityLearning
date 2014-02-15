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
		A grid for pointy hexagons, that is, hexagons with two vertical edges.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Grids
	*/
	[Serializable]
	public partial class PointyHexGrid<TCell> :
		AbstractUniformGrid<TCell, PointyHexPoint>,
		IEvenGrid<TCell, PointyHexPoint, PointyHexPoint>
	{
		#region Implementation
		protected override void InitNeighbors()
		{
			neighborDirections = new PointList<PointyHexPoint>
			{
				PointyHexPoint.East,
				PointyHexPoint.NorthEast,
				PointyHexPoint.NorthWest,
				PointyHexPoint.West,
				PointyHexPoint.SouthWest,
				PointyHexPoint.SouthEast
			};
		}
		#endregion

		#region Storage
		override protected PointyHexPoint PointFromArrayPoint(int aX, int aY)
		{
			return GridPointFromArrayPoint(new ArrayPoint(aX, aY));
		}

		override protected ArrayPoint ArrayPointFromPoint(int hX, int hY)
		{
			return ArrayPointFromPoint(new PointyHexPoint(hY, hX));
		}

		override protected ArrayPoint ArrayPointFromPoint(PointyHexPoint hexPoint)
		{
			return ArrayPointFromGridPoint(hexPoint);
		}

		public static PointyHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new PointyHexPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(PointyHexPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public IEnumerable<PointyHexPoint> GetPrincipleNeighborDirections()
		{
			return neighborDirections.Take(neighborDirections.Count() / 2);
		}
		#endregion

		#region Wrapped Grids
		/**
			Returns a grid wrapped horizontally along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, PointyHexPoint> HorizontallyWrappedRectangle(int width, int height)
		{
			var grid = Rectangle(width, height);
			var wrapper = new PointyHexHorizontalRectangleWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, PointyHexPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
}
