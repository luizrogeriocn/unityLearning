//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

/**
	A biasic example using the Cairo grid.
*/
public class CairoTest : GLMonoBehaviour
{
	private readonly Vector2 cellDimensions = new Vector2(128, 128);

	public SplicedCell cellPrefab;
	public GameObject root;
		
	private CairoGrid<SplicedCell> grid;
	private IMap3D<CairoPoint> map;
	
	public void Start()
	{	
		BuildGrid();		
	}
	
	public void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);            
			CairoPoint gridPoint = map[worldPosition];
			
			if(grid.Contains(gridPoint))
			{
				grid[gridPoint].HighlightOn = !grid[gridPoint].HighlightOn;
			}
		}
	}
	
	private void BuildGrid()
	{
		const int width = 2;
		const int height = 2;
		
		grid = CairoGrid<SplicedCell>.Default(width, height);
		
		map = new CairoMap(cellDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY();
		
		foreach(CairoPoint point in grid)
		{
			SplicedCell cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localPosition = worldPoint;	
			cell.transform.localScale = Vector3.one;
			
			cell.SetColor(ExampleUtils.colors[ColorMap(point.GetColor12())]);
			cell.SetText(PointToString(point));
			cell.SetOrientation(point.I);
			
			grid[point] = cell;
		}		
	}
	
	private int ColorMap(int oldColor)
	{
		return ((oldColor % 4) * 4 + oldColor / 4);
	}

	private string PointToString(CairoPoint point)
	{
		return "" + point.X + ", " + point.Y + " | " + point.I; 
	}
}
