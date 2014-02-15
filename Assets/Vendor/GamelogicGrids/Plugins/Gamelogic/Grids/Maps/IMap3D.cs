//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

namespace Gamelogic.Grids
{
	/**
		An IMap maps world coordinates to Grid coordinates and vice versa.
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Interface
	*/
	public interface IMap3D<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/**
			Gets a world point given a grid point.
		*/
		Vector3 this[TPoint point]
		{
			get;
		}

		/**
			Gets a grid point given a world point.
		*/
		TPoint this[Vector3 point]
		{
			get;
		}

		IMap<TPoint> To2D();
	}
}