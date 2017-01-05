using System;
namespace PolyEngine.InternalPhysics
{
	public class Mat2
	{

		public float m00, m01;
		public float m10, m11;

		public Mat2()
		{
		}

		public Mat2(float radians)
		{
			set(radians);
		}

		public Mat2(float a, float b, float c, float d)
		{
			set(a, b, c, d);
		}

		/**
		 * Sets this matrix to a rotation matrix with the given radians.
		 */
		public void set(float radians)
		{
			float c = (float)Mathf.Cos(radians);
			float s = (float)Mathf.Sin(radians);

			m00 = c;
			m01 = -s;
			m10 = s;
			m11 = c;
		}

		/**
		 * Sets the values of this matrix.
		 */
		public void set(float a, float b, float c, float d)
		{
			m00 = a;
			m01 = b;
			m10 = c;
			m11 = d;
		}

		/**
		 * Sets this matrix to have the same values as the given matrix.
		 */
		public void set(Mat2 m)
		{
			m00 = m.m00;
			m01 = m.m01;
			m10 = m.m10;
			m11 = m.m11;
		}

		/**
		 * Sets the values of this matrix to their absolute value.
		 */
		public void absi()
		{
			abs(this);
		}

		/**
		 * Returns a new matrix that is the absolute value of this matrix.
		 */
		public Mat2 abs()
		{
			return abs(new Mat2());
		}

		/**
		 * Sets out to the absolute value of this matrix.
		 */
		public Mat2 abs(Mat2 o )
		{
			o.m00 = Mathf.Abs(m00);
			o.m01 = Mathf.Abs(m01);
			o.m10 = Mathf.Abs(m10);
			o.m11 = Mathf.Abs(m11);
			return o;
		}

		/**
		 * Sets out to the x-axis (1st column) of this matrix.
		 */
		public Vector2 getAxisX(Vector2 o )
		{
			o.x = m00;
			o.y = m10;
			return o;
		}

		/**
		 * Returns a new vector that is the x-axis (1st column) of this matrix.
		 */
		public Vector2 getAxisX()
		{
			return getAxisX(Vector2.zero);
		}

		/**
		 * Sets out to the y-axis (2nd column) of this matrix.
		 */
		public Vector2 getAxisY(Vector2 o )
		{
			o.x = m01;
			o.y = m11;
			return o;
		}

		/**
		 * Returns a new vector that is the y-axis (2nd column) of this matrix.
		 */
		public Vector2 getAxisY()
		{
			return getAxisY(Vector2.zero);
		}

		/**
		 * Sets the matrix to it's transpose.
		 */
		public void transposei()
		{
			float t = m01;
			m01 = m10;
			m10 = t;
		}

		/**
		 * Sets out to the transpose of this matrix.
		 */
		public Mat2 transpose(Mat2 o )
		{
			o.m00 = m00;
			o.m01 = m10;
			o.m10 = m01;
			o.m11 = m11;
			return o;
		}

		/**
		 * Returns a new matrix that is the transpose of this matrix.
		 */
		public Mat2 transpose()
		{
			return transpose(new Mat2());
		}

		/**
		 * Transforms v by this matrix.
		 */
		public Vector2 muli(Vector2 v)
		{
			return mul(v.x, v.y, v);
		}

		/**
		 * Sets out to the transformation of v by this matrix.
		 */
		public Vector2 mul(Vector2 v, Vector2 o )
		{
			return mul(v.x, v.y, o );
		}

		/**
		 * Returns a new vector that is the transformation of v by this matrix.
		 */
		public Vector2 mul(Vector2 v)
		{
			return mul(v.x, v.y, Vector2.zero);
		}

		/**
		 * Sets out the to transformation of {x,y} by this matrix.
		 */
		public Vector2 mul(float x, float y, Vector2 o )
		{
			o.x = m00 * x + m01 * y;
			o.y = m10 * x + m11 * y;
			return o;
		}

		/**
		 * Multiplies this matrix by x.
		 */
		public void muli(Mat2 x)
		{
			set(
				m00 * x.m00 + m01 * x.m10,
				m00 * x.m01 + m01 * x.m11,
				m10 * x.m00 + m11 * x.m10,
				m10 * x.m01 + m11 * x.m11);
		}

		/**
		 * Sets out to the multiplication of this matrix and x.
		 */
		public Mat2 mul(Mat2 x, Mat2 o )
		{
			o.m00 = m00 * x.m00 + m01 * x.m10;
			o.m01 = m00 * x.m01 + m01 * x.m11;
			o.m10 = m10 * x.m00 + m11 * x.m10;
			o.m11 = m10 * x.m01 + m11 * x.m11;
			return o;
		}

		/**
		 * Returns a new matrix that is the multiplication of this and x.
		 */
		public Mat2 mul(Mat2 x)
		{
			return mul(x, new Mat2());
		}

		public static Vector2 operator *(Mat2 a, Vector2 b)
		{
			Vector2 o = Vector2.zero;
			o.x = a.m00 * b.x + a.m01 * b.y;
			o.y = a.m10 * b.x + a.m11 * b.y;
			return o;
		}

	}
}

