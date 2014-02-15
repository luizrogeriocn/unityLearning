//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//


namespace Gamelogic.Grids
{
	/**
		Indicates that a grid supports an edge grid.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.1
	*/
	public interface ISupportsEdgeGrid<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/**
			Makes a grid that corresponds to the edges of this grid.

			If point is inside this grid, then all of point.GetEdges() 
			are in the grid returned by this method.
		*/
		IGrid<TNewCell, TPoint> MakeEdgeGrid<TNewCell>();
	}
}