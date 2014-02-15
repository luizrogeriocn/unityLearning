//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		A rhombille grid in the flat orientation, that is, there are rhombusses with horizontal edges.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Grids
	*/
	[Serializable]
	public partial class FlatRhombGrid<TCell> : AbstractSplicedGrid<TCell, FlatRhombPoint, FlatHexPoint>
	{
		#region Implementation
		protected override void InitNeighbors()
		{
			neighborDirections = new IEnumerable<FlatRhombPoint>[]
			{
				new PointList<FlatRhombPoint>
				{
					new FlatRhombPoint(0, 0, 2),
					new FlatRhombPoint(0, 0, 1),
					new FlatRhombPoint(-1, 0, 2),
					new FlatRhombPoint(0, -1, 1)
				},

				new PointList<FlatRhombPoint>
				{
					new FlatRhombPoint(0, 0, 1),
					new FlatRhombPoint(0, 1, 2),
					new FlatRhombPoint(-1, 1, 1),				
					new FlatRhombPoint(0, 0, 2),
				},

				new PointList<FlatRhombPoint>
				{
					new FlatRhombPoint(1, 0, 1),
					new FlatRhombPoint(0, 0, 2),
					new FlatRhombPoint(0, 0, 1),
					new FlatRhombPoint(1, -1, 1)				
				}
			};
		}
		protected override FlatRhombPoint MakePoint(FlatHexPoint basePoint, int index)
		{
			return new FlatRhombPoint(basePoint.X, basePoint.Y, index);
		}

		protected override IGrid<TCell[], FlatHexPoint> MakeUnderlyingGrid(int width, int height)
		{
			return new FlatHexGrid<TCell[]>(width, height);
		}
		#endregion

		#region StorageInfo
		public static FlatHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return FlatHexGrid<TCell>.GridPointFromArrayPoint(point);
		}

		public static ArrayPoint ArrayPointFromGridPoint(FlatHexPoint point)
		{
			return FlatHexGrid<TCell>.ArrayPointFromGridPoint(point);
		}
		#endregion
	}
}
