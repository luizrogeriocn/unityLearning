//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

/**
	This example shows you how to use a brick mapper with a hex grid.
	
	(You can set the shape in the inspector dynamically).
*/
public class FlatBrickTest : GLMonoBehaviour
{
	public enum Shape
	{
		Hexagon,
		Rectangle,
		FatRectangle,
		ThinRectangle,
		LeftTriangle,
		RightTriangle,
		Paralallellogram,
		Diamond
	}

	private readonly Vector2 HexDimensions = new Vector2(119 + 4, 44 + 4);	

	public Cell cellPrefab;	
	public Texture2D testTexture;
	public Shape shape;	
	public GameObject root;
	
	private Shape previousShape;
	
	private FlatHexGrid<Cell> grid;
	private IMap3D<FlatHexPoint> map;
	
	public void Start()
	{
		BuildGrid();
		
		previousShape = shape;
	}
	
	public void Update()
	{
		if(previousShape != shape)
		{
			DestroyGrid();
			BuildGrid();			
		}
		
		previousShape = shape;
		
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;			
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, mousePosition);
			FlatHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				grid[hexPoint].HighlightOn = !grid[hexPoint].HighlightOn;
			}
		}		
	}
	
	private void BuildGrid()
	{
		const int width = 7;
		const int height = 9;
		
		switch(shape)
		{
			case Shape.Rectangle:
				grid = FlatHexGrid<Cell>.Rectangle(width, height);
			break;
			
			case Shape.FatRectangle:
				grid = FlatHexGrid<Cell>.FatRectangle(width, height);
			break;
			
			case Shape.ThinRectangle:
				grid = FlatHexGrid<Cell>.ThinRectangle(width, height);
			break;
				
			case Shape.Hexagon:
				grid = FlatHexGrid<Cell>.Hexagon(4);
			break;
			
			case Shape.LeftTriangle:
				grid = FlatHexGrid<Cell>.LeftTriangle(4);
			break;
			
			case Shape.RightTriangle:
				grid = FlatHexGrid<Cell>.RightTriangle(4);
			break;
			
			case Shape.Paralallellogram:
				grid = FlatHexGrid<Cell>.Parallelogram(width, height);
			break;
			
			case Shape.Diamond:
				grid = FlatHexGrid<Cell>.Diamond(5);
			break;
		}

		

		map = new FlatBrickMap(HexDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY()
				;
		
		foreach(FlatHexPoint point in grid)
		{
			Cell cell = Instantiate(cellPrefab);
			Vector2 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			
			cell.SetColor(ExampleUtils.colors[point.GetColor3_7()]);
			cell.SetText(point.ToString());
			
			grid[point] = cell;
		}
	}
	
	private void DestroyGrid()
	{	
		foreach(FlatHexPoint point in grid)
		{
			Destroy(grid[point].gameObject);
			grid[point] = null;
		}
	}
}
