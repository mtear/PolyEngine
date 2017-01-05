using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PolyEngine
{
	public abstract class PolyCollider
	{
		public float width, height;
		protected Vector2 origin;

		public List<PolyGameObject> CurrentCollisions = new List<PolyGameObject>();
		public List<PolyGameObject> PendingCollisions = new List<PolyGameObject>();

		protected PolyMaterial material;
		public PolyMaterial Material
		{
			get
			{
				return material;
			}
		}

		public PolyCollider(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		public virtual void UpdateOrigin(Vector2 origin)
		{
			//this.origin.X = origin.X;
			//this.origin.Y = origin.Y;
			UpdatePhysics();
		}

		public void Scale(float scale)
		{
			this.width *= scale;
			this.height *= scale;
			UpdatePhysics();
		}

		public float Elasticity
		{
			get
			{
				return material.Elasticity();
			}
		}

		public void Update()
		{
			for (int i = 0; i < PendingCollisions.Count; i++)
			{
				PolyGameObject pgo = PendingCollisions[i];
				if (!CurrentCollisions.Contains(pgo))
				{
					PendingCollisions.RemoveAt(i--);
				}
				else { //if current contains pgo
					CurrentCollisions.Remove(pgo);
				}
			}

			//Add remaining currente to pending
			PendingCollisions.AddRange(CurrentCollisions);

			CurrentCollisions.Clear();

			UpdatePhysics();
		}

		protected abstract void UpdatePhysics();
	}
}

