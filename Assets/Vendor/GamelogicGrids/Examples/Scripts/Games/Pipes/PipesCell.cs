//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using Gamelogic;

public class PipesCell : Cell
{
	private List<int> edgeData;
	
	public List<int> EdgeData
	{
		get
		{
			return edgeData;
		}
		
		set
		{
			edgeData = value;
		}
	}
	
	public MonoBehaviour Image
	{
		get
		{
			return image;
		}
	}
	
	public void RotateCW()
	{
		var newEdgeData = edgeData.ButFirst().ToList();
		
		newEdgeData.Add(edgeData.First());
		edgeData = newEdgeData;
		image.transform.RotateAroundZ(-60);
	}
	
	public void RotateCCW()
	{
		List<int> newEdgeData = edgeData.ButLast().ToList();
		
		newEdgeData.Insert(0, edgeData.Last());
		edgeData = newEdgeData;
		image.transform.RotateAroundZ(60);
	}	
}