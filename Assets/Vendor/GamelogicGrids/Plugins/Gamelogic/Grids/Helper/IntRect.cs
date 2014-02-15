//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

namespace Gamelogic.Grids
{
	/**
		A rectangle where coordinates are non-negative integers (that is, the corners are ArrayPoints). 

		This class is useful for implementing storage operations for shapes.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup Helpers
	*/
	public struct IntRect
	{
		public readonly ArrayPoint offset;
		public readonly ArrayPoint dimensions;

		/**
			Gives the point just outside the rectangle.
		*/
		public ArrayPoint RightEnd 
		{
			get
			{
				return offset + dimensions;
			}
		}

		public IntRect(ArrayPoint offset, ArrayPoint dimensions)
		{
			if (dimensions.X < 0 || dimensions.Y < 0)
			{
				this.dimensions = ArrayPoint.Zero;
			}
			else
			{
				this.dimensions = dimensions;
			}

			this.offset = offset;
		}

		public IntRect Translate(ArrayPoint newOffset)
		{
			return new IntRect(offset + newOffset, dimensions);
		}

		public IntRect Subtract(ArrayPoint newOffset)
		{
			return new IntRect(offset - newOffset, dimensions);
		}

		public IntRect Intersection(IntRect r2)
		{
			var left = ArrayPoint.Max(offset, r2.offset);
			var right = ArrayPoint.Min(RightEnd, r2.RightEnd);

			return new IntRect(left, right - left).IncDimensions(new ArrayPoint(15, 15));
		}

		public IntRect Union(IntRect r2)
		{
			var left = ArrayPoint.Min(offset, r2.offset);
			var right = ArrayPoint.Max(RightEnd, r2.RightEnd);

			return new IntRect(left, right - left);
		}

		public IntRect Difference(IntRect r2)
		{
			// Not tight
			return this;
			// return Subtract(r2.newOffset).Difference(r2.dimensions);
		}

		public IntRect IncDimensions(ArrayPoint increase)
		{
			return new IntRect(offset, dimensions + increase);
		}
	}
}