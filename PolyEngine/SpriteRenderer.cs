using System;
using PolyEngine.Rendering;

using XNA = Microsoft.Xna.Framework;

namespace PolyEngine
{
	public class SpriteRenderer : Renderer
	{


		#region Public Variables

		//color Rendering color for the Sprite graphic.
		public XNA.Color color = XNA.Color.White;

		//flipX Flips the sprite on the X axis.

		//flipY   Flips the sprite on the Y axis.

		//sprite The Sprite to render.
		public Sprite sprite = null;

		#endregion Public Variables


		#region Protected Methods

		protected override Bounds GetBounds()
		{
			if (sprite == null) return new Bounds(new Vector3(0,0,0), new Vector3(0,0,0) );
			return sprite.bounds; //TODO make it scale so render doesn't need that shit
		}

		#endregion Protected Methods


		#region Internal Methods

		internal override void OnRender()
		{
			if (sprite == null) return;

			GraphicsEngine.DrawSprite(sprite, color, transform);
		}

		#endregion Internal Methods
	}
}

