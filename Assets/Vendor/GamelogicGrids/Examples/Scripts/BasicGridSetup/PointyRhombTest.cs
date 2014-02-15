//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

public class PointyRhombTest : GLMonoBehaviour
{
	private readonly Vector2 CellDimensions = new Vector2(140, 120); 
	
	public SplicedCell cellPrefab;
	public GameObject root;
	public Camera guiCamera;
	public bool randomRotate;
	
	private PointyRhombGrid<SplicedCell> grid;
	private IMap3D<PointyRhombPoint> map;
	
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

			PointyRhombPoint gridPoint = map[worldPosition];
			
			if(grid.Contains(gridPoint))
			{
				grid[gridPoint].HighlightOn = !grid[gridPoint].HighlightOn;
			}
		}
		
		foreach(PointyRhombPoint point in grid)
		{
			var cell = grid[point];
			
			if(randomRotate && Random.value < 0.05f)
			{
				cell.transform.SetRotationZ(Random.value < 0.5f ? 180 : 0);
			}
			
			grid[point] = cell;
		}
	}
	
	private void BuildGrid()
	{
		const int width = 6;
		const int height = 6;
		
		grid = PointyRhombGrid<SplicedCell>
			.BeginShape()
			.Rectangle(width, height)
			.EndShape();
		
		map = new PointyRhombMap(CellDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignTopLeft(grid)
			.To3DXY();


		foreach(PointyRhombPoint point in grid)
		{			
			SplicedCell cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			cell.SetColor(ExampleUtils.colors[point.GetColor12()]);
			cell.SetText(point.ToString());
			cell.SetOrientation(point.I);
			
			if(randomRotate && Random.value < 0.5f)
			{
				cell.transform.SetRotationZ(180);
			}
			
			grid[point] = cell;
		}
	}
}
