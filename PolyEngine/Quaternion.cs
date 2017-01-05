using System;
using XNA = Microsoft.Xna.Framework;

namespace PolyEngine
{
	public struct Quaternion
	{


		#region Public Variables

		public const float kEpsilon = 1E-06f;
		public float x;
		public float y;
		public float z;
		public float w;
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return this.x;
					case 1:
						return this.y;
					case 2:
						return this.z;
					case 3:
						return this.w;
					default:
						throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						this.x = value;
						break;
					case 1:
						this.y = value;
						break;
					case 2:
						this.z = value;
						break;
					case 3:
						this.w = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.FromQ2(this) * 57.29578f;
			}
			set
			{
				if (value.x < 0) value.x = 360 + value.x;
				if (value.y < 0) value.y = 360 + value.y;
				if (value.z < 0) value.z = 360 + value.z;

				this = Quaternion.ToQ(value * 0.0174532924f);
			}
		}

		#endregion Public Variables


		#region Constructors

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		#endregion Constructors


		#region Public Methods

		//Set Set x, y, z and w components of an existing Quaternion.
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		//SetFromToRotation   Creates a rotation which rotates from fromDirection to toDirection.
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			//this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		//SetLookRotation Creates a rotation with the specified forward and upwards directions.
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		public void SetLookRotation(Vector3 view, Vector3 up)
		{
			//this = Quaternion.LookRotation(view, up);
		}

		//ToAngleAxis Converts a rotation to angle-axis representation (angles in degrees).

		//ToString Returns a nicely formatted string of the Quaternion.
		public override string ToString()
		{
			return "(" + x + ", " + y + ", " + z + ", " + w + ")";
		}
		public string ToString(string format)
		{
			return "(" + x + ", " + y + ", " + z + ", " + w + ")";
		}

		#endregion Public Methods


		#region Static Methods

		//Angle Returns the angle in degrees between two rotations a and b.
		public static float Angle(Quaternion a, Quaternion b)
		{
			float f = Quaternion.Dot(a, b);
			return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * 57.29578f;
		}

		//AngleAxis Creates a rotation which rotates angle degrees around axis.

		//Dot The dot product between two rotations.
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		//Euler Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.ToQ(new Vector3(x, y, z) * 0.0174532924f);
		}

		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.ToQ(euler * 0.0174532924f);
		}

		//FromToRotation Creates a rotation which rotates from fromDirection to toDirection.

		//Inverse Returns the Inverse of rotation.
		/*public static Quaternion Inverse(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Inverse(ref rotation);
		}*/

		//Lerp Interpolates between a and b by t and normalizes the result afterwards.The parameter t is clamped to the range [0, 1].
		/*public static Quaternion Lerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_Lerp(ref from, ref to, t);
		}*/

		//LerpUnclamped Interpolates between a and b by t and normalizes the result afterwards.The parameter t is not clamped.

		//LookRotation Creates a rotation with the specified forward and upwards directions.
		/*public static Quaternion LookRotation(Vector3 forward, Vector3 upwards)
		{
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref upwards);
		}*/

		/*public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up);
		}*/

		//RotateTowards Rotates a rotation from towards to.
		/*public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			if (num == 0f)
			{
				return to;
			}
			float t = Mathf.Min(1f, maxDegreesDelta / num);
			return Quaternion.SlerpUnclamped(from, to, t);
		}*/

		//Slerp Spherically interpolates between a and b by t.The parameter t is clamped to the range [0, 1].
		/*public static Quaternion Slerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}*/

		//SlerpUnclamped Spherically interpolates between a and b by t.The parameter t is not clamped.
		/*private static Quaternion SlerpUnclamped(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_UnclampedSlerp(ref from, ref to, t);
		}*/

		#endregion Static Methods


		#region Internal Methods

		internal static Quaternion ToQ(Vector3 v)
		{
			return ToQ(v.y, v.x, v.z);
		}

		internal static Quaternion ToQ(float yaw, float pitch, float roll)
		{
			yaw *= Mathf.Deg2Rad;
			pitch *= Mathf.Deg2Rad;
			roll *= Mathf.Deg2Rad;
			float rollOver2 = roll * 0.5f;
			float sinRollOver2 = (float)Math.Sin((double)rollOver2);
			float cosRollOver2 = (float)Math.Cos((double)rollOver2);
			float pitchOver2 = pitch * 0.5f;
			float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
			float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
			float yawOver2 = yaw * 0.5f;
			float sinYawOver2 = (float)Math.Sin((double)yawOver2);
			float cosYawOver2 = (float)Math.Cos((double)yawOver2);
			Quaternion result;
			result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
			result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
			result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
			result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

			return result;
		}

		internal static Vector3 FromQ2(Quaternion q1)
		{
			float sqw = q1.w * q1.w;
			float sqx = q1.x * q1.x;
			float sqy = q1.y * q1.y;
			float sqz = q1.z * q1.z;
			float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
			float test = q1.x * q1.w - q1.y * q1.z;
			Vector3 v = Vector3.zero;

			if (test > 0.4995f * unit)
			{ // singularity at north pole
				v.y = 2f * Mathf.Atan2(q1.y, q1.x);
				v.x = Mathf.PI / 2;
				v.z = 0;
				return NormalizeAngles(v * Mathf.Rad2Deg);
			}
			if (test < -0.4995f * unit)
			{ // singularity at south pole
				v.y = -2f * Mathf.Atan2(q1.y, q1.x);
				v.x = -Mathf.PI / 2;
				v.z = 0;
				return NormalizeAngles(v * Mathf.Rad2Deg);
			}
			Quaternion q = new Quaternion(q1.w, q1.z, q1.x, q1.y);
			v.y = (float)Math.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
			v.x = (float)Math.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
			v.z = (float)Math.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}

		internal static Vector3 NormalizeAngles(Vector3 angles)
		{
			angles.x = NormalizeAngle(angles.x);
			angles.y = NormalizeAngle(angles.y);
			angles.z = NormalizeAngle(angles.z);
			return angles;
		}

		internal static float NormalizeAngle(float angle)
		{
			while (angle > 360)
				angle -= 360;
			while (angle < 0)
				angle += 360;
			return angle;
		}

		#endregion Internal Methods


		#region Operators

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)obj;
			return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
		}

		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result = Vector3.zero;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) > 0.999999f;
		}

		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) <= 0.999999f;
		}

		#endregion Operators
	}
}
