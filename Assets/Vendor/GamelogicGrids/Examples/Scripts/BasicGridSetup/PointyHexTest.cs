//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

/**
*/
public class PointyHexTest : GLMonoBehaviour
{
	public enum Shape
	{
		Hexagon,
		Rectangle,
		FatRectangle,
		ThinRectangle,
		UpTriangle,
		DownTriangle,
		Paralallellogram,
		Diamond
	}
	
	private readonly Vector2 HexDimensions = new Vector2(74, 84);

	public Cell cellPrefab;
	public Texture2D testTexture;
	public GameObject root;
	public Shape shape;	
	
	private Shape previousShape;
	private PointyHexGrid<Cell> grid;
	private IMap3D<PointyHexPoint> map;
	private PointyHexHexagonWrapper _hexagonWrapper;

	public void Start()
	{		
		BuildGrid();
		previousShape = shape;
	}

	

	private void BuildGrid()
	{
		const int width = 3;
		const int height = 5;
		_hexagonWrapper = new PointyHexHexagonWrapper(2);
		
		switch(shape)
		{
			case Shape.Rectangle:
				grid = PointyHexGrid<Cell>.Rectangle(width, height);
			break;
			
			case Shape.FatRectangle:
				grid = PointyHexGrid<Cell>.FatRectangle(width, height);
			break;
			
			case Shape.ThinRectangle:
				grid = PointyHexGrid<Cell>.ThinRectangle(width, height);
			break;
				
			case Shape.Hexagon:
				grid = PointyHexGrid<Cell>.Hexagon(10);
			break;
			
			case Shape.UpTriangle:
				grid = PointyHexGrid<Cell>.UpTriangle(4);
			break;
			
			case Shape.DownTriangle:
				grid = PointyHexGrid<Cell>.DownTriangle(4);
			break;
			
			case Shape.Paralallellogram:
				grid = PointyHexGrid<Cell>.Parallelogram(10, 5);
			break;
			
			case Shape.Diamond:
				grid = PointyHexGrid<Cell>.Diamond(4);
			break;
		}
		
		map = new PointyHexMap(HexDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY();		

		foreach(PointyHexPoint point in grid)
		{
			Cell cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];

			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			cell.SetColor(ExampleUtils.Blend(0.7f, ExampleUtils.colors[point.GetColor3_7()], Color.white));
			
			cell.SetText(point.ToString());
			grid[point] = cell;
		}

		foreach (var point in grid)
		{
			grid[_hexagonWrapper.Wrap(point)].SetColor(ExampleUtils.colors[point.GetColor3_7()]);
		}
	}

	public void Update()
	{
		if (previousShape != shape)
		{
			DestroyGrid();
			BuildGrid();

			foreach (var point in grid)
			{
				Vector2 worldPoint = map[point];
				grid[point].transform.localPosition = worldPoint;
			}
		}

		previousShape = shape;

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint hexPoint = map[worldPosition];

			if (grid.Contains(hexPoint))
			{
				grid[hexPoint].HighlightOn = !grid[hexPoint].HighlightOn;
				grid[_hexagonWrapper.Wrap(hexPoint)].HighlightOn = !grid[_hexagonWrapper.Wrap(hexPoint)].HighlightOn;
			}
		}
	}

	private void DestroyGrid()
	{
		foreach(PointyHexPoint point in grid)
		{
			Destroy(grid[point].gameObject);
			grid[point] = null;
		}
	}
}
