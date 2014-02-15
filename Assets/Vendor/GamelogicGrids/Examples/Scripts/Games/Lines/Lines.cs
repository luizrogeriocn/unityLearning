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

public class Lines : GLMonoBehaviour
{
	#region Constants
	private readonly Vector2 hexDimensions = new Vector2(74, 84);//
	#endregion

	#region Inspector
	public LinesCell cellPrefab;
	public int width = 5;
	public int height = 5;
	public int lineLength = 4;
	public int colorCount = 3;
	public int cellsPerTurn = 5;
	
	public GameObject root;
	#endregion
	
	#region Private Fields
	private PointyHexGrid<LinesCell> grid;
	private IMap3D<PointyHexPoint> map;
	private bool isHoldingCell;
	private LinesCell cellThatIsBeingHeld;
	#endregion 
	
	#region Callbacks
	public void Start()
	{
		BuildGrid();
		AddNewCells();
	}	
	
	public void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{		
			ProcessClick();
		}
	}

	private void ProcessClick()
	{
		Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
		PointyHexPoint hexPoint = map[worldPosition];

		if (grid.Contains(hexPoint))
		{
			ClickPoint(hexPoint);
		}
	}

	#endregion

	#region Implementation
	private void ClickPoint(PointyHexPoint hexPoint)
	{
		LinesCell clickedCell = grid [hexPoint];
		
		if (isHoldingCell)
		{
			if (clickedCell.IsEmpty)
			{
				MoveCell(clickedCell);
				
				if (!ClearLinesAroundPoint(hexPoint))
				{
					AddNewCells();
				}//otherwise, give the player a "free" turn.
				
			}
			else if (clickedCell == cellThatIsBeingHeld)
			{
				DropCell();
			}
		}
		else
		{
			if (!clickedCell.IsEmpty)
			{
				PickUpCell(clickedCell);
			}
		}
	}

	private void PickUpCell(LinesCell clickedCell)
	{
		cellThatIsBeingHeld = clickedCell;
		isHoldingCell = true;
		clickedCell.HighlightOn = true;
	}

	private void DropCell()
	{
		cellThatIsBeingHeld.HighlightOn = false;
		cellThatIsBeingHeld = null;
		isHoldingCell = false;
	}

	private void MoveCell(LinesCell clickedCell)
	{
		cellThatIsBeingHeld.HighlightOn = false;
		SwapCells (cellThatIsBeingHeld, clickedCell);
		cellThatIsBeingHeld = null;
		isHoldingCell = false;
	}

	private void BuildGrid()
	{
		grid = PointyHexGrid<LinesCell>
			.BeginShape()
			.UpTriangle(7)
			.Translate(-1, 2)
			.Union()
			.DownTriangle(7)
			.EndShape();
		
		map = new PointyHexMap(hexDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach (PointyHexPoint point in grid)
		{
			var cell = Instantiate<LinesCell> (cellPrefab);
			Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localPosition = worldPoint;
			cell.transform.localScale = Vector3.one;
			
			cell.SetState (true, -1);
			grid[point] = cell;
		}
	}	
	
	private void AddNewCells()
	{
		var emptyCells = GetEmptyCells();
		IEnumerable<PointyHexPoint> cellsToPlace = emptyCells.SampleRandom(cellsPerTurn);
		
		foreach(PointyHexPoint point in cellsToPlace)
		{
			SetCellToRandom(grid[point]);
			ClearLinesAroundPoint(point);
		}
		
		emptyCells = GetEmptyCells();
		
		if(!emptyCells.Any())
		{
			Debug.Log ("Game Ends!");
		}
	}
	
	private IEnumerable<PointyHexPoint> GetEmptyCells ()
	{
		return
			from point in grid
			where grid [point].IsEmpty
			select point;
	}

	private void SetCellToRandom(LinesCell cell)
	{
		int newColor = (Random.Range(0, colorCount));		
		cell.SetState(false, newColor);
	}

	private static void SwapCells(LinesCell cell1, LinesCell cell2)
	{
		int tempColor = cell1.Color;
		bool tempIsEmpty = cell1.IsEmpty;
		
		cell1.SetState(cell2.IsEmpty, cell2.Color);
		cell2.SetState(tempIsEmpty, tempColor);
	}

	private bool ClearLinesAroundPoint(PointyHexPoint point)
	{
		bool wasLinesRemoved = false;
		IEnumerable<IEnumerable<PointyHexPoint>> lines =
			from line in Algorithms.GetConnectedLines(grid, point, (p1, p2) => grid[p1].Color == grid[p2].Color)
			where line.Count() >= lineLength
			select line;
		
		foreach (IEnumerable<PointyHexPoint> line in lines)
		{
			foreach(PointyHexPoint linePoint in line)
			{
				grid[linePoint].SetState(true, -1);
			}
			
			wasLinesRemoved = true;
		}

		return wasLinesRemoved;
	}
	#endregion
}
