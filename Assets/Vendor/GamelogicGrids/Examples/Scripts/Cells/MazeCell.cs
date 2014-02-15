//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

[AddComponentMenu("Gamelogic/Cells/MazeCell")]
public class MazeCell : Cell
{
	public void SetOrientation(int index, bool open)
	{
		SetFrame(index + (open ? 3 : 0));
		SetHighlightFrame(index);
	}
}