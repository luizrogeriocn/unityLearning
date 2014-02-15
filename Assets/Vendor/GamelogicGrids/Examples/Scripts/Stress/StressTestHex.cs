//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Collections;
using System.Linq;
using UnityEngine;

public class StressTestHex : GLMonoBehaviour 
{
	public Block cellPrefab;
	public int cellsPerIteration = 1000;
	public Camera cam;
	public int width = 500;
	public int height = 500;
	public int totalCellCount;
	
	private PointyHexGrid<Block> grid;
	private IMap3D<PointyHexPoint> map;
	
	
	public void Start()
	{
		StartCoroutine(BuildGrid());
	}
	
	public void OnGUI()
	{
		GUILayout.TextField("Hexes: " + totalCellCount);
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{			
			Vector3 worldPosition = ExampleUtils.ScreenToWorld(Input.mousePosition);
			PointyHexPoint hexPoint = map[worldPosition];
			
			if(grid.Contains(hexPoint))
			{
				if(grid[hexPoint] != null)
				{
					grid[hexPoint].gameObject.active = !grid[hexPoint].gameObject.active;
				}
			}
		}	
		
		if(Input.GetKey(KeyCode.UpArrow))
		{
			cam.transform.position = cam.transform.position + Vector3.up * .1f;
		}
		
		if(Input.GetKey(KeyCode.DownArrow))
		{
			cam.transform.position = cam.transform.position + Vector3.down * .1f;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			cam.transform.position = cam.transform.position + Vector3.left * .1f;
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			cam.transform.position = cam.transform.position + Vector3.right * .1f;
		}
	}
	
	public IEnumerator BuildGrid()
	{	
		totalCellCount = 0;
		grid = PointyHexGrid<Block>.Rectangle(width, height);
		
		map = new PointyHexMap(new Vector2(69, 80))
			.Scale(0.004f)
			.To3DXY();
		
		int cellCount = 0;
		
		var shuffledGrid = grid.ToList();
		shuffledGrid.Shuffle();
		
		foreach(var point in grid)
		{
			var cell = Instantiate(cellPrefab);			
			Vector3 worldPoint = map[point];

			cell.transform.localPosition = worldPoint;
			
			cellCount++;
			totalCellCount++;
			
			grid[point] = cell;
			
			if(cellCount >= cellsPerIteration)
			{
				cellCount = 0;
				yield return null;
			}
		}
	}
}
