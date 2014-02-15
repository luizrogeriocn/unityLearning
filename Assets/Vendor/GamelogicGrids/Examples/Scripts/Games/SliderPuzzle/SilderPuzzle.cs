//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
	This example shows how the Grids library work with normal Unity Planes.
*/
public class SilderPuzzle : GLMonoBehaviour
{
	private class SliderCell
	{
		public RectPoint originalGridPoint;
		public GameObject cellObject;
	}
	
	public GameObject cellPrefab;
	public GameObject root;
	public int puzzleSize;
	public Texture2D puzzleImage;

	private RectGrid<SliderCell> grid;
	private IMap3D<RectPoint> map;
	private Vector2 cellDimensions = new Vector2(200, 200);

	private RectPoint emptyCell;
	

	public void Start()
	{
		BuildGrid();
		StartCoroutine(InitPuzzle());
	}

	public void Update()
	{
		HandleCell();

		if (IsGameFinished())
		{
			Debug.Log("Game finished: you solved the puzzle!");
		}
	}

	private bool IsGameFinished()
	{
		return grid.Where(gridPoint => gridPoint != emptyCell).All(gridPoint => grid[gridPoint].originalGridPoint == gridPoint);
	}

	private void HandleCell()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ProcessClick();
		}
	}

	private void ProcessClick()
	{
		var worldPosition = ExampleUtils.ScreenToWorld(Input.mousePosition);
		var gridPosition = map[worldPosition];

		if (grid.Contains(gridPosition))
		{
			if (grid.GetNeighbors(gridPosition).Contains(emptyCell))
			{
				SwapWithEmpty(gridPosition);
			}
		}
	}

	private void SwapWithEmpty(RectPoint gridPosition)
	{
		grid[emptyCell] = grid[gridPosition];
		grid[emptyCell].cellObject.transform.localPosition = map[emptyCell];
		grid[gridPosition] = null;
		emptyCell = gridPosition;
	}

	private void BuildGrid()
	{
		grid = RectGrid<SliderCell>.Rectangle(puzzleSize, puzzleSize);

		map = new RectMap(cellDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.To3DXY();

		var textureScale = 1f / puzzleSize;
		var textureScaleVector = new Vector2(textureScale, textureScale);

		foreach (var point in grid)
		{
			var cellObject = Instantiate(cellPrefab);

			cellObject.transform.parent = root.transform;
			cellObject.transform.localPosition = map[point];
			
			cellObject.renderer.material.mainTexture = puzzleImage;
			cellObject.renderer.material.mainTextureScale = textureScaleVector;
			cellObject.renderer.material.mainTextureOffset = new Vector2(-textureScale * (point.X + 1), textureScale * point.Y);

			var cell = new SliderCell();
			cell.cellObject = cellObject;
			cell.originalGridPoint = point;

			grid[point] = cell;			
		}

		emptyCell = RectPoint.Zero;
		grid[emptyCell].cellObject.renderer.enabled = false;

	}

	private IEnumerator InitPuzzle()
	{
		var memory = new Queue<RectPoint>();
		
		memory.Enqueue(emptyCell);
		memory.Enqueue(emptyCell);

		for (var i = 0; i < 2 * puzzleSize * puzzleSize; i++)
		{
			var oldPath = memory.Dequeue();

			var randomNeighbor = grid.GetNeighbors(emptyCell)
				.Where(point => (point != oldPath))
				.SampleRandom(1)
				.First();

			memory.Enqueue(randomNeighbor);
			SwapWithEmpty(randomNeighbor);

			yield return new WaitForSeconds(0.2f); //Let us see the shuffle!
		}
	}
}