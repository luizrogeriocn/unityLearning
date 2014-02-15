//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
	This example gives an example of building a new 
	algorithm on top of the grid components. 
*/
[AddComponentMenu("Gamelogic/Examples/MazeBuilder")]
public class MazeBuilder : GLMonoBehaviour 
{
	private readonly Vector2 cellDimensions = new Vector2(70, 80);
	private const int size = 20;

	private readonly Color offColor = ExampleUtils.colors[4];
	private readonly Color onColor = ExampleUtils.colors[6];

	public Cell cellPrefab;
	private PointyHexGrid<Cell> grid;
	private PointyHexGrid<bool> logicalGrid;
	private List<PointyHexPoint> smallGrid;
	private IMap3D<PointyHexPoint> map;
	private PointList<PointyHexPoint> dirtyCells;
	public GameObject root;
	


	private PointyHexPoint startNode;
	private PointyHexPoint endNode;
	private int iterationCount;
	
	public void Awake()
	{
		dirtyCells = new PointList<PointyHexPoint>();
	}
	
	public void Start()
	{
		iterationCount = 0;
		
		BuildGrid();
		InitPath();
	}
	
	private void BuildGrid()
	{
		startNode = new PointyHexPoint(-size, size);
		endNode = new PointyHexPoint(size, -size);
		
		smallGrid = PointyHexGrid<int>
			.Hexagon(size)
			.ToList();
			
		grid = PointyHexGrid<Cell>
			.Hexagon(size + 1);
		
		logicalGrid = PointyHexGrid<bool>
			.Hexagon(size + 1);
		
		map = new PointyHexMap(cellDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY();
		
		foreach(var point in grid)
		{			
			var cell = MakeCell(point);

			grid[point] = cell;
			logicalGrid[point] = false;
		}
	}

	private Cell MakeCell(PointyHexPoint point)
	{
		var cell = Instantiate<Cell>(cellPrefab);

		cell.transform.parent = root.transform;
		cell.transform.localScale = Vector3.one;
		cell.transform.localPosition = map[point];

		cell.SetText("");
		cell.SetColor(offColor);
		return cell;
	}


	private void InitPath()
	{
		for(int i = -size; i <= size; i++)
		{
			ToggleCellAt(new PointyHexPoint(i, -i));
		}
	}
	
	public void Update()
	{
		if(iterationCount < 400)  //stop after a while
		{
			iterationCount++;
			
			for(int i = 0; i < 400; i++) //do a fiew iterations per update
			{
				ToggleRandomCell();
			}
		}
		
		foreach(var point in dirtyCells) //only update visual cells each update
		{
			UpdateCell(point);
		}
		
		dirtyCells.Clear();
	}
	
	public void ToggleRandomCell()
	{		
		int count = smallGrid.Count();		
		int index = Random.Range(0, count);
		
		PointyHexPoint randomPoint = smallGrid[index];
		
		if(randomPoint == endNode || randomPoint == startNode)
		{
			return;
		}

		if ((!logicalGrid[randomPoint] || !(Random.value < 0.5f)) &&
		    (logicalGrid[randomPoint] || !(Random.value < 20f/iterationCount + 0.02f)))
		{
			return;
		}

		List<PointyHexPoint> neighborHood = grid.GetAllNeighbors(randomPoint).ToList();
		neighborHood.Add(randomPoint);
			
		var closedCells = 
			from point in neighborHood 
			where !logicalGrid[point]
			select point;

		var pointyHexPoints = closedCells as PointyHexPoint[] ?? closedCells.ToArray();
			
		if (pointyHexPoints.Count() >= 7) return;
			
		bool closedCellsAreConnected;
				
		if(pointyHexPoints.Any())
		{
			closedCellsAreConnected = Algorithms.IsConnected(
				logicalGrid, 
				pointyHexPoints, 
				(x, y) => (logicalGrid[x] == logicalGrid[y]));
		}
		else
		{
			closedCellsAreConnected = true;
		}
				
		if(closedCellsAreConnected)
		{
			ToggleCellAt(randomPoint);
		}
	}
			
	private void UpdateCell(PointyHexPoint point)
	{
		grid[point].SetColor(logicalGrid[point] ? onColor : offColor);
	}
	
	private void ToggleCellAt(PointyHexPoint point)
	{
		dirtyCells.Add(point);
		logicalGrid[point] = !logicalGrid[point];
	}
}
