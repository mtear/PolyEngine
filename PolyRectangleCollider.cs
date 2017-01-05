using System;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace PolyEngine
{
	public class PolyRectangleCollider : PolyCollider
	{

		public RectangleF rect;
		public RectangleF Rect
		{
			get
			{
				return rect;
			}
		}

		public PolyRectangleCollider(Vector2 origin, float width, float height)
			: base(width, height)
		{
			/*rect = new RectangleF(origin.X - width / 2,
			                                    origin.Y - height / 2,
			                                    width,
			                                    height);
			material = new BounceMaterial();*/
		}

		public RectangleF Collides(PolyRectangleCollider col)
		{
			return RectangleF.Intersect(rect, col.Rect);
		}

		protected override void UpdatePhysics()
		{
			/*this.rect.X = origin.X - width / 2;
			this.rect.Y = origin.Y - height / 2;
			this.rect.Width = width;
			this.rect.Height = height;*/
		}
	}
}

