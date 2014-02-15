//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

namespace Gamelogic.Grids
{
	/**
		This class provides extensions for integers.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Helpers
	*/
	static class IntExtensions
	{
		public static bool InHalfOpenInterval(this int n, int closedBottom, int openTop)
		{
			return closedBottom <= n && n < openTop;
		}
	}
}