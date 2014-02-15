//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsOutHex : GLMonoBehaviour, IResetable
{
	private const int Symmetry2 = 0;
	private const int Symmetry3 = 1;
	private const int Symmetry6 = 2;

	private readonly Vector2 hexDimensions = new Vector2(74, 84);
	private readonly Color offColor = ExampleUtils.colors[4];
	private readonly Color onColor = ExampleUtils.colors[6];

	public Cell cellPrefab;
	public GameObject root;
	
	private PointyHexGrid<Cell> grid;
	private IMap3D<PointyHexPoint> map;
	
	public void Start()
	{
		Reset();
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			
			PointyHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				ToggleCellAt(hexPoint);
			}
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
		grid = PointyHexGrid<Cell>.Hexagon(5);
		
		map = new PointyHexMap(hexDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach(PointyHexPoint point in grid)
		{
			Cell cell = Instantiate(cellPrefab);

			Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			
			cell.SetColor(offColor);
			cell.SetText("");
						
			grid[point] = cell;
		}
	}
	
	private void InitGame()
	{
		int pattern = Random.Range(0, 4);
		
		switch(pattern)
		{
			case 0:
				InitPattern1();
				break;
			case 1:
				InitPattern2();
				break;			
			case 2:
				InitPattern1();
				InitPattern2();
			break;
			case 3:
				InitPattern3();
			break;
		}		
		
		if(grid.All(p => !grid[p].HighlightOn))
		{
			Reset();
		}
	}
	
	private void InitPattern2()
	{
		int start = Random.Range(0, 3);
		int end = Random.Range(start, 3);
		int symmetry = Random.Range(0, 3); 
		
		switch(symmetry)
		{
			case Symmetry6:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
					ToggleCellAt((PointyHexPoint.West + PointyHexPoint.SouthWest) * i);			
					ToggleCellAt((PointyHexPoint.NorthEast + PointyHexPoint.NorthWest) * i);
					ToggleCellAt((PointyHexPoint.SouthWest + PointyHexPoint.SouthEast) * i);
					ToggleCellAt((PointyHexPoint.NorthWest + PointyHexPoint.West) * i);
					ToggleCellAt((PointyHexPoint.SouthEast + PointyHexPoint.East) * i);
				}
			break;
			case Symmetry3:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
					ToggleCellAt((PointyHexPoint.SouthWest + PointyHexPoint.SouthEast) * i);
					ToggleCellAt((PointyHexPoint.NorthWest + PointyHexPoint.West) * i);
		
				}
			break;
			case Symmetry2:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
					ToggleCellAt((PointyHexPoint.West + PointyHexPoint.SouthWest) * i);			
				}
			break;
		}
	}
	
	private void InitPattern1()
	{	
		int start = Random.Range(0, 5);
		int end = Random.Range(start, 5);
		int symmetry = Random.Range(0, 3); 
		
		switch(symmetry)
		{
			case Symmetry6:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt(PointyHexPoint.East * i);
					ToggleCellAt(PointyHexPoint.West * i);			
					ToggleCellAt(PointyHexPoint.NorthEast * i);
					ToggleCellAt(PointyHexPoint.SouthWest * i);
					ToggleCellAt(PointyHexPoint.NorthWest * i);
					ToggleCellAt(PointyHexPoint.SouthEast * i);
				}
			break;
			case Symmetry3:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt(PointyHexPoint.East * i);
					ToggleCellAt(PointyHexPoint.SouthWest * i);
					ToggleCellAt(PointyHexPoint.NorthWest * i);
		
				}
			break;
			case Symmetry2:
				for(int i = start; i <= end; i++)
				{			
					ToggleCellAt(PointyHexPoint.East * i);
					ToggleCellAt(PointyHexPoint.West * i);			
				}
			break;
		}
	}
	
	private void InitPattern3()
	{
		var randomPoints = grid.SampleRandom(2);
		var pattern = new HashSet <PointyHexPoint>();
		int symmetry = Random.Range(0, 3); 
		
		foreach (var pointyHexPoints in randomPoints.Select(point1 => grid.Where(p => (p - point1).Magnitude() <= 3).SampleRandom(2)).Select(randomGroup => randomGroup as IList<PointyHexPoint> ?? randomGroup.ToList()))
		{
			pattern.AddRange(pointyHexPoints);
			
			switch(symmetry)
			{
				case Symmetry6:
				
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate60()));
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate120()));
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate180()));
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate240()));
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate300()));
				
					break;
					
				case Symmetry3:			
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate120()));
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate240()));
				
					break;
					
				case Symmetry2:			
					pattern.AddRange(pointyHexPoints.Select(p => p.Rotate180()));			
					break;
			}
		}
		
		foreach (var point in pattern)
		{
			ToggleCellAt(point);
		}
	}
	
	private void ToggleCellAt(PointyHexPoint hexPoint)
	{
		foreach(PointyHexPoint point in grid.GetNeighbors(hexPoint))
		{
			grid[point].HighlightOn = !grid[point].HighlightOn;
			grid[point].SetColor(grid[point].HighlightOn ? onColor : offColor);
		}
	}
}