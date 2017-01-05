using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PolyEngine
{
	public class PolyGraphics
	{
		GraphicsDeviceManager graphicsManager;
		SpriteBatch spriteBatch;

		public int ScreenWidth
		{
			get
			{
				return graphicsManager.GraphicsDevice.Viewport.Width;
			}
		}
		public int ScreenHeight
		{
			get
			{
				return graphicsManager.GraphicsDevice.Viewport.Height;
			}
		}

		Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

		public PolyGraphics(GraphicsDeviceManager graphicsManager,
		                   SpriteBatch spriteBatch)
		{
			this.graphicsManager = graphicsManager;
			this.spriteBatch = spriteBatch;
		}

		public void LoadSprite(string name)
		{
			//if (!sprites.ContainsKey(name))
			//	sprites.Add(name, Poly._Game.Content.Load<Texture2D>(name));
		}

		public Texture2D GetSprite(string name)
		{
			if (sprites.ContainsKey(name)) return sprites[name];
			else {
				LoadSprite(name);
				return GetSprite(name);
			}
		}

		public void DrawSprite(PolyGameObject pgo)
		{
			//spriteBatch.Draw(pgo.Sprite.Texture, pgo.Physics.TopLeft(), null, Color.White,
			  //               0, new Vector2(0,0), pgo.Scale, SpriteEffects.None, 0);
		}

		public void DrawTexture(string name, Vector2 position)
		{
			//spriteBatch.Draw(GetSprite(name), position, Color.White);
		}
	}
}

