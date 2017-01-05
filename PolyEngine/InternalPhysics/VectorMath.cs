using System;
namespace PolyEngine.InternalPhysics
{
	internal class VectorMath
	{

		public static float EPSILON = 0.00001f;
		public static float BIAS_RELATIVE = 0.95f;
		public static float BIAS_ABSOLUTE = 0.01f;

		public static Vector2 cross(Vector2 v, float a, Vector2 o )
		{
			o.x = v.y * a;
			o.y = v.x * -a;
			return o;
		}

		public static Vector2 cross( Vector2 v, float a )
		{
  			return new Vector2(a* v.y, -a* v.x );
		}

		public static Vector2 cross(float a, Vector2 v )
		{
			return new Vector2(-a * v.y, a * v.x);
		}

		public static float cross( Vector2 a, Vector2 b )
		{
			return a.x * b.y - a.y * b.x;
		}

		public static bool gt(float a, float b)
		{
			return a >= b * BIAS_RELATIVE + a * BIAS_ABSOLUTE;
		}
	}
}

