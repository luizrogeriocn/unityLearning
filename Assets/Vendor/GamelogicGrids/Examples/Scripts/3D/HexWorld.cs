using Gamelogic.Grids;
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using UnityEngine;

/**
	This example shows how you can use a grid in 3D.
*/
public class HexWorld : GLMonoBehaviour
{
	public Block blockPrefab;
	public GameObject gridRoot;
	public Texture2D heightMap;
	
	private FlatHexGrid<Block> grid;
	private IMap3D<FlatHexPoint> map;

	public void Start()
	{
		BuildGrid();
	}
	
	private void BuildGrid()
	{
		grid = FlatHexGrid<Block>.FatRectangle(50, 50);
		
		map = new FlatHexMap(new Vector2(.80f, .69f) * 0.34f)
			.To3DXZ();
		
		var map2D = new FlatHexMap(new Vector2(80, 60) * 0.05f);	
		
		foreach(var point in grid)
		{
			var block = Instantiate(blockPrefab);
			//block.transform.parent = gridRoot.transform;
			block.transform.rotation = Quaternion.Euler(new Vector3(90, 90, 90));
			block.transform.localPosition = map[point];

			int x = Mathf.FloorToInt(map2D[point].x);
			int y = Mathf.FloorToInt(map2D[point].y);
			float height = heightMap.GetPixel(x, y).r * 4 - 2;

			if (height <= 0)
			{
				height = 0.01f;
			}

			block.SetColor(ExampleUtils.Blend(height, ExampleUtils.colors[0], ExampleUtils.colors[1]));
			block.transform.localScale = new Vector3(1, 1, -height); //height is in z because the model is rotated :/
			
			grid[point] = block;
		}
	}
}
