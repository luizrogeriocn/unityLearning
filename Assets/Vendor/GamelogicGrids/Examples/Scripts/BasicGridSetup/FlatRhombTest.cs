//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

public class FlatRhombTest : GLMonoBehaviour
{
	private readonly Vector2 CellDimensions = new Vector2(120, 140); 
	
	public SplicedCell cellPrefab;
	public GameObject root;
	
	private FlatRhombGrid<SplicedCell> grid;
	private IMap3D<FlatRhombPoint> map;
	
	public void Start()
	{	
		BuildGrid();
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);            
			FlatRhombPoint gridPoint = map[worldPosition];
			
			if(grid.Contains(gridPoint))
			{
				grid[gridPoint].HighlightOn = !grid[gridPoint].HighlightOn;
			}
		}
	}
	
	private void BuildGrid()
	{
		const int width = 4;
		const int height = 4;
		
		grid = FlatRhombGrid<SplicedCell>
			.BeginShape()
			.Rectangle(width, height)
			.EndShape();
		
		map = new FlatRhombMap(CellDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignBottomLeft(grid)
			.To3DXY()
				;
		
		foreach(FlatRhombPoint point in grid)
		{
			SplicedCell cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localPosition = worldPoint;	
			cell.transform.localScale = Vector3.one;
			
			cell.SetColor(ExampleUtils.colors[point.GetColor12()]);	
			cell.SetText(point.ToString());
			cell.SetOrientation(point.I);
			
			grid[point] = cell;
		}
	}
}
