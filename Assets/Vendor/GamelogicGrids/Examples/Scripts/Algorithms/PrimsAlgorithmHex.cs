//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/PrimsAlgorithmHex")]
public class PrimsAlgorithmHex : GLMonoBehaviour, IResetable
{
	public Cell cellPrefab;
	public GameObject root;
	
	private readonly Vector2 cellDimensions = new Vector2(69, 80);
	
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
		var grid = PointyHexGrid<Cell>.Hexagon(6);

		var map = new PointyHexMap(cellDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.Scale(.97f) //Makes cells overlap slightly; prevents border artefacts
			.To3DXY();

		foreach (var point in grid)
		{
			Cell cell = Instantiate<Cell>(cellPrefab);
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = map[point];
			cell.SetText("");
			int color = point.GetColor2_4();
			cell.SetFrame(color);
			grid[point] = cell;
		}
		
		foreach (var point in MazeAlgorithms.GenerateMazeWalls(grid))
		{
			grid[point].SetFrame(0);
			yield return null;
		}
		
		yield return null;
	}
}
