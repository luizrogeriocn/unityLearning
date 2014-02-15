//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/PrimsAlgorithm")]
public class PrimsAlgorithm : GLMonoBehaviour, IResetable
{
	public Cell cellPrefab;
	public MazeCell edgePrefab;
	public GameObject root;
	
	private readonly Vector2 cellDimensions = new Vector2(140, 120);
	
	public void Start()
	{
		Reset();
	}
	
	public void Reset()
	{
		root.transform.DestroyChildren();
		StartCoroutine(BuildGrid());
	}	

	public IEnumerator BuildGrid()
	{
		var grid = FlatTriGrid<Cell>.Hexagon(5);// Rectangle(8, 6);

		var edgeGrid = grid.MakeEdgeGrid<MazeCell>();

		var edgeMap = new PointyRhombMap(cellDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(edgeGrid)
			.Scale(1.1f)
			.To3DXY();

		foreach (var point in edgeGrid)
		{
			MazeCell cell = Instantiate<MazeCell>(edgePrefab);
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = edgeMap[point];
			cell.SetText("");
			cell.SetOrientation(point.I, false);

			edgeGrid[point] = cell;
		}
		
		foreach (var point in MazeAlgorithms.GenerateMazeWalls(grid))
		{
			edgeGrid[point].SetOrientation(point.I, true);

			yield return null;
		}
		
		yield return null;
	}
}
