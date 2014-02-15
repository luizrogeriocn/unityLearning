//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

[AddComponentMenu("Gamelogic/Cells/SplicedCell")]
public class SplicedCell : Cell
{
	public void SetOrientation(int index)
	{
		SetFrame(index);
		SetHighlightFrame(index);
	}
}
