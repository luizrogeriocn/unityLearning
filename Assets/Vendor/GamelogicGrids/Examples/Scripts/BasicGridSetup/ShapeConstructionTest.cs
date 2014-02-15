//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

/*
	This example shows how to build add your own shape functions, and how to define
	shapes in terms of others.
*/

/**
	Extensions for PointyTriOp that defines three new shapes.
*/
public static class PointyTriOpExtensions
{
	public static PointyTriShapeInfo<TCell> Ring<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()	//If you do not use begin group and Endgroup
							// the shapes will behave unexpectedly when combined
						 	// with other shapes.
				.Hexagon(4)
				.Translate(-2, -2)
				.Difference()
				.Hexagon(2)
				.Translate(4, -1)
				.Intersection()
				.Star(3)
				.Translate(0, 3)
			.EndGroup(op);
	}
		
	public static PointyTriShapeInfo<TCell> Chain<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
			.EndGroup(op);
	}
	
	public static PointyTriShapeInfo<TCell> ChainMail<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
			.EndGroup(op);
	}
}

public class ShapeConstructionTest : GLMonoBehaviour
{
	private readonly Vector2 triDimensions = new Vector2(69, 80);

	public SplicedCell cellPrefab;
	public Texture2D testTexture;
	public GameObject root;

	private PointyTriGrid<SplicedCell> grid;
	private IMap3D<PointyTriPoint> map;
	
	public void Start()
	{
		BuildGrid();
	}
	
	public void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;			
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, mousePosition);            
			PointyTriPoint triPoint = map[worldPosition];
			
			if(grid.Contains(triPoint))
			{
				grid[triPoint].HighlightOn = !grid[triPoint].HighlightOn;
			}
		}
	}
	
	private void BuildGrid()
	{
		grid = PointyTriGrid<SplicedCell>
			.BeginShape()
				.ChainMail()
				//You can now chain the newly defined method to BeginShape
			.EndShape();
		
		map = new PointyTriMap(triDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach(PointyTriPoint point in grid)
		{
			SplicedCell cell = Instantiate(cellPrefab);
		    Vector2 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
					
			cell.SetColor(ExampleUtils.colors[point.GetColor4()]);
			cell.SetText("");
			cell.SetOrientation(point.I);
			
			grid[point] = cell;
		}
	}
}