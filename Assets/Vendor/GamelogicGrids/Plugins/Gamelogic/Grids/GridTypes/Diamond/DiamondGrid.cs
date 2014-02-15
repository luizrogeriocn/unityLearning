//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

namespace Gamelogic.Grids
{
	/**
		Represents a diamond grid.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Grids
	*/
	public partial class DiamondGrid<TCell> : AbstractUniformGrid<TCell, DiamondPoint>
	{
		#region Storage
		override protected ArrayPoint ArrayPointFromPoint(DiamondPoint point)
		{
			return ArrayPointFromGridPoint(point);
		}

		override protected ArrayPoint ArrayPointFromPoint(int x, int y)
		{
			return ArrayPointFromGridPoint(new DiamondPoint(x, y));
		}

		override protected DiamondPoint PointFromArrayPoint(int x, int y)
		{
			return GridPointFromArrayPoint(new ArrayPoint(x, y));
		}
		#endregion

		#region Neighbbors
		override protected void InitNeighbors()
		{
			neighborDirections = new PointList<DiamondPoint>
			{
				DiamondPoint.NorthEast,
				DiamondPoint.NorthWest,
				DiamondPoint.SouthWest,
				DiamondPoint.SouthEast
			};
		}

		public static DiamondPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new DiamondPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(DiamondPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public DiamondGrid<TCell> SetNeighborsDiagonals()
		{
			neighborDirections = new PointList<DiamondPoint>
			{
				DiamondPoint.East,
				DiamondPoint.North,
				DiamondPoint.West,
				DiamondPoint.South
			};

			return this;
		}

		public DiamondGrid<TCell> SetNeighborsMain()
		{
			neighborDirections = new PointList<DiamondPoint>
			{
				DiamondPoint.NorthEast,
				DiamondPoint.NorthWest,
				DiamondPoint.SouthWest,
				DiamondPoint.SouthEast
			};

			return this;
		}

		public DiamondGrid<TCell> SetNeighborsMainAndDiagonals()
		{
			neighborDirections = new PointList<DiamondPoint>
			{
				DiamondPoint.East,
				DiamondPoint.NorthEast,
				DiamondPoint.North,
				DiamondPoint.NorthWest,
				DiamondPoint.West,
				DiamondPoint.SouthWest,
				DiamondPoint.South,
				DiamondPoint.SouthEast
			};

			return this;
		}
		#endregion
	}
}