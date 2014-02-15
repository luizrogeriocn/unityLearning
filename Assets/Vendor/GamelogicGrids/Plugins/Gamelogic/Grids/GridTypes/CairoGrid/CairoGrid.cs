//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		A CiaroGrid is a grid where all the cells are pentagons (so each cell has five neighbors).
		
		See http://en.wikipedia.org/wiki/Cairo_pentagonal_tiling.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.2
		
		@ingroup Grids
	*/
	[Experimental]
	public partial class CairoGrid<TCell> : AbstractSplicedGrid<TCell, CairoPoint, PointyHexPoint>
	{
		#region Implementation
		protected override void InitNeighbors()
		{
			neighborDirections = new IEnumerable<CairoPoint>[]
			{
				new PointList<CairoPoint>
				{
					new CairoPoint(0, 0, 3),
					new CairoPoint(0, 0, 2),
					new CairoPoint(0, 0, 1),
					new CairoPoint(0, -1, 3),
					new CairoPoint(1, -1, 1),
				},

				new PointList<CairoPoint>
				{
					new CairoPoint(0, 0, 1),
					new CairoPoint(-1, 1, -1),
					new CairoPoint(-1, 0, 2),
					new CairoPoint(0, -1, 1),
					new CairoPoint(0, 0, -1),
				},

				new PointList<CairoPoint>
				{
					new CairoPoint(0, 1, -1),
					new CairoPoint(-1, 1, 1),
					new CairoPoint(0, 0, -1),
					new CairoPoint(0, 0, -2),
					new CairoPoint(0, 0, 1),
				},

				new PointList<CairoPoint>
				{
					new CairoPoint(1, 0, -2),
					new CairoPoint(0, 1, -3),
					new CairoPoint(0, 0, -1),
					new CairoPoint(0, 0, -3),
					new CairoPoint(1, -1, -1),
				}
			};
		}

		protected override CairoPoint MakePoint(PointyHexPoint basePoint, int index)
		{
			return new CairoPoint(basePoint.X, basePoint.Y, index);
		}

		protected override IGrid<TCell[], PointyHexPoint> MakeUnderlyingGrid(int width, int height)
		{
			return new PointyHexGrid<TCell[]>(width, height);
		}
		#endregion

		#region StorageInfo
		public static PointyHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return PointyHexGrid<TCell>.GridPointFromArrayPoint(point);
		}

		public static ArrayPoint ArrayPointFromGridPoint(PointyHexPoint point)
		{
			return PointyHexGrid<TCell>.ArrayPointFromGridPoint(point);
		}
		#endregion
	}
}
