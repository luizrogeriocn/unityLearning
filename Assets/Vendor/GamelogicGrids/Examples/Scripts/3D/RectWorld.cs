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
public class RectWorld : GLMonoBehaviour
{
	public Block blockPrefab;
	public GameObject gridRoot;
	public Texture2D heightMap;
	
	private RectGrid<Block> grid;
	private IMap3D<RectPoint> map;

	public void Start()
	{
		BuildGrid();
	}
	
	private void BuildGrid()
	{
		grid = RectGrid<Block>.Rectangle(50, 50);
		
		map = new RectMap(new Vector2(.80f, .69f) * 0.3f)
			.To3DXZ();

		var map2D = new RectMap(new Vector2(80, 60) * 0.05f);	
		
		foreach(var point in grid)
		{
			var block = Instantiate(blockPrefab);
			//block.transform.parent = gridRoot.transform;
			block.transform.rotation = Quaternion.Euler(new Vector3(90, 90, 90));
			block.transform.localPosition = map[point];

			int x = Mathf.FloorToInt(map2D[point].x);
			int y = Mathf.FloorToInt(map2D[point].y);
			float height = heightMap.GetPixel(x, y).r * 4 - 1.5f;

			if (height <= 0)
			{
				height = 0.01f;
			}

			block.SetColor(ExampleUtils.Blend(height, ExampleUtils.colors[0], ExampleUtils.colors[1]));
			block.transform.localScale = new Vector3(.25f, .25f, -height); //height is in z because the model is rotated :/
			
			grid[point] = block;
		}
	}
}
