//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	/**
		Represents a 2D integers point that can is used to access a cell in a Grid.
	
		@implementers GridPoint base classes must be immatable for many of the algorithms to work correctly. In particular, 
		GridPoints are used as keys in dictionaries and sets.
	
		@implementers It is also a good idea to overload the `==` and `!=` operators.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Interface
	*/
	public interface IGridPoint<TPoint> : IEquatable<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/**
			Returns whether these points are equal. 
	 	
			@implementers Also provide overloads for the `==` and `!=` operators.
			@implementers Since derrived types should be immatable, 
			two points should be equal when X and Y for the two points are equal.
		*/
		//bool Equals(object other);

		int GetHashCode();

		/**
			@returns a string representation of this GridPoint. 
		*/
		string ToString();

		/**
		The lattice distance between two points.
		
		@implementers Two points should have a distance of 1 if and only if they are neighbors.
	*/
		int DistanceFrom(TPoint other);
	}
}