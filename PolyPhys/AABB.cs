using System;
using Microsoft.Xna.Framework;

namespace PolyPhys
{
	public class AABB : PhysObj
	{
		public AABB(float x, float y, float width, float height)
		{
			Mass = 1f;
			Position.X = x;
			Position.Y = y;
			this.width = width;
			this.height = height;
		}

		private float width = 0, height = 0;
		public float Width
		{
			get { return width; }
		}
		public float Height
		{
			get { return height; }
		}

		public float Top { get { return Position.Y - Height/2; } }
		public float Left { get { return Position.X - Width/2; } }
		public float Right { get { return Position.X + Width/2; } }
		public float Bottom { get { return Position.Y + Height/2; } }

		public override void SetDimensions(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		public override void Scale(float s)
		{
			SetDimensions(width * s, height * s);
		}

		public override Vector2 TopLeft()
		{
			topLeft.X = Left;
			topLeft.Y = Top;
			return topLeft;
		}
	}
}

