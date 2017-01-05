using System;
namespace PolyEngine
{
	public struct Vector2
	{


		#region Static Fields

		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}
		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}
		public static Vector2 right
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		#endregion Static Fields


		#region Public Fields

		public float x
		{
			get
			{
				return components[0];
			}
			set
			{
				components[0] = value;
				RecalculatePrivateVariables();
			}
		}

		public float y
		{
			get
			{
				return components[1];
			}
			set
			{
				components[1] = value;
				RecalculatePrivateVariables();
			}
		}

		public float magnitude
		{
			get
			{
				return mMagnitude;
			}
		}

		//normalized Returns this vector with a magnitude of 1 (Read Only).
		public Vector2 normalized
		{
			get
			{
				if (mMagnitude == 0) return new Vector2(0, 0);
				return new Vector2(x / magnitude, y / magnitude);
			}
		}

		public float this[int i]
		{
			get { return components[i]; }
			set { components[i] = value; RecalculatePrivateVariables(); }
		}

		#endregion Public Fields


		#region Private Fields

		private Microsoft.Xna.Framework.Vector2 mXNA;

		private float mMagnitude, mSqrMagnitude;

		private float[] components;

		#endregion Private Fields


		#region Constructors

		public Vector2(float x, float y)
		{
			components = new float[2];
			components[0] = x;
			components[1] = y;

			mSqrMagnitude = x * x + y * y;
			mMagnitude = Mathf.Sqrt(mSqrMagnitude);

			mXNA.X = x;
			mXNA.Y = y;
		}

		#endregion Constructors


		#region Public Methods

		//Normalize
		public void Normalize()
		{
			Set(normalized.x, normalized.y);
		}

		//Set Set x, y and z components of an existing Vector2
		public void Set(float new_x, float new_y)
		{
			components[0] = new_x;
			y = new_y;
		}

		#endregion Public Methods


		#region Internal Methods

		internal void RecalculatePrivateVariables() //todo make private
		{
			mSqrMagnitude = x * x + y * y;
			mMagnitude = Mathf.Sqrt(mSqrMagnitude);
		}

		internal Microsoft.Xna.Framework.Vector2 ToXNA()
		{
			mXNA.X = x;
			mXNA.Y = y;
			return mXNA;
		}

		#endregion Internal Methods


		#region Static Methods

		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		#endregion Static Methods


		#region Operators

		public override bool Equals(object obj)
		{
			if (obj == null) return false;

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		#endregion Operators

	}
}

