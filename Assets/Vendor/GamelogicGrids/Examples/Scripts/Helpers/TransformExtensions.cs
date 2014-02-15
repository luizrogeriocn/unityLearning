//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;

using UnityEngine;

public static class TransformExtensions
{
	public static void SetRotationZ(this Transform transform, float angle)
	{
		transform.eulerAngles = new Vector3(0, 0, angle);
	}

	public static void SetLocalRotationZ(this Transform transform, float angle)
	{
		transform.localEulerAngles = new Vector3(0, 0, angle);
	}

	public static void RotateAroundZ(this Transform transform, float angle)
	{
		var rotation = new Vector3(0, 0, angle);
		transform.Rotate(rotation);
	}
	
	public static void DestroyChildren(this Transform transform)
	{
		var childCount = transform.GetChildCount();
		
		//Add children to list before destroying
		//otherwise GetChild(i) may bomb out
		var children = new List<Transform>();
		
		for(var i = 0; i < childCount; i++)
		{
			var child = transform.GetChild(i);
			children.Add(child);
		}
		
		foreach(var child in children)
		{
			Object.Destroy(child.gameObject);
		}
	}
}