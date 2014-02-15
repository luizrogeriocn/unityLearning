//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		A struct that represents a point of a FlatRhombGrid.

		@immutable
		
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Points

	*/
	public partial struct FlatRhombPoint :
		IEdge<FlatHexPoint>,
		IEdge<PointyTriPoint>
	{
		#region Constants
		public const int SpliceCount = 3;

		public static readonly IList<IEnumerable<FlatHexPoint>> HexEdgeFaceDirections = new List<IEnumerable<FlatHexPoint>>
		{
			new PointList<FlatHexPoint>
			{
				new FlatHexPoint(-1, 0),
				new FlatHexPoint(0, -1)
			},

			new PointList<FlatHexPoint>
			{
				new FlatHexPoint(0, 0),
				new FlatHexPoint(-1, 0)
			},

			new PointList<FlatHexPoint>
			{
				new FlatHexPoint(0, 0),
				new FlatHexPoint(0, -1)
			}
		};

		public static readonly IList<IEnumerable<PointyTriPoint>> TriEdgeFaceDirections = new List<IEnumerable<PointyTriPoint>>
		{
			
			new PointList<PointyTriPoint>
			{
				new PointyTriPoint(0, -1, 0),
				new PointyTriPoint(0, -1, 1)
			},
			
			new PointList<PointyTriPoint>
			{
				new PointyTriPoint(0, 0, 0),
				new PointyTriPoint(0, -1, 1)
			},

			new PointList<PointyTriPoint>
			{
				new PointyTriPoint(1, -1, 0),
				new PointyTriPoint(0, -1, 1)
			},
		};
		#endregion

		#region Magnitude
		public int DistanceFrom(FlatRhombPoint other)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Colorings
		public int GetColor12()
		{
			return basePoint.GetColor2_4() + 4 * I;
		}
		#endregion

		#region Vertices and Edges
		[Experimental]
		IEnumerable<FlatHexPoint> IEdge<FlatHexPoint>.GetEdgeFaces()
		{
			var basePointCopy = BasePoint;
			return HexEdgeFaceDirections[I].Select(x => x + basePointCopy);
		}

		[Experimental]
		IEnumerable<PointyTriPoint> IEdge<PointyTriPoint>.GetEdgeFaces()
		{
			var basePointCopy = BasePoint;
			return TriEdgeFaceDirections[I].Select(x => x + basePointCopy);
		}
		#endregion
	}
}

