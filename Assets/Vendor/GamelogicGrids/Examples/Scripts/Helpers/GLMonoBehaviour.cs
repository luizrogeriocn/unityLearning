//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

/**
	Provides some additional functions for MonoBehaviour, 
	in particular type safe delagates.
	
	@copyright Gamelogic.
	@author Herman Tulleken
*/
public class GLMonoBehaviour : MonoBehaviour
{	
	#region Typesafe instatiation
	public static T Instantiate<T>(T obj) where T : MonoBehaviour
	{
		return (T) Object.Instantiate(obj);
	}
	
	public static GameObject Instantiate(GameObject obj)
	{
		return (GameObject) Object.Instantiate(obj);
	}
	#endregion
}


