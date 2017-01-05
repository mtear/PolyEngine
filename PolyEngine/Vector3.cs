using XNA = Microsoft.Xna.Framework;

namespace PolyEngine
{
	public struct Vector3
	{


		#region Static Variables

		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}
		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}
		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}
		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}
		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}
		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}
		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		#endregion Static Variables


		#region Public Variables

		//magnitude Returns the length of this vector (Read Only).
		public float magnitude
		{
			get
			{
				return mMagnitude;
			}
		}

		//normalized Returns this vector with a magnitude of 1 (Read Only).
		public Vector3 normalized
		{
			get
			{
				if (mMagnitude == 0) return new Vector3(0, 0, 0);
				return new Vector3(x / magnitude, y / magnitude, z / magnitude);
			}
		}

		//sqrMagnitude Returns the squared length of this vector (Read Only).
		public float sqrMagnitude
		{
			get
			{
				return mSqrMagnitude;
			}
		}

		//this[int]	Access the x, y, z components using [0], [1], [2] respectively.
		public float this[int i]
		{
			get { return components[i]; }
			set { components[i] = value; RecalculatePrivateVariables(); }
		}

		//x X component of the vector.
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

		//y   Y component of the vector.
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

		//z Z component of the vector.
		public float z
		{
			get
			{
				return components[2];
			}
			set
			{
				components[2] = value;
				RecalculatePrivateVariables();
			}
		}

		#endregion Public Variables


		#region Private Variables

		private float mMagnitude, mSqrMagnitude;
		private float[] components;

		private XNA.Vector2 mXNA2D;

		#endregion Private Variables


		#region Constructors

		public Vector3(float x, float y, float z)
		{
			components = new float[3];
			components[0] = x;
			components[1] = y;
			components[2] = z;

			mSqrMagnitude = x * x + y * y + z * z;
			mMagnitude = Mathf.Sqrt(mSqrMagnitude);

			mXNA2D.X = x;
			mXNA2D.Y = y;
		}

		#endregion Constructors


		#region Public Methods

		public override bool Equals(object obj)
		{
			if (obj is Vector3)
			{
				return Equals((Vector3)obj);
			}

			return false;
		}

		public bool Equals(Vector3 other)
		{
			return (this - other).sqrMagnitude < 0.0001f;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() + y.GetHashCode();
		}

		//Normalize
		public void Normalize()
		{
			Set(normalized.x, normalized.y, normalized.z);
		}

		//Set Set x, y and z components of an existing Vector3.
		public void Set(float new_x, float new_y, float new_z)
		{
			components[0] = new_x;
			components[1] = new_y;
			z = new_z;
		}

		internal XNA.Vector2 To2DXNAVector()
		{
			mXNA2D.X = x;
			mXNA2D.Y = y;
			return mXNA2D;
		}

		//ToString    Returns a nicely formatted string for this vector.
		public override string ToString()
		{
			return "(" + x + ", " + y + ", " + z + ")";
		}

		#endregion Public Methods


		#region Private Methods

		private void RecalculatePrivateVariables()
		{
			mSqrMagnitude = x * x + y * y + z * z;
			mMagnitude = Mathf.Sqrt(mSqrMagnitude);

			mXNA2D.X = x;
			mXNA2D.Y = y;
		}

		#endregion Private Methods


		#region Static Methods

		//Angle Returns the angle in degrees between from and to.
		public static float Angle(Vector3 from, Vector3 to)
		{
			float angle = Mathf.Acos(Dot(from, to) / (from.magnitude * to.magnitude));

			if (angle <= 180) return angle;
			else return 360 - angle;
		}

		//ClampMagnitude Returns a copy of vector with its magnitude clamped to maxLength.
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			return vector.normalized * maxLength;
		}

		//Cross   Cross Product of two vectors.
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y,
							   lhs.z * rhs.x - lhs.x * rhs.z,
							   lhs.x * rhs.y - lhs.y * rhs.x);
		}

		//Distance Returns the distance between a and b.
		public static float Distance(Vector3 a, Vector3 b)
		{
			return (a - b).magnitude;
		}

		//Dot Dot Product of two vectors.
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y + rhs.y + lhs.z + rhs.z;
		}

		//Lerp Linearly interpolates between two vectors.
		public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
		{
			return LerpUnclamped(from, to, Mathf.Clamp01(t));
		}

		//LerpUnclamped Linearly interpolates between two vectors.
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t,
						   a.y + (b.y - a.y) * t,
						   a.z + (b.z - a.z) * t);
		}

		//Max Returns a vector that is made from the largest components of two vectors.
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x),
							   Mathf.Min(lhs.y, rhs.y),
			                   Mathf.Min(lhs.z, rhs.z));
		}

		//Min Returns a vector that is made from the smallest components of two vectors.
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x),
							   Mathf.Max(lhs.y, rhs.y),
							   Mathf.Max(lhs.z, rhs.z));
		}

		//MoveTowards Moves a point current in a straight line towards a target point.
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 v = target - current;
			if (v.magnitude <= maxDistanceDelta || v.magnitude == 0)
			{
				return target;
			}
			return current + v / v.magnitude * maxDistanceDelta;
		}

		//OrthoNormalize  Makes vectors normalized and orthogonal to each other.
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			normal.Normalize();
			tangent.Normalize();
			//TODO
		}

		//Project Projects a vector onto another vector.
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			/*float num = Vector3.Dot(onNormal, onNormal);
			if (num < Mathf.Epsilon)
			{
				return Vector3.zero;
			}
			return onNormal * Vector3.Dot(vector, onNormal) / num;*/
			return new Vector3(0,0,0); //TODO
		}

		//ProjectOnPlane Projects a vector onto a plane defined by a normal orthogonal to the plane.
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			return vector - Vector3.Project(vector, planeNormal);
		}

		//Reflect Reflects a vector off the plane defined by a normal.
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		//RotateTowards Rotates a vector current towards target.

		//Scale Multiplies two vectors component-wise.
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		//Slerp Spherically interpolates between two vectors.

		//SlerpUnclamped Spherically interpolates between two vectors.

		//SmoothDamp Gradually changes a vector towards a desired goal over time.
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity,
								 float smoothTime)
		{
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, Mathf.Infinity, Time.deltaTime);
		}

		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity,
								 float smoothTime, float maxSpeed)
		{
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, Time.deltaTime);
		}

		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity,
		                                 float smoothTime, float maxSpeed,
		                                 float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector3 vector = current - target;
			Vector3 vector2 = target;
			float maxLength = maxSpeed * smoothTime;
			vector = Vector3.ClampMagnitude(vector, maxLength);
			target = current - vector;
			Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * d;
			Vector3 vector4 = target + (vector + vector3) * d;
			if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		#endregion Static Methods


		#region Operators

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}

		#endregion Operators


	}
}

