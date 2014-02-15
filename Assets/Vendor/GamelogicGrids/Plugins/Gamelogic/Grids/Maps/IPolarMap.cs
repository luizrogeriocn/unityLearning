namespace Gamelogic.Grids
{
	interface IPolarMap<TPoint>
	{
		/**
			Returns the Z angle in degrees of the given grid point at 
			the start of the sector.

			This is useful for making a mesh for the sector band, for instance.
		*/
		float GetStartAngleZ(TPoint gridPoint);

		/**
			Returns the Z angle in degrees of the given grid point at 
			the end of the sector.

			This is useful for making a mesh for the sector band, for instance.
		*/
		float GetEndAngleZ(TPoint gridPoint);

		/**
			Gets the inside radius of the band ath the given grid point.
		*/
		float GetInnerRadius(TPoint gridPoint);

		/**
			Gets the outside radius of the band ath the given grid point.
		*/
		float GetOuterRadius(TPoint gridPoint);
	}
}
