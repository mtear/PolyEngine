using System;
using Microsoft.Xna.Framework;

namespace PolyPhys
{
	public struct Manifold
	{
		public PhysObj A, B;
		public float PenetrationDepth;
		public Vector2 Normal;
		public bool AreColliding;
	}
}

