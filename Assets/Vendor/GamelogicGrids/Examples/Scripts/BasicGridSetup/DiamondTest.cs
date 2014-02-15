//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

/**
	This example shows how to use a diamond grid.
	
	It also shows how to use transforms such as rotation 
	and scale on the map.
*/
public class DiamondTest : GLMonoBehaviour
{
	private readonly Vector2 triDimensions = new Vector2(60, 60); 
	
	public Cell cellPrefab;
	public GameObject root;
	
	private DiamondGrid<Cell> grid;
	private IMap3D<DiamondPoint> map;	
	
	public void Start()
	{
		BuildGrid();		
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{		
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			
			DiamondPoint rectPoint = map[worldPosition];
			
			if(grid.Contains(rectPoint))
			{
				grid[rectPoint].HighlightOn = !grid[rectPoint].HighlightOn;
			}
		}
	}	
	
	private void BuildGrid()
	{		
		grid = DiamondGrid<Cell>.ThinRectangle(5, 5);
		
		map = new DiamondMap(triDimensions)
			.AnchorCellMiddleCenter()			
			.Rotate(45.0f/2)
			.Scale(2)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY();
		
		foreach(DiamondPoint point in grid)
		{
			Cell cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;			
			
			//the grid is rotated; rotate the cells by the same amount
			cell.transform.RotateAroundZ(45.0f/2); 
			cell.SetColor(ExampleUtils.colors[point.GetColor4()]);		
			cell.SetText("(" + point.X + ", " + point.Y + ")");
			
			grid[point] = cell;
		}
	}
}
