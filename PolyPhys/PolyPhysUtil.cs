using System;
using Microsoft.Xna.Framework;

namespace PolyPhys
{
	public static class PolyPhysUtil
	{
		public static Vector2 GetNormal(Vector2 a, Vector2 b)
		{
			Vector2 ret = (b - a);
			ret.Normalize();
			return ret;
		}

		public static float DotProduct(Vector2 x, Vector2 y)
		{
			return x.X * y.X + x.Y * y.Y;
		}
	}
}

