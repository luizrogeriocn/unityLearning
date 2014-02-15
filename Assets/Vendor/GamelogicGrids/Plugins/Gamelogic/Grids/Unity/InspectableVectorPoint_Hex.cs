//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

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
	public partial class InspectableVectorPoint
	{
		public PointyHexPoint GetPointyHexPoint()
		{
			return new PointyHexPoint(x, y);
		}
	
#if !EXPERIMENTAL
		public FlatHexPoint GetFlatHexPoint()
		{
			return new FlatHexPoint(x, y);
		}
#endif
	}
}