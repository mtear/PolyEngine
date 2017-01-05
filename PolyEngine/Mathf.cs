using System;

namespace PolyEngine
{
	public struct Mathf
	{
		public const float PI = 3.14159274f;
		public const float Infinity = float.PositiveInfinity;
		public const float NegativeInfinity = float.NegativeInfinity;
		public const float Deg2Rad = 0.0174532924f;
		public const float Rad2Deg = 57.29578f;

		//TODO
		//public static readonly float Epsilon = (!MathfInternal.IsFlushToZeroEnabled) ? MathfInternal.FloatMinDenormal : MathfInternal.FloatMinNormal;

		public static float Abs(float f)
		{
			return f > 0 ? f : -f;
		}

		public static int Abs(int value)
		{
			return value > 0 ? value : -value;
		}

		public static float Acos(float value)
		{
			return (float)Math.Acos(value);
		}

		public static float Atan2(float v1, float v2)
		{
			return (float)Math.Atan2(v1, v2);
		}
		
		public static float Clamp(float value, float min, float max)
		{
			return (value < min) ? min : (value > max) ? max : value;
		}

		public static int Clamp(int value, int min, int max)
		{
			return (value < min) ? min : (value > max) ? max : value;
		}

		public static float Clamp01(float value)
		{
			if (value > 1) return 1;
			else if (value < 0) return 0;
			else return value;
		}

		public static float Cos(float value)
		{
			return (float)Math.Cos(value);
		}

		public static float InverseLerp(float from, float to, float value)
		{
			if (from < to)
			{
				if (value < from)
				{
					return 0f;
				}
				if (value > to)
				{
					return 1f;
				}
				value -= from;
				value /= to - from;
				return value;
			}
			else
			{
				if (from <= to)
				{
					return 0f;
				}
				if (value < to)
				{
					return 1f;
				}
				if (value > from)
				{
					return 0f;
				}
				return 1f - (value - to) / (from - to);
			}
		}

		public static float Lerp(float from, float to, float t)
		{
			return from + (to - from) * Mathf.Clamp01(t);
		}

		public static float Max(float a, float b)
		{
			if (a > b) return a;
			else return b;
		}

		public static float Min(float a, float b)
		{
			if (a < b) return a;
			else return b;
		}

		public static float Sin(float value)
		{
			return (float)Math.Sin(value);
		}

		public static float Sqrt(float f) {
			return (float)System.Math.Sqrt((double)f);
		}

	}
}

