//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	/**
		A coordinate for a cell in a CiaroGrid. The base point is a pointy hex point, and indices run from 0 to 3.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.2
		
		@ingroup Points
	*/
	[Experimental]
	public partial struct CairoPoint : 
		ISplicedPoint <CairoPoint, PointyHexPoint>
	{
		#region Constants
		public const int SpliceCount = 4;
		#endregion

		#region Properties
		/**
			This is a redundant coordinate that is useful for certain algorithms and calculation.
			The coordinates of a triangle satsify this identity: X + Y + Z + I = 0.
		*/
		public int Z
		{
			get
			{
				return (BasePoint.Z - I);
			}
		}
		#endregion

		#region Magnitude
		public int DistanceFrom(CairoPoint other)
		{
			throw new NotImplementedException();
		}
		#endregion
		
		#region Colorings
		public int GetColor12()
		{
			return (BasePoint.GetColor1_3 () * 4 + I);
		}
		#endregion
	}
}
