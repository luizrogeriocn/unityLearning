using System;

namespace Gamelogic.Grids
{
	/**
		This class provides extensions that implement obsolete methods.
		
		These methods will be removed in a future version of this library.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.3

		@ingroup Scaffolding
	*/
	public static class IMapObsoleteExtensions
	{
		[Obsolete("Use AnchorCellMiddleRight instead. Will be removed in next version.")]
		public static IMap<TPoint> AnchorCellMiddelRight<TPoint>(this IMap<TPoint> map) 
			where TPoint:IGridPoint<TPoint>
		{
			return map.AnchorCellMiddleRight();
		}

		[Obsolete("Use AnchorCellMiddleLeft instead. Will be removed in next version.")]
		public static IMap<TPoint> AnchorCellMiddelLeft<TPoint>(this IMap<TPoint> map)
			where TPoint : IGridPoint<TPoint>

		{
			return map.AnchorCellMiddleLeft();
		}

		[Obsolete("Use AnchorCellMiddleCenter instead. Will be removed in next version.")]
		public static IMap<TPoint> AnchorCellMiddelCenter<TPoint>(this IMap<TPoint> map)
			where TPoint:IGridPoint<TPoint>
		{
			return map.AnchorCellMiddleCenter();
		}
	}

	/**
		This class provides extensions that implement obsolete methods.
		
		These methods will be removed in a future version of this library.

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.3

		@ingroup Scaffolding
	*/
	public static class WindowedMapObsoleteExtensions
	{
		[Obsolete("Use AlignMiddle instead. Will be removed in next version.")]
		public static IMap<TPoint> AlignMiddel<TPoint>(this WindowedMap<TPoint> map, IGridSpace<TPoint> grid)
			where TPoint: IGridPoint<TPoint>
		{
			return map.AlignMiddle(grid);
		}

		[Obsolete("Use AlignMiddleLeft instead. Will be removed in next version.")]
		public static IMap<TPoint> AlignMiddelLeft<TPoint>(WindowedMap<TPoint> map, IGridSpace<TPoint> grid)
			where TPoint : IGridPoint<TPoint>
		{
			return map.AlignMiddleLeft(grid);
		}

		[Obsolete("Use AlignMiddleCenter instead. Will be removed in next version.")]
		public static IMap<TPoint> AlignMiddelCenter<TPoint>(this WindowedMap<TPoint> map, IGridSpace<TPoint> grid)
			where TPoint : IGridPoint<TPoint>
		{
			return map.AlignMiddleCenter(grid);
		}
	}
}
