using System;
using Microsoft.Xna.Framework;

namespace PolyPhys
{
	public class Circle : PhysObj
	{

		float radius = 0;
		public float Radius
		{
			get { return radius; }
		}

		public Circle(float radius)
		{
			this.radius = radius;
		}

		public override void Scale(float s)
		{
			radius *= s;
		}

		public override void SetDimensions(float width, float height)
		{
			radius = width;
		}

		public override Vector2 TopLeft()
		{
			topLeft.X = Position.X - radius;
			topLeft.Y = Position.Y - radius;
			return topLeft;
		}
	}
}

