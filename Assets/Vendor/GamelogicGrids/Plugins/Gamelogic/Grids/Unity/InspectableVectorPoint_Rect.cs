//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

namespace Gamelogic.Grids
{
	public partial class InspectableVectorPoint
	{
		public RectPoint GetRectPoint()
		{
			return new RectPoint(x, y);
		}
	
		public DiamondPoint GetDiamondPoint()
		{
			return new DiamondPoint(x, y);
		}
	}
}