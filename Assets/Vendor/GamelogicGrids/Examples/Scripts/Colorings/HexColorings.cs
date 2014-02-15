//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System;
using UnityEngine;

public class HexColorings : GLMonoBehaviour 
{
	public Cell cellPrefab;
	public GameObject root;
	
	private PointyHexGrid<Cell> grid;
	private IMap3D<PointyHexPoint> map;
	private int colorPatternIndex = 0;
	
	public void Start()
	{
		BuildGrid();
	}
	
	public void Update()
	{
		if(Input.anyKeyDown)
		{
			colorPatternIndex++;
			colorPatternIndex %= 10;
			
			ColorGrid(colorPatternIndex);
		}
	}
	
	public void BuildGrid()
	{
		grid = PointyHexGrid<Cell>.FatRectangle(16, 20);
		
		map = new PointyHexMap(new Vector2(69, 80))
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.Scale(1.05f)
			.To3DXY();
		
		foreach(var point in grid)
		{
			Cell cell = Instantiate(cellPrefab);
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = map[point];
			
			grid[point] = cell;
		}
		
		ColorGrid(colorPatternIndex);
	}
	
	private void ColorPoint(PointyHexPoint point,  int colorPattern)
	{
		Func<int> coloring;
		var p = point;
		switch(colorPattern)
		{
			case 0:
				coloring = p.GetColor1_1;
				break;
			case 1:
				coloring = p.GetColor1_2;
				break;
			case 2:
				coloring = p.GetColor1_3;
				break;
			case 3:
				coloring = p.GetColor2_2;
				break;
			case 4:
				coloring = p.GetColor2_4;
				break;
			case 5:
				coloring = p.GetColor3_2;
				break;
			case 6:
				coloring = p.GetColor3_7;
				break;
			case 7:
				coloring = p.GetColor5_5;
				break;
			case 8:
				coloring = p.GetColor6;
				break;
			case 9:
				coloring = (() => p.GetColor(3, 1, 2));
				break;
			default:
				coloring = p.GetColor1_1;
			break;
		}
		
		int color = coloring();
		
		grid[point].SetColor(ExampleUtils.colors[color]);
	}
	
	private void ColorGrid(int colorPattern)
	{
		foreach(var point in grid)
		{
			ColorPoint(point, colorPattern);
		}
	}
}
