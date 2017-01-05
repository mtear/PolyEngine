using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using PolyPhys;

namespace PolyEngine
{
	public class PolyGameObject
	{
		string name = "";
		public string Name
		{
			get
			{
				return name;
			}
		}

		float scale = 1;
		public float Scale
		{
			get
			{
				return scale;
			}
			set
			{
				scale = value;

				Physics.Scale(value);
			}
		}

		public bool Inert = false;

		PolySprite sprite;
		public PolySprite Sprite
		{
			get
			{
				return sprite;
			}
		}

		public PhysObj Physics;

		public PolyGameObject(string name, PolySprite sprite)
		{
			this.name = name;
			this.sprite = sprite;

			Physics = new AABB(0, 0, sprite.Texture.Width, sprite.Texture.Height);
		}

		public void AddForce(Vector2 force)
		{
			if (Inert) return;
			//Have forces with IDs that can be added and removed when you enter a field
		}

		public void UpdatePhysics(float elapsedTime)
		{
			if (Inert) return;

			Physics.Update(elapsedTime);
		}

		public void SetCollider(PhysObj col)
		{
			Physics = col;
			Physics.Update(0);
		}

		public void Collision(PolyGameObject pgo, RectangleF collision, bool first)
		{
			

		}
	}
}

