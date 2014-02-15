//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using UnityEngine;

[AddComponentMenu("Gamelogic/Cells/MeshCell")]
public class MeshCell : GLMonoBehaviour
{
	private bool on;
	private TextMesh textMesh;

	private Color color;
	private Color highlightColor;

	public Color Color
	{
		get
		{
			return color;
		}

		set
		{
			color = value;
			highlightColor = ExampleUtils.Blend(0.5f, color, Color.white);

			UpdateColor();
		}
	}

	private void UpdateColor()
	{
		var mesh = GetComponent<MeshFilter>().mesh;
		var colors = new Color[mesh.vertexCount];

		for (int i = 0; i < colors.Length; i++)
		{
			colors[i] = HighlightOn ? highlightColor : color;
		}

		mesh.colors = colors;
	}

	public Vector3 Pivot
	{
		set
		{
			textMesh.transform.localPosition = value;
		}
	}


	public string Text
	{
		set
		{
			textMesh.text = value;
		}
	}

	public bool HighlightOn
	{
		get
		{
			return on;
		}

		set
		{
			on = value;

			UpdateColor();
		}
	}

	public void Awake()
	{
		textMesh = GetComponentInChildren<TextMesh>();
	}
}