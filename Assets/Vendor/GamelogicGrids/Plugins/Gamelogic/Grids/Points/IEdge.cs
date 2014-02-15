//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/**
		A point type implements this interface if that point type can be the edge grid of TPoint.

		For example, FlatRhombPoints are the points of the edge grid for FlatHexPoints, and hence
		FlatRombPoint implements IEdge<FlatHexPoint>.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.1

		(This class replaces IEdgeAnchor).

		@ingroup Interface
	*/

	public interface IEdge<TPoint>
	{
		/**
			Get the coordinates of the faces that corresponds to this point treated as an edge.
		*/
		IEnumerable<TPoint> GetEdgeFaces();
	}
}