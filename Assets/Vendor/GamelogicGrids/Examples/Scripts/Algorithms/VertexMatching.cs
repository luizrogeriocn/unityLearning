//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/VertexMatching")]
public class VertexMatching : GLMonoBehaviour
{	
	public int size = 4;
	public GameObject root;
	
	private Vector2 hexDimensions = new Vector2(65, 74);
	
	private int[] frameIndices = 
	{
		 0,  1, -1,  2, -1,  3, -1,  4,
		-1,  5, -1,  6, -1, -1, -1, -1,
		-1, -1, -1, -1, -1,  7, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1,  8, -1, -1, -1, -1, -1,
		-1, -1, -1, -1,  9, -1, 10, -1,
		11, -1, 12, -1, 13, -1, 14, 15		
	};
	
	public Cell cellPrefab;	
	
	private PointyHexGrid<Cell> grid;
	private IMap3D<PointyHexPoint> map;
	
	public void Start()
	{
		Random.seed = 0;
		BuildGrid();
	}
	
	private void BuildGrid()
	{		
		grid = PointyHexGrid<Cell>.Default(size, size);
		
		map = new PointyHexMap(hexDimensions)
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(grid)
			.AnchorCellMiddleCenter()
			.Scale(1.1f)
			.To3DXY();
		
		FlatTriGrid<int> vertexGrid = (FlatTriGrid<int>) grid.MakeVertexGrid<int>();// new FlatTriGrid<int>(width + 2, height + 2);
		
		foreach(FlatTriPoint point in vertexGrid)
		{
			vertexGrid[point] = UnityEngine.Random.Range(0, 2);
		}
		
		foreach (PointyHexPoint point in grid)
		{
			Cell cell = Instantiate<Cell> (cellPrefab);
			Vector2 worldPoint = map[point];
			
			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;	
			
			cell.SetColor(Color.white);
			grid[point] = cell;
			
			SetCellSprite(vertexGrid, point, cell);					
		}
	}	
	
	private void SetCellSprite(FlatTriGrid<int> vertexGrid, PointyHexPoint point, Cell cell)
	{
		var vertices = 
			from vertexPoint in point.GetVertices ()
			select vertexGrid [vertexPoint];
		
		int imageIndex = vertices.Reverse().Aggregate ((x, y) => (x << 1) + y);
		float zRotation = 30;
		
		for (int i = 0; i < 6; i++)
		{
			if (frameIndices [imageIndex] != -1)
			{
				cell.SetFrame (frameIndices [imageIndex]);
				cell.transform.SetRotationZ (zRotation);
				break;
			}
			
			zRotation += 60;
			imageIndex = RotateEdgeNumberClockWise (imageIndex);
		}
	}
	
	public int RotateEdgeNumberClockWise(int edge)
	{
		return ((edge & 1) << 5) + (edge >> 1);
	}
	
	public int RotateEdgeNumberCounterClockWise(int edge)
	{
		return ((edge << 1) & 63) + (edge >> 5); 
	}
}
