//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	[Serializable]
	/**
		This class provides is a mutable class that can be used to construct
		partial vector points.
	
		It is provided for use in Unity's inspector.
	
		Typical usage us this:
	
		\code
		[Serializable]
		public MyClass
		{
			public InspectableVectorPoint playerStart;
				
			private PointyTriPoint playerPosition;
	
			public void Start()
			{
				playerPosition = playerStart.GetPointyTriPoint();
			}
		}
		\endcode
	
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0

		@ingroup UnityUtilities
	*/
	public class InspectableSplicedVectorPoint
	{
		public int x;
		public int y;
		public int index;
	
		public PointyTriPoint GetPointyTriPoint()
		{
			return new PointyTriPoint(x, y, index);
		}
	
		public FlatTriPoint GetPointyFlatPoint()
		{
			return new FlatTriPoint(x, y, index);
		}
	
		public PointyRhombPoint GetPointyRhombPoint()
		{
			return new PointyRhombPoint(x, y, index);
		}
	
		public FlatRhombPoint GetFlatRhombPoint()
		{
			return new FlatRhombPoint(x, y, index);
		}
	}
}
