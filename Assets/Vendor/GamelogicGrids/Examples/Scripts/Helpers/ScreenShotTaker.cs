//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using UnityEngine;

public class ScreenShotTaker : GLMonoBehaviour
{
	public const KeyCode ScreenShotKey = KeyCode.F9;
	
	private int screenshotCount;

	public void Start()
	{
		if(!PlayerPrefs.HasKey("code-spot::screenshotCount"))
		{
			PlayerPrefs.SetInt("code-spot::screenshotCount", 0);
		}
		else
		{
			screenshotCount = PlayerPrefs.GetInt("code-spot::screenshotCount");
		}
	}
	
	public void Update()
	{
		if(Input.GetKeyDown(ScreenShotKey))
		{
			string path = "screen_" + screenshotCount +".png";
			Application.CaptureScreenshot(path);	
			
			screenshotCount++;
			PlayerPrefs.SetInt("code-spot::screenshotCount", screenshotCount);
		}
	}
}
