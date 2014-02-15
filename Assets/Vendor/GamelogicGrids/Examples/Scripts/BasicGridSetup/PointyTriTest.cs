//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

public class PointyTriTest : GLMonoBehaviour
{
	public SplicedCell cellPrefab;
	public GameObject root;

	private readonly Vector2 TriDimensions = new Vector2(69, 80);
	private PointyTriGrid<SplicedCell> grid;
	private IMap3D<PointyTriPoint> map;

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
			PointyTriPoint triPoint = map[worldPosition];
			
			if(grid.Contains(triPoint))
			{
				grid[triPoint].HighlightOn = !grid[triPoint].HighlightOn;
			}
		}
	}

	private static Color GetColor(int n, int m)
	{
		float a = n/(float) m;
		float b = Mathf.Sin(a*2*Mathf.PI*10)/2 + 0.5f;

		var c1 = ExampleUtils.Blend(a, ExampleUtils.colors[2], ExampleUtils.colors[3]);
		var c2 = ExampleUtils.Blend(Mathf.Pow(b, 1.73f), c1, ExampleUtils.colors[0]);

		return c2;
	}
	
	private void BuildGrid()
	{
		const int side = 3;

		grid = PointyTriGrid<SplicedCell>			
			.Star(side);
		
		map = new PointyTriMap(TriDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.To3DXY();
		
		foreach(PointyTriPoint point in grid)
		{
			SplicedCell cell = Instantiate(cellPrefab);
		    Vector2 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;

			cell.SetColor(GetColor(point.GetColor(12, 4, 4), 74));
			cell.SetText(point.ToString());
			cell.SetOrientation(point.I);
			
			grid[point] = cell;
		}
	}
}
