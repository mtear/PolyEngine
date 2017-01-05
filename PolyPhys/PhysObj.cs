using System;
using Microsoft.Xna.Framework;

namespace PolyPhys
{
	public abstract class PhysObj
	{
		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;

		protected Vector2 topLeft = Vector2.Zero;

		private float mass = 0;
		public float Mass
		{
			get { return mass; }
			set
			{
				if (value < 0) throw new Exception("Negative mass");
				mass = value;
				if (value == 0) invertedmass = 0;
				else invertedmass = 1f / value;
			}
		}
		private float invertedmass = 0;
		public float InvertedMass
		{
			get { return invertedmass; }
		}

		public float Restitution = 0;

		public abstract void SetDimensions(float width, float height);

		public void SetPosition(Vector2 position)
		{
			Position.X = position.X;
			Position.Y = position.Y;
		}

		public abstract void Scale(float s);

		public void Update(float elapsedTime)
		{
			Velocity += Acceleration * elapsedTime;
			Position += Velocity * elapsedTime;
		}

		public abstract Vector2 TopLeft();

	}
}

