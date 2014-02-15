//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Linq;
using UnityEngine;

public class LightsOutTri: GLMonoBehaviour
{
	private readonly Vector2 cellDimensions = new Vector2(80, 69);
	private readonly Color offColor = ExampleUtils.colors[4];
	private readonly Color onColor = ExampleUtils.colors[6];
	
	public SplicedCell cellPrefab;
	public GameObject root;
	
	private FlatTriGrid<Cell> grid;
	private IMap3D<FlatTriPoint> map;

	public void Start()
	{
		grid = FlatTriGrid<Cell>.Star(3);

		map = new FlatTriMap(cellDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();

		foreach (FlatTriPoint point in grid)
		{
			var cell = Instantiate<SplicedCell>(cellPrefab);

			Vector3 worldPoint = map[point];

			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;

			cell.SetColor(offColor);
			cell.SetText("");
			cell.SetOrientation(point.I);

			grid[point] = cell;
		}

		InitGame();
	}

	public void InitGame()
	{
		//Initialiase with random pattern
		grid.SampleRandom(9).ToList().ForEach(ToggleCellAt);		
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ProcessClick();
		}
	}

	private void ProcessClick()
	{
		Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);

		FlatTriPoint gridPoint = map[worldPosition];

		if (grid.Contains(gridPoint))
		{
			ToggleCellAt(gridPoint);
		}
	}

	private void ToggleCellAt(FlatTriPoint gridPoint)
	{
		foreach (FlatTriPoint point in grid.GetNeighbors(gridPoint))
		{
			grid[point].HighlightOn = !grid[point].HighlightOn;
			grid[point].SetColor(grid[point].HighlightOn ? onColor : offColor);
		}
	}
}