//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

public class LinesCell : Cell
{
	private int color;
	private bool isEmpty;
	
	public int Color
	{
		get
		{
			return color;
		}
	}
	
	public bool IsEmpty
	{
		get
		{
			return isEmpty;
		}
	}
	
	public void SetState (bool isEmpty, int color)
	{
		this.isEmpty = isEmpty;
		this.color = isEmpty ? -1 : color;
		SetColor(isEmpty ? UnityEngine.Color.white : ExampleUtils.colors[color]);
	}
}