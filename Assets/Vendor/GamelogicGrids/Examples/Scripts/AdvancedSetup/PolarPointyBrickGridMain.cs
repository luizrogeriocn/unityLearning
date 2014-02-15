//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/PolarPointyBrickGridMain")]
public class PolarPointyBrickGridMain : GLMonoBehaviour
{
	public MeshCell cellPrefab;
	public GameObject gridRoot;

	private IGrid<MeshCell, PointyHexPoint> grid;
	private PolarPointyBrickMap map; 

	public void Start ()
	{
		BuildGrid();
	}

	private void BuildGrid()
	{
		const int width = 6;
		const int height = 5;
		const float border = 0f;
		const float quadSize = 15f;

		grid = PointyHexGrid<MeshCell>.HorizontallyWrappedRectangle(width, height);
		map = new PolarPointyBrickMap(Vector2.zero, 50, 350, new VectorPoint(width, height));

		foreach (var point in grid)
		{
			var cell = Instantiate(cellPrefab);
			cell.transform.parent = gridRoot.transform;

			Mesh mesh = cell.GetComponent<MeshFilter>().mesh;
			
			float innerRadius = map.GetInnerRadius(point) + border/2;
			float outerRadius = map.GetOuterRadius(point) - border/2;
			float startAngle = map.GetStartAngleZ(point);
			float endAngle = map.GetEndAngleZ(point) - border * Mathf.Rad2Deg / outerRadius;
			int quadCount = Mathf.CeilToInt(outerRadius * 2 * Mathf.PI / (quadSize * width));

			MeshUtil.MakeBandedSector(mesh, startAngle, endAngle,innerRadius, outerRadius, quadCount);

			cell.Color = ExampleUtils.colors[point.GetColor(3, -1, 2)];
			cell.HighlightOn = false;
			cell.Pivot = map[point].XYTo3D();
			cell.Text = point.ToString();

			grid[point] = cell;
		}
	}

	// Update is called once per frame
	public void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			var worldPosition = ExampleUtils.ScreenToWorld(gridRoot, Input.mousePosition);

			var gridPoint = map[worldPosition];

			if (grid.Contains(gridPoint))
			{
				foreach (var point in grid.GetNeighbors(gridPoint))
				{
					grid[point].HighlightOn = !grid[point].HighlightOn;
				}
				
			}
		}
	}
}
