//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using UnityEngine;

public class SuperGridExample : GLMonoBehaviour
{
	public Cell cellPrefab;
	public Cell pathPrefab;
	public GameObject gridRoot;
	public GameObject pathRoot;
	public InspectableVectorPoint bigSize; 
	public InspectableVectorPoint smallSize;
	public Vector2 cellDimensions;

	private IMap3D<RectPoint> bigMap;
	private IMap3D<RectPoint> smallMap;
	private RectPoint bigDimensions;
	private RectPoint smallDimensions;
	private SuperRectGrid<Cell> grid;
	private int cellSelectionState = 0;
	private RectPoint point0;
	private RectPoint point1;

	public void Start()
	{
		bigDimensions = bigSize.GetRectPoint();
		smallDimensions = smallSize.GetRectPoint();

		Reset();
	}

	public void Reset()
	{
		BuildGrid();
	}

	private void BuildGrid()
	{
		grid = new SuperRectGrid<Cell>(bigDimensions, smallDimensions);

		var bigCellDimensions = new Vector2(cellDimensions.x*smallDimensions.X, cellDimensions.y*smallDimensions.Y);

		bigMap = new RectMap(bigCellDimensions * 1.05f)
			.AnchorCellBottomLeft()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid.BaseGrid) //pass in the base grid
			.Scale(1.5f)
			.To3DXY();

		smallMap = new RectMap(cellDimensions)
			.Scale(1.05f)
			.To3DXY();

		foreach (var bigPoint in grid.BaseGrid)
		{
			var smallGrid = grid.GetSmallGrid(bigPoint);

			foreach (var smallPoint in smallGrid)
			{
				Cell cell = Instantiate(cellPrefab);
				cell.transform.parent = gridRoot.transform;
				cell.transform.localScale = Vector3.one;
				cell.transform.localPosition = bigMap[bigPoint] + smallMap[smallPoint];

				var colorIndex = bigPoint.GetColor1_2()*2 + smallPoint.GetColor1_2();

				cell.SetColor(ExampleUtils.colors[colorIndex]);
				cell.SetText("");
				grid[bigPoint, smallPoint] = cell;
			}
		}
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(gridRoot, Input.mousePosition);
			var bigPoint = bigMap[worldPosition];

			if (grid.BaseGrid.Contains(bigPoint))
			{
				var bigWorldPosition = bigMap[bigPoint];
				var smallWorldPosition = worldPosition - bigWorldPosition;
				var smallPoint = smallMap[smallWorldPosition];

				if (grid.GetSmallGrid(bigPoint).Contains(smallPoint))
				{
					var cell = grid[bigPoint, smallPoint];
					cell.HighlightOn = !cell.HighlightOn;
				}
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(gridRoot, Input.mousePosition);
			var bigPoint = bigMap[worldPosition];

			if (grid.BaseGrid.Contains(bigPoint))
			{
				var bigWorldPosition = bigMap[bigPoint];
				var smallWorldPosition = worldPosition - bigWorldPosition;
				var smallPoint = smallMap[smallWorldPosition];

				if (grid.GetSmallGrid(bigPoint).Contains(smallPoint))
				{
					var point = grid.CombinePoints(bigPoint, smallPoint);

					if (cellSelectionState == 0)
					{
						SelectFirstPoint(point);
					}
					else
					{
						SelectLastPoint(point);
					}
				}
			}
		}
	}

	private void SelectLastPoint(RectPoint point)
	{
		cellSelectionState = 0;
		point1 = point;

		var path = Algorithms.AStar(grid, point0, point1, (p, q) => p.DistanceFrom(q), cell => !cell.HighlightOn);

		foreach (var pathPoint in path.ButFirst())
		{
			var bigPathPoint = grid.GetBigPoint(pathPoint);
			var smallPathPoint = grid.GetSmallPoint(pathPoint);
			var pathMarker = Instantiate(pathPrefab);

			pathMarker.SetText("");
			pathMarker.transform.parent = pathRoot.transform;
			pathMarker.transform.localScale = Vector3.one;
			pathMarker.transform.localPosition = bigMap[bigPathPoint] + smallMap[smallPathPoint] - Vector3.forward;
		}
	}

	private void SelectFirstPoint(RectPoint point)
	{
		cellSelectionState = 1;

		point0 = point;
		pathRoot.transform.DestroyChildren();

		var bigPathPoint = grid.GetBigPoint(point);
		var smallPathPoint = grid.GetSmallPoint(point);
		var pathMarker = Instantiate(pathPrefab);
		pathMarker.SetText("");

		pathMarker.transform.parent = pathRoot.transform;
		pathMarker.transform.localScale = Vector3.one;
		pathMarker.transform.localPosition = bigMap[bigPathPoint] + smallMap[smallPathPoint] - Vector3.forward;
	}
}
