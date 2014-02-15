//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Linq;

using UnityEngine;

[AddComponentMenu("Gamelogic/Cells/Cell")]
public class Cell : GLMonoBehaviour 
{	
	public string[] spriteNames;
	
	protected UISprite image;
	protected UISprite highlight;
	protected UILabel spriteText;
	
	private bool highlightOn;
	
	public bool HighlightOn
	{
		set
		{
			highlightOn = value;
			highlight.enabled = value;
		}
		
		get
		{
			return highlightOn;
		}			
	}
	
	public void Awake()
	{
		image = GetComponentsInChildren<UISprite>().First(x => x.name == "Image");
		highlight = GetComponentsInChildren<UISprite>().First(x => x.name == "Highlight");
		spriteText = GetComponentInChildren<UILabel>();
		
		HighlightOn = false;
		
	}
	
	public void SetColor(Color color)
	{
		image.color = color;
	}
	
	public void SetText(string newText)
	{
		spriteText.text = newText;
	}
	
	public void SetFrame(int frameIndex)
	{
		image.spriteName = spriteNames[frameIndex];
		image.MarkAsChanged();
	}
	
	public void SetHighlightFrame(int frameIndex)
	{
		highlight.spriteName = spriteNames[frameIndex];
		image.MarkAsChanged();
	}
}
