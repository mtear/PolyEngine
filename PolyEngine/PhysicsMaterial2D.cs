using System;
namespace PolyEngine
{
	public class PhysicsMaterial2D : Object
	{
		public float bounciness = 1f, friction = 0f;

		internal static PhysicsMaterial2D FrictionlessNonElastic()
		{
			return new PhysicsMaterial2D();
		}

	}
}

