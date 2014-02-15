//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright � 2013 Gamelogic (Pty) Ltd         //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
	This example gives an example of building a new 
	algorithm on top of the grid components. 
*/
[AddComponentMenu("Gamelogic/Examples/Diffusion")]
public class DiffusionHex : GLMonoBehaviour 
{
	public Cell cellPrefab;
	private PointyHexGrid<Cell> grid;
	private PointyHexGrid<float> gas;
	private IMap3D<PointyHexPoint> map;
	public GameObject root;
	
	private Vector2 cellDimensions = new Vector2(69, 80);
	
	private Color offColor = new Color(1, 1, 1, 0);
	
	public void Start()
	{	
		BuildGrid();
	}
	
	private void BuildGrid()
	{		
		grid = PointyHexGrid<Cell>
			.FatRectangle(18, 21);
		
		gas = PointyHexGrid<float>
			.FatRectangle(18, 21);
		
		map = new PointyHexMap(cellDimensions)
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.Scale(.5f) //for a tighter packing 
			.To3DXY();
		
		foreach(var point in grid)
		{			
			Cell cell = Instantiate<Cell>(cellPrefab);
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = map[point];
			
			cell.SetText("");
			cell.SetColor(offColor);
			
			grid[point] = cell;
			gas[point] = 0;
		}
	}
	
	float CalculateAverage(PointyHexPoint point, IEnumerable<PointyHexPoint> neighbors)
	{
		float sum = neighbors
			.Select(x => gas[x])
			.Aggregate((p, q) => p + q) + gas[point];
		
		return sum / (neighbors.Count() + 1);
	}
	
	public void Update()
	{
		Algorithms.AggregateNeighborhood<float, PointyHexPoint>(gas, CalculateAverage); //This adds the 
		
		if(Input.GetMouseButton(0))
		{
			Vector3 worldPoint = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint point = map[worldPoint];
			
			if(gas.Contains(point))
			{
				gas[point] = 2;
			}
		}
		
		if(Input.GetMouseButton(1))
		{
			Vector3 worldPoint = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);
			PointyHexPoint point = map[worldPoint];
			
			if(gas.Contains(point))
			{
				gas[point] = -1;
			}
		}
		
		foreach(var point in gas)
		{
			UpdateCell(point);
		}
	}
	
	
			
	private void UpdateCell(PointyHexPoint point)
	{
		Color newColor = ExampleUtils.Blend(gas[point], ExampleUtils.colors[4], ExampleUtils.colors[7]);
		grid[point].SetColor(newColor);
	}
}
