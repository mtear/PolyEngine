using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PolyEngine
{
	public class PolySprite
	{
		Texture2D texture;
		Vector2 origin;
		public Vector2 Origin
		{
			get
			{
				return origin;
			}
		}

		public Texture2D Texture
		{
			get
			{
				return texture;
			}
		}

		public PolySprite(string name)
		{
			//texture = Poly._Game.GFX.GetSprite(name);
			//origin.X = texture.Width / 2;
			//origin.Y = texture.Height / 2;
		}

	}
}

