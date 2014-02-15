﻿//SplicedGrids.tt//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System;

namespace Gamelogic.Grids
{

	public partial class FlatTriGrid<TCell>
	{
		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside, PointyHexPoint offset) :
			this(width, height, isInside, x => x.Translate(offset), x => x.Translate(offset.Negate()))
		{}

		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside, Func<FlatTriPoint, FlatTriPoint> gridPointTransform, Func<FlatTriPoint, FlatTriPoint> inverseGridPointTransform) :
			base(width, height, FlatTriPoint.SpliceCount, isInside, gridPointTransform, inverseGridPointTransform)
		{}

		public static bool DefaultContains(FlatTriPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point.BasePoint);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
	}
	
	public partial class PointyTriGrid<TCell>
	{
		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside, FlatHexPoint offset) :
			this(width, height, isInside, x => x.Translate(offset), x => x.Translate(offset.Negate()))
		{}

		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside, Func<PointyTriPoint, PointyTriPoint> gridPointTransform, Func<PointyTriPoint, PointyTriPoint> inverseGridPointTransform) :
			base(width, height, PointyTriPoint.SpliceCount, isInside, gridPointTransform, inverseGridPointTransform)
		{}

		public static bool DefaultContains(PointyTriPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point.BasePoint);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
	}
	
	public partial class FlatRhombGrid<TCell>
	{
		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside, FlatHexPoint offset) :
			this(width, height, isInside, x => x.Translate(offset), x => x.Translate(offset.Negate()))
		{}

		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside, Func<FlatRhombPoint, FlatRhombPoint> gridPointTransform, Func<FlatRhombPoint, FlatRhombPoint> inverseGridPointTransform) :
			base(width, height, FlatRhombPoint.SpliceCount, isInside, gridPointTransform, inverseGridPointTransform)
		{}

		public static bool DefaultContains(FlatRhombPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point.BasePoint);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
	}
	
	public partial class PointyRhombGrid<TCell>
	{
		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside, PointyHexPoint offset) :
			this(width, height, isInside, x => x.Translate(offset), x => x.Translate(offset.Negate()))
		{}

		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside, Func<PointyRhombPoint, PointyRhombPoint> gridPointTransform, Func<PointyRhombPoint, PointyRhombPoint> inverseGridPointTransform) :
			base(width, height, PointyRhombPoint.SpliceCount, isInside, gridPointTransform, inverseGridPointTransform)
		{}

		public static bool DefaultContains(PointyRhombPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point.BasePoint);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
	}
	
	public partial class CairoGrid<TCell>
	{
		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside, PointyHexPoint offset) :
			this(width, height, isInside, x => x.Translate(offset), x => x.Translate(offset.Negate()))
		{}

		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside, Func<CairoPoint, CairoPoint> gridPointTransform, Func<CairoPoint, CairoPoint> inverseGridPointTransform) :
			base(width, height, CairoPoint.SpliceCount, isInside, gridPointTransform, inverseGridPointTransform)
		{}

		public static bool DefaultContains(CairoPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point.BasePoint);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
	}
	
}
