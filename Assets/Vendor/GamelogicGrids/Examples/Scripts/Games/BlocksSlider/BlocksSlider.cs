//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlocksSlider : GLMonoBehaviour
{	
	private enum BlockType
	{
		Vertical,
		Horizontal
	}

	private class Block
	{
		private readonly Dictionary<RectPoint, Cell> tileObjects;
		private readonly int blockID;

		public int ID
		{
			get
			{
				return blockID;
			}
		}

		public IEnumerable<RectPoint> TilePoints
		{
			get
			{
				return tileObjects.Keys;
			}
		}

		public IEnumerable<Cell> TileObjects
		{
			get
			{
				return tileObjects.Values;
			}
		}

		public RectPoint CurrentPosition
		{
			get;
			set;
		}

		public Block(int blockID)
		{
			tileObjects = new Dictionary<RectPoint, Cell>();
			this.blockID = blockID;
		}

		//All tile points are relative to CurrentPosition
		public void AddTile(RectPoint tilePoint, Cell tileObject)
		{
			tileObjects[tilePoint] = tileObject;
		}

		public void UpdateTilePositions(IMap3D<RectPoint> map)
		{
			foreach (RectPoint tilePoint in TilePoints)
			{
				tileObjects[tilePoint].transform.localPosition = map[CurrentPosition + tilePoint];
			}
		}

		public void Clear()
		{
			tileObjects.Clear();
		}
	}
	
	public static readonly Color[] colors =
	{		
		ExampleUtils.ColorFromInt(42, 192, 217),
		
		ExampleUtils.ColorFromInt(114, 197, 29),
		ExampleUtils.ColorFromInt(247, 188, 0),
		ExampleUtils.ColorFromInt(215, 55, 82),
		
		ExampleUtils.ColorFromInt(198, 224, 34),
		ExampleUtils.ColorFromInt(255, 215, 87),
		ExampleUtils.ColorFromInt(228, 120, 129),	
		
		ExampleUtils.ColorFromInt(114, 197, 29),
		ExampleUtils.ColorFromInt(247, 188, 0),
		ExampleUtils.ColorFromInt(215, 55, 82)
	};
	
	public GameObject uiRoot;
	public Cell cellPrefab;

	private RectGrid<Cell> tileGrid;
	private RectGrid<Block> blocksGrid;
	private IMap3D<RectPoint> map;
	private Block currentBlock;
	private int currentBlockIndex;
	private List<Block> blocks;
	private RectPoint winPosition;

	private readonly Dictionary<KeyCode, RectPoint> keysToDirections = new Dictionary<KeyCode, RectPoint>
	{
		{KeyCode.UpArrow, RectPoint.North},
		{KeyCode.LeftArrow, RectPoint.West},
		{KeyCode.DownArrow, RectPoint.South},
		{KeyCode.RightArrow, RectPoint.East},
	};

	public void Start()
	{
		BuildPuzzle();
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ProcessMouse();
		}

		foreach (KeyCode key in keysToDirections.Keys.Where(Input.GetKeyDown))
		{
			ProcessDirectionKey(key);
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			ProcessTabKey();
		}

		if (blocksGrid[winPosition] != null && blocksGrid[winPosition].ID == 0)
		{
			Debug.Log("Game finished!");
		}
	}

	private void ProcessTabKey()
	{
		SetHighlight(currentBlock, false);

		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			currentBlockIndex--;
		}
		else
		{
			currentBlockIndex++;
		}

		currentBlockIndex = currentBlockIndex + blocks.Count()%blocks.Count();
		currentBlock = blocks[currentBlockIndex];
		SetHighlight(currentBlock, true);
	}

	private void ProcessDirectionKey(KeyCode key)
	{
		RectPoint newPoint = currentBlock.CurrentPosition + keysToDirections[key];

		if (CanOccupy(currentBlock, newPoint))
		{
			RemoveTile(currentBlock);
			PlaceTile(currentBlock, newPoint);
			currentBlock.UpdateTilePositions(map);
		}
	}

	private void ProcessMouse()
	{
		Vector3 worldPosition = ExampleUtils.ScreenToWorld_NGUI(uiRoot, Input.mousePosition);
		RectPoint rectPosition = map[worldPosition];

		if (tileGrid.Contains(rectPosition))
		{
			if (blocksGrid[rectPosition] != null)
			{
				SetHighlight(currentBlock, false);
				currentBlock = blocksGrid[rectPosition];
				SetHighlight(currentBlock, true);
			}
		}
	}

	private void BuildPuzzle()
	{
		const int size = 5;

		tileGrid = RectGrid<Cell>.Rectangle(size, size);
		blocksGrid = (RectGrid<Block>)tileGrid.CloneStructure<Block>();

		map = new RectMap(new Vector2(40, 40))
			.AnchorCellMiddleCenter()
			.WithWindow(ExampleUtils.ScreenRect)
			.AlignMiddleCenter(tileGrid)
			.To3DXY();

		foreach (RectPoint point in tileGrid)
		{
			var cell = MakeCell(point);

			tileGrid[point] = cell;
			blocksGrid[point] = null;
		}

		blocks = new List<Block>();

		AddFirstBlock();
		AddOtherBlocks();

		winPosition = new RectPoint(4, 2);
	}

	private void AddFirstBlock()
	{
		currentBlock = MakeBlock(0, BlockType.Horizontal, new RectPoint(2, 2));
		SetHighlight(currentBlock, true);
		blocks.Add(currentBlock);
	}

	private void AddOtherBlocks()
	{
		blocks.Add(MakeBlock(1, BlockType.Horizontal, new RectPoint(1, 1)));
		blocks.Add(MakeBlock(2, BlockType.Vertical, new RectPoint(4, 2)));
		blocks.Add(MakeBlock(3, BlockType.Horizontal, new RectPoint(3, 4)));
		blocks.Add(MakeBlock(4, BlockType.Vertical, new RectPoint(2, 3)));
		blocks.Add(MakeBlock(5, BlockType.Vertical, new RectPoint(4, 0)));
		blocks.Add(MakeBlock(6, BlockType.Vertical, new RectPoint(3, 0)));
		blocks.Add(MakeBlock(7, BlockType.Vertical, new RectPoint(0, 0)));
		blocks.Add(MakeBlock(8, BlockType.Vertical, new RectPoint(0, 2)));
		blocks.Add(MakeBlock(9, BlockType.Vertical, new RectPoint(1, 2)));
	}

	private Cell MakeCell(RectPoint point)
	{
		var cell = Instantiate<Cell>(cellPrefab);
		cell.transform.parent = uiRoot.transform;
		cell.transform.localScale = Vector3.one;
		cell.transform.localPosition = map[point] + Vector3.forward;
		cell.SetColor(ExampleUtils.ColorFromInt(40, 40, 40));
		cell.HighlightOn = false;
		cell.SetText("");
		return cell;
	}

	private bool CanOccupy(Block block, RectPoint gridPoint)
	{
		foreach (RectPoint tilePoint in block.TilePoints)
		{
			var absoluteTilePoint = gridPoint + tilePoint;

			if (!tileGrid.Contains(absoluteTilePoint))
			{
				return false;
			}

			if ((blocksGrid[absoluteTilePoint] != null) && (blocksGrid[absoluteTilePoint] != block))
			{
				return false;
			}
		}

		return true;
	}

	private Cell MakeTile()
	{
		Cell cell = Instantiate(cellPrefab);
		cell.transform.parent = uiRoot.transform;
		cell.transform.localScale = Vector3.one;

		return cell;
	}

	private Block MakeBlock(int id, BlockType type, RectPoint initialPosition)
	{
		var block = new Block(id);

		if (type == BlockType.Horizontal)
		{
			block.AddTile(RectPoint.Zero, MakeTile());
			block.AddTile(RectPoint.East, MakeTile());
		}
		else
		{
			block.AddTile(RectPoint.Zero, MakeTile());
			block.AddTile(RectPoint.North, MakeTile());
		}

		if (CanOccupy(block, initialPosition))
		{
			foreach (Cell tile in block.TileObjects)
			{
				tile.SetColor(colors[id]);
				tile.SetText("");
			}

			PlaceTile(block, initialPosition);
			block.UpdateTilePositions(map);
		}
		else
		{
			Debug.LogError("Invalid placement. Making block empty");

			foreach (Cell tile in block.TileObjects)
			{
				Destroy(tile.gameObject);
			}

			block.Clear();
		}

		return block;
	}

	private void PlaceTile(Block block, RectPoint gridPoint)
	{
		foreach (RectPoint tilePoint in block.TilePoints)
		{
			tileGrid[gridPoint + tilePoint].gameObject.active = false;
			blocksGrid[gridPoint + tilePoint] = block;
		}

		block.CurrentPosition = gridPoint;
	}

	private void RemoveTile(Block block)
	{
		foreach (RectPoint tilePoint in block.TilePoints)
		{
			tileGrid[block.CurrentPosition + tilePoint].gameObject.active = true;
			blocksGrid[block.CurrentPosition + tilePoint] = null;
		}
	}

	private static void SetHighlight(Block block, bool highlightOn)
	{
		foreach (Cell tile in block.TileObjects)
		{
			tile.HighlightOn = highlightOn;
		}
	}	
}