//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/PolarFlatBrickGridMain")]
public class PolarFlatBrickGridMain : GLMonoBehaviour
{
	public MeshCell cellPrefab;
	public GameObject gridRoot;

	private IGrid<MeshCell, FlatHexPoint> grid;
	private PolarFlatBrickMap map; 

	public void Start ()
	{
		BuildGrid();
	}

	private void BuildGrid()
	{
		const int width = 6;
		const int height = 3;
		const float border = 0;
		const int quadCount = 15;

		grid = FlatHexGrid<MeshCell>.HorizontallyWrappedRectangle(width, height);
		map = new PolarFlatBrickMap(Vector2.zero, 50, 300, new VectorPoint(width, height));

		foreach (var point in grid)
		{
			var cell = Instantiate(cellPrefab);
			cell.transform.parent = gridRoot.transform;

			Mesh mesh = cell.GetComponent<MeshFilter>().mesh;
			
			float innerRadius = map.GetInnerRadius(point) + border/2;
			float outerRadius = map.GetOuterRadius(point) - border/2;
			float startAngle = map.GetStartAngleZ(point);
			float endAngle = map.GetEndAngleZ(point) - border * Mathf.Rad2Deg / outerRadius;

			MeshUtil.MakeBandedSector(mesh, startAngle, endAngle,innerRadius, outerRadius, quadCount);

			cell.Color = ExampleUtils.colors[point.GetColor1_3()];
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
				grid[gridPoint].HighlightOn = !grid[gridPoint].HighlightOn;
			}
		}
	}
}
