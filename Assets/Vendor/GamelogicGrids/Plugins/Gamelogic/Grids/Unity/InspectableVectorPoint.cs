//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	/**
		This class provides is a mutable class that can be used to construct
		VectorPoints.

		It is provided for use in Unity's inspector.

		Typical usage us this:

			[Serializable]
			public MyClass
			{
				public InspectableVectorPoint playerStart;
			
				private PointyHexPoint playerPosition;

				public void Start()
				{
					playerPosition = playerStart.GetPointyHexPoint();
				}
			}

		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup UnityUtilities
	*/
	[Serializable]
	public partial class InspectableVectorPoint
	{
		public int x;
		public int y;
	
		public VectorPoint GetVectorPoint()
		{
			return new VectorPoint(x, y);
		}
	}
}