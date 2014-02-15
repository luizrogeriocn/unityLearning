//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Linq;
using UnityEngine;

public class Pipes : GLMonoBehaviour
{	
	/*
		In this example each tile image has a binary presentation with 
		six digits that correponds to the six edges	of the hex and
		indicate whether edges have pipes through them or not.
		
		This arrays maps the image number to the sprite frame index.
	*/
	private readonly int[] FrameIndices = 
	{
		 0,  1, -1,  2, -1,  3, -1, 4,
		-1,  5, -1,  6, -1, -1, -1,  7,
		-1, -1, -1,  8, -1,  9, -1, 10,
		-1, -1, -1, 11, -1, -1, -1, 12,
		-1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, 13		
	};
	private readonly Vector2 hexDimensions = new Vector2(74, 85);

	public PipesCell cellPrefab;	
	public GameObject root;
	
	private PointyHexGrid<PipesCell> grid;
	private IMap3D<PointyHexPoint> map;
		
	public void Start()
	{
		Random.seed = 5;
		
		BuildGrid();
		RandomRotateCells();
		UpdateHighlights();
	}
	
	private void BuildGrid()
	{		
		grid = PointyHexGrid<PipesCell>.Hexagon(5);
		
		map = new PointyHexMap(hexDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		var edgeGrid = grid.MakeEdgeGrid<int>();
			
		foreach(PointyRhombPoint point in edgeGrid)
		{
			edgeGrid[point] = Random.Range(0, 2);			
		}
		
		foreach (PointyHexPoint point in grid)
		{
			var cell = Instantiate<PipesCell> (cellPrefab);
			Vector3 rPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = rPoint;			
			cell.transform.SetRotationZ(-30);
			
			grid[point] = cell;
			
			SetCellSprite(edgeGrid, point, cell);					
		}
	}	

	private void RandomRotateCells()
	{		
		foreach (PointyHexPoint point in grid)
		{
			int rotationCount = Random.Range (0, 6);
			
			for (int i = 0; i < rotationCount; i++)
			{
				grid [point].RotateCW();
			}			
		}
	}
	
	private void UpdateHighlights()
	{
		foreach (var point in grid)
		{
			UpdateHighlight(point);
		}
	}

	private void SetCellSprite(IGrid<int, PointyRhombPoint> edgeGrid, PointyHexPoint point, PipesCell cell)
	{
		var edges = 
			from edgePoint in point.GetEdges()
			select edgeGrid [edgePoint];
		
		int imageIndex = edges.Reverse().Aggregate ((x, y) => (x << 1) + y);
		
		// Because images are flat hex, not pointy, and we want them pointy
		float zRotation = -30; 
		
		for (int i = 0; i < 6; i++)
		{
			if (FrameIndices [imageIndex] != -1) //we found it
			{ 
				//so we can use it
				cell.SetFrame (FrameIndices[imageIndex]);
				cell.Image.transform.SetRotationZ (zRotation);
				
				break;
			}
			
			//While we cannot find the sprite, transform and check again
			zRotation += 60;
			imageIndex = RotateEdgeNumberClockWise (imageIndex);
		}
		
		cell.EdgeData = edges.ToList();
	}
	
	public int RotateEdgeNumberClockWise(int edge)
	{
		return ((edge & 1) << 5) + (edge >> 1);
	}
	
	public int RotateEdgeNumberCounterClockWise(int edge)
	{
		return ((edge << 1) & 63) + (edge >> 5); 
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				grid[hexPoint].RotateCW();				
				UpdateHighlight(hexPoint);
				
				foreach(var neighbor in grid.GetNeighbors(hexPoint))
				{
					UpdateHighlight(neighbor);
				}
			}
		}
		else if(Input.GetMouseButtonDown(1))
		{		
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				grid[hexPoint].RotateCCW();
				UpdateHighlight(hexPoint);
				
				foreach(var neighbor in grid.GetNeighbors(hexPoint))
				{
					UpdateHighlight(neighbor);
				}
			}
		}
		
		if(HasGameFinished())
		{
			Debug.Log ("Game Ended!");
		}
	}
	
	public bool HasGameFinished()
	{	
		return grid.All(IsClosed);
	}
	
	public void UpdateHighlight(PointyHexPoint point)
	{
		grid[point].HighlightOn = IsClosed(point);
	}
	
	public bool IsClosed(PointyHexPoint point)
	{		
		var neighbors = grid.GetAllNeighbors(point).ToList();
		bool isClosed = true;
		
		//We use for loop so that we can use the index in the access operation below
		for (int i = 0; i < neighbors.Count(); i++) 
		{			
			if (grid.Contains(neighbors[i]))
			{
				//(i + 3) % 6 is the opposite neighbor
				//Here we abuse the fact that neighbors are returned in (anti-clockwise) order.
				if (grid[point].EdgeData[i] != grid[neighbors[i]].EdgeData[(i+3)%6]) 
				{
					isClosed = false;
				}
			}
		}

		return isClosed;
	}
}