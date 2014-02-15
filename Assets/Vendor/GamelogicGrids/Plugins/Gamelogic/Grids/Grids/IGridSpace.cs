//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		A grid space is an object that can determine whether a point is inside it our not. 
		Unlike grids, it does not contain data, and therefor there is no data at points.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Interface
	*/
	public interface IGridSpace<TPoint> : IEnumerable<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/**
			Returns whether a point is inside the grid.
		
			\implementers Use this method to control the shape of the grid.
		*/
		bool Contains(TPoint point);
	}
}