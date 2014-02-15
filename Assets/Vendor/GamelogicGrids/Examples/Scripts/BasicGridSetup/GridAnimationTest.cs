//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System;
using UnityEngine;

/**
	This shows one way of animating a grid using a map.
*/
public class GridAnimationTest : GLMonoBehaviour
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
	
	private readonly Vector2 hexDimensions = new Vector2(74, 64);
	
	public Cell cellPrefab;
	
	public GameObject root;
	public Shape shape;	
	
	private Shape previousShape;

	private PointyHexGrid<Cell> grid;
	private IMap3D<PointyHexPoint> map;

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
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				grid[hexPoint].HighlightOn = !grid[hexPoint].HighlightOn;
			}
		}	
		
		foreach(var point in grid)
		{
			Vector2 worldPoint = map[point];
			grid[point].transform.localPosition = worldPoint;
		}
	}
	
	private void BuildGrid()
	{
		const int width = 3;
		const int height = 5;
		
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
				grid = PointyHexGrid<Cell>.Hexagon(3);
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

		Func<PointyHexPoint, PointyHexPoint> gridPointTransform = point => point.ScaleUp(2).Rotate60();
		Func<PointyHexPoint, PointyHexPoint> inverseGridPointTransform = point => point.Rotate300().ScaleDown(2);

		grid.SetGridPointTransforms(gridPointTransform, inverseGridPointTransform);
		
		map = new PointyHexMap(hexDimensions)
			.SetGridPointTransforms(gridPointTransform, inverseGridPointTransform)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			
			.Animate((x, t) => x.Rotate(45 + 0*t), (x, t) => x.Rotate(-45 + 0*t))
			.Animate((x, t) => x + new Vector2(75 * Mathf.Sin(t/5.0f), 0),
					 (x, t) => x - new Vector2(75 * Mathf.Sin(t/5.0f), 0))
			.Animate((x, t) => x * (1.5f + 0.5f * Mathf.Sin(t*7)),
					 (x, t) => x / (1.5f + 0.5f * Mathf.Sin(t * 7))) 
				.To3DXY();

		

		foreach(PointyHexPoint point in grid)
		{
			var cell = Instantiate<Cell>(cellPrefab);
			Vector3 worldPoint = map[point];
			
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
		foreach(PointyHexPoint point in grid)
		{
			Destroy(grid[point].gameObject);
			grid[point] = null;
		}
	}
}
