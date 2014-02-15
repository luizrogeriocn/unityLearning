//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Gamelogic.Grids
{
	
	public partial class RectGrid<TCell>
	{
		#region Creation
		public RectGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public RectGrid(int width, int height, Func<RectPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public RectGrid(int width, int height, Func<RectPoint, bool> isInside, RectPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected RectPoint GridOrigin
		{
			get
			{
				return PointTransform(RectPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, RectPoint> CloneStructure<NewCellType>()
		{
			return new RectGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static RectOp<TCell> BeginShape()
		{
			return new RectOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<RectPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class DiamondGrid<TCell>
	{
		#region Creation
		public DiamondGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside, DiamondPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected DiamondPoint GridOrigin
		{
			get
			{
				return PointTransform(DiamondPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, DiamondPoint> CloneStructure<NewCellType>()
		{
			return new DiamondGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static DiamondOp<TCell> BeginShape()
		{
			return new DiamondOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<DiamondPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class PointyHexGrid<TCell>
	{
		#region Creation
		public PointyHexGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public PointyHexGrid(int width, int height, Func<PointyHexPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public PointyHexGrid(int width, int height, Func<PointyHexPoint, bool> isInside, PointyHexPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected PointyHexPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyHexPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, PointyHexPoint> CloneStructure<NewCellType>()
		{
			return new PointyHexGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static PointyHexOp<TCell> BeginShape()
		{
			return new PointyHexOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyHexPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class FlatHexGrid<TCell>
	{
		#region Creation
		public FlatHexGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public FlatHexGrid(int width, int height, Func<FlatHexPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public FlatHexGrid(int width, int height, Func<FlatHexPoint, bool> isInside, FlatHexPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected FlatHexPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatHexPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, FlatHexPoint> CloneStructure<NewCellType>()
		{
			return new FlatHexGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static FlatHexOp<TCell> BeginShape()
		{
			return new FlatHexOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatHexPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class PointyTriGrid<TCell>
	{
		#region Creation
		public PointyTriGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside, PointyTriPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected PointyTriPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyTriPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, PointyTriPoint> CloneStructure<NewCellType>()
		{
			return new PointyTriGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static PointyTriOp<TCell> BeginShape()
		{
			return new PointyTriOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyTriPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class FlatTriGrid<TCell>
	{
		#region Creation
		public FlatTriGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside, FlatTriPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected FlatTriPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatTriPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, FlatTriPoint> CloneStructure<NewCellType>()
		{
			return new FlatTriGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static FlatTriOp<TCell> BeginShape()
		{
			return new FlatTriOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatTriPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class PointyRhombGrid<TCell>
	{
		#region Creation
		public PointyRhombGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside, PointyRhombPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected PointyRhombPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyRhombPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, PointyRhombPoint> CloneStructure<NewCellType>()
		{
			return new PointyRhombGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static PointyRhombOp<TCell> BeginShape()
		{
			return new PointyRhombOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyRhombPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class FlatRhombGrid<TCell>
	{
		#region Creation
		public FlatRhombGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside, FlatRhombPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected FlatRhombPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatRhombPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, FlatRhombPoint> CloneStructure<NewCellType>()
		{
			return new FlatRhombGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static FlatRhombOp<TCell> BeginShape()
		{
			return new FlatRhombOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatRhombPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
	
	public partial class CairoGrid<TCell>
	{
		#region Creation
		public CairoGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside, CairoPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset))
		{}		
		#endregion

		#region Properties
		protected CairoPoint GridOrigin
		{
			get
			{
				return PointTransform(CairoPoint.Zero);
			}
		}
		#endregion

		#region Constructors
		override public IGrid<NewCellType, CairoPoint> CloneStructure<NewCellType>()
		{
			return new CairoGrid<NewCellType>(width, height, contains, PointTransform, InversePointTransform);
		}
		#endregion

		#region Shape Fluents
		public static CairoOp<TCell> BeginShape()
		{
			return new CairoOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<CairoPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}	
}
