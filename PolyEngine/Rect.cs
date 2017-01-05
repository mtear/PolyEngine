﻿using System;

namespace PolyEngine
{
	public struct Rect
	{
		private float m_XMin;
		private float m_YMin;
		private float m_Width;
		private float m_Height;

		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		public Microsoft.Xna.Framework.Rectangle XNARectangle
		{
			get
			{
				mXNARectangle.X = (int)x;
				mXNARectangle.Y = (int)y;
				mXNARectangle.Width = (int)width;
				mXNARectangle.Height = (int)height;
				return mXNARectangle;
			}
		}

		private Microsoft.Xna.Framework.Rectangle mXNARectangle;

		public Rect(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;

			mXNARectangle = new Microsoft.Xna.Framework.Rectangle((int)left,
			                                                      (int)top,
			                                                      (int)width,
			                                                      (int)height);
		}

		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;

			mXNARectangle = source.mXNARectangle;
		}

		public static Rect MinMaxRect(float left, float top, float right, float bottom)
		{
			return new Rect(left, top, right - left, bottom - top);
		}

		public void Set(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		public override string ToString()
		{
			return "(x:" + this.x + ", y:" + this.y + ", width:" + this.width + ", height:" + this.height + ")";
		}

		public string ToString(string format)
		{
			return ToString(); //TODO
		}

		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		public bool Contains(Vector3 point, bool allowInverse)
		{
			if (!allowInverse)
			{
				return this.Contains(point);
			}
			bool flag = false;
			if ((this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax))
			{
				flag = true;
			}
			return flag && ((this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax));
		}

		private static Rect OrderMinMax(Rect rect)
		{
			if (rect.xMin > rect.xMax)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			if (rect.yMin > rect.yMax)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.width.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^ this.height.GetHashCode() >> 1;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Rect))
			{
				return false;
			}
			Rect rect = (Rect)obj;
			return this.x.Equals(rect.x) && this.y.Equals(rect.y) && this.width.Equals(rect.width) && this.height.Equals(rect.height);
		}

		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x ||
			       lhs.y != rhs.y ||
			       lhs.width != rhs.width ||
			       lhs.height != rhs.height;
		}

		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x &&
			       lhs.y == rhs.y && 
			       lhs.width == rhs.width && 
			       lhs.height == rhs.height;
		}
	}
}