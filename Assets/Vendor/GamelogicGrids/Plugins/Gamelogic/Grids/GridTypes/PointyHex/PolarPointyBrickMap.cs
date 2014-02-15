//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

namespace Gamelogic.Grids
{
	/**
		This map can be used with a horizontally wrapped PointyHexGrid.

		For now, alignment does not work as with the other maps.

		Example:
		<code>
			IGrid<PointyHexPoint> grid;
			IMap3D map;

			private void BuildGrid()
			{
				grid = PointyHexGrid<TCell>.HorizontallyWrappedParallelogram(width, height);
				map = new PolarPointyBrickMap(Vector3.zero, 10, 110, newRectPoint(5, 10).To3DXY();

				foreach(var point in grid)
				{
					var cell = Instaitate(cellPrefab);
					cell.transform.localPosition = map[
				}
			}

			public void Click(Vector3 worldPoint)
			{
				var gridPoint = map[worldPoint] 
				if(grid.Contains(gridPoint))
				{
					ClickCell(grid[gridPoint]);
				}
			}		
		</code>

		@since 1.7
		@ingroup 
	*/

	[Experimental]
	public class PolarPointyBrickMap : AbstractMap<PointyHexPoint>, IPolarMap<PointyHexPoint>
	{
		#region Fields

		private readonly float sectorAngleRad;

		#endregion

		#region Properties

		public float SectorAngle
		{
			get;
			private set;
		}

		public Vector2 Center
		{
			get;
			private set;
		}

		public VectorPoint SectorsAndBands
		{
			get;
			private set;
		}

		public float InnerRadius
		{
			get;
			private set;
		}

		public float OuterRadius
		{
			get;
			private set;
		}

		#endregion

		#region Construction

		public PolarPointyBrickMap(Vector2 center, float innerRadius, float outerRadius, VectorPoint sectorsAndBands)
			: base(Vector2.one)
		{
			Center = center;
			InnerRadius = innerRadius;
			OuterRadius = outerRadius;
			SectorsAndBands = sectorsAndBands;
			sectorAngleRad = (2f*Mathf.PI)/SectorsAndBands.X;
			SectorAngle = 360f/SectorsAndBands.X;
		}

		#endregion

		#region AbstractMap Implementation

		public override PointyHexPoint RawWorldToGrid(Vector2 worldPoint)
		{
			float angleRad = Mathf.Atan2(worldPoint.y - Center.y, worldPoint.x - Center.x);

			if (angleRad < 0)
			{
				angleRad += 2*Mathf.PI;
			}

			float radius = (new Vector2(worldPoint.x - Center.x, worldPoint.y - Center.y)).magnitude;

			int n = Mathf.FloorToInt((radius - InnerRadius)/(OuterRadius - InnerRadius)*SectorsAndBands.Y);
			int m = Mathf.FloorToInt((angleRad - sectorAngleRad*n/2)/sectorAngleRad);

			return new PointyHexPoint(m, n);
		}

		public override Vector2 GridToWorld(PointyHexPoint gridPoint)
		{
			float m = gridPoint.X;
			float n = gridPoint.Y;
			float angleRad = (m/SectorsAndBands.X)*2f*Mathf.PI + (Mathf.PI/SectorsAndBands.X) + n*sectorAngleRad/2f;
			float radius = (n/SectorsAndBands.Y)*(OuterRadius - InnerRadius) + InnerRadius +
			               (OuterRadius - InnerRadius)/(2f*SectorsAndBands.Y);
			float x = radius*Mathf.Cos(angleRad) + Center.x;
			float y = radius*Mathf.Sin(angleRad) + Center.y;

			return new Vector2(x, y);
		}

		#endregion

		#region Interface

		/**
			Returns the Z angle in degrees of the given grid point.

			This can beused to rotate cells appropriately.
		*/

		public float GetStartAngleZ(PointyHexPoint gridPoint)
		{
			float m = gridPoint.X;
			float n = gridPoint.Y;
			float angle = m*SectorAngle + n*SectorAngle/2;

			return angle;
		}

		public float GetEndAngleZ(PointyHexPoint gridPoint)
		{
			float angle = GetStartAngleZ(gridPoint) + SectorAngle;

			return angle;
		}

		public float GetInnerRadius(PointyHexPoint gridPoint)
		{
			return (gridPoint.Y/(float) SectorsAndBands.Y)*(OuterRadius - InnerRadius) + InnerRadius;
		}

		public float GetOuterRadius(PointyHexPoint gridPoint)
		{
			return ((gridPoint.Y + 1)/(float) SectorsAndBands.Y)*(OuterRadius - InnerRadius) + InnerRadius;
		}

		#endregion
	}
}