//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	/**
		Represents a rectangular grid.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Grids
	*/
	[Serializable]
	public partial class RectGrid<TCell> :
		AbstractUniformGrid<TCell, RectPoint>
	{
		#region Properties
		public int Width
		{
			get
			{
				return width;
			}
		}

		public int Height
		{
			get
			{
				return height;
			}
		}
		#endregion		

		#region Neighbors
		public RectGrid<TCell> SetNeighborsMain()
		{
			neighborDirections = new PointList<RectPoint>
			{
				RectPoint.East,
				RectPoint.North,
				RectPoint.West,
				RectPoint.South
			};

			return this;
		}

		public RectGrid<TCell> SetNeighborsDiagonals()
		{
			neighborDirections = new PointList<RectPoint>
			{
				RectPoint.NorthEast,
				RectPoint.NorthWest,
				RectPoint.SouthWest,
				RectPoint.SouthEast
			};

			return this;
		}

		public RectGrid<TCell> SetNeighborsMainAndDiagonals()
		{
			neighborDirections = new PointList<RectPoint>
			{
				RectPoint.East,
				RectPoint.NorthEast,
				RectPoint.North,
				RectPoint.NorthWest,
				RectPoint.West,
				RectPoint.SouthWest,
				RectPoint.South,
				RectPoint.SouthEast
			};

			return this;
		}

		protected override void InitNeighbors()
		{
			SetNeighborsMain();
		}
		#endregion

		#region Storage
		public static ArrayPoint ArrayPointFromGridPoint(RectPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public static RectPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new RectPoint(point.X, point.Y);
		}

		//TODO do we still need these?
		override protected ArrayPoint ArrayPointFromPoint(RectPoint point)
		{
			return ArrayPointFromGridPoint(point);
		}

		override protected ArrayPoint ArrayPointFromPoint(int x, int y)
		{
			return new ArrayPoint(x, y);
		}

		override protected RectPoint PointFromArrayPoint(int x, int y)
		{
			return new RectPoint(x, y);
		}
		#endregion
	}
}