//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Linq;
using UnityEngine;

/**
	This example shows the Lights Out game on a Cairo grid.
*/
public class LightsOutCairo: GLMonoBehaviour, IResetable
{
	private readonly Vector2 CellDimensions = new Vector2(128 + 4, 128 + 4);
	private readonly Color OffColor = ExampleUtils.colors[4];
	private readonly Color OnColor = ExampleUtils.colors[6]; 
	
	public SplicedCell cellPrefab;
	public GameObject root;
	
	private CairoGrid<Cell> grid;
	private IMap3D<CairoPoint> map;

	public void Start()
	{
		Reset();
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

		var gridPoint = map[worldPosition];

		if (grid.Contains(gridPoint))
		{
			ToggleCellAt(gridPoint);
		}
	}

	public void Reset()
	{
		root.transform.DestroyChildren();
		BuildGrid();
		InitGame();
	}

	private void BuildGrid()
	{
		grid = CairoGrid<Cell>.Default(4, 6);
		
		map = new CairoMap(CellDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach (var point in grid)
		{
			var cell = Instantiate<SplicedCell>(cellPrefab);
		
			Vector3 worldPoint = map[point];
		
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
		
			cell.SetColor(OffColor);
			cell.SetText("");
			cell.SetOrientation(point.I);
		
			grid[point] = cell;
		}
	}

	private void InitGame()
	{
		//Initialiase with random pattern
		grid.SampleRandom(9).ToList().ForEach(ToggleCellAt);		
	}	

	private void ToggleCellAt(CairoPoint gridPoint)
	{
		foreach (var point in grid.GetNeighbors(gridPoint))
		{
			grid[point].HighlightOn = !grid[point].HighlightOn;
			grid[point].SetColor(grid[point].HighlightOn ? OnColor : OffColor);
		}
	}
}