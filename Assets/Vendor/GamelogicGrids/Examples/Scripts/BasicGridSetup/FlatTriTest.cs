//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

public class FlatTriTest : GLMonoBehaviour
{
	private readonly Vector2 TriDimensions = new Vector2(80, 69); 
	
	public SplicedCell cellPrefab;
	public Texture2D testTexture;
	public GameObject root;
	
	private FlatTriGrid<SplicedCell> grid;
	private IMap3D<FlatTriPoint> map;
	
	public void Start()
	{
		BuildGrid();		
	}
	
	public void Update()
	{		
		if(Input.GetMouseButtonDown(0))
		{		
			Vector2 worldPosition = ExampleUtils.ScreenToWorld_NGUI(root, Input.mousePosition);            
			FlatTriPoint triPoint = map[worldPosition];
			
			if(grid.Contains(triPoint))
			{
				grid[triPoint].HighlightOn = !grid[triPoint].HighlightOn;
			}
		}
	}	
	
	private void BuildGrid()
	{
		const int side = 6;
		grid = FlatTriGrid<SplicedCell>
			.BeginShape()
				.Hexagon(side)
			.EndShape();
		
		map = new FlatTriMap(TriDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach(FlatTriPoint point in grid)
		{
			SplicedCell cell = Instantiate(cellPrefab);
		    Vector3 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			
			cell.SetColor(ExampleUtils.colors[point.GetColor2() * 2]);
			cell.SetText("(" + (point.X * 2 + point.I) + ", " + point.Y + ")");
			cell.SetOrientation(point.I);
			
			grid[point] = cell;
		}
	}
}
