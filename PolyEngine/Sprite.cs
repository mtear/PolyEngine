using System;
using Microsoft.Xna.Framework.Graphics;

namespace PolyEngine
{
	public class Sprite : Object
	{


		#region Public Variables

		//associatedAlphaSplitTexture Returns the texture that contains the alpha channel from the source texture.Unity generates this texture under the hood for sprites that have alpha in the source, and need to be compressed using techniques like ETC1.Returns NULL if there is no associated alpha texture for the source sprite.This is the case if the sprite has not been setup to use ETC1 compression.
		//border Returns the border sizes of the sprite.
		//bounds Bounds of the Sprite, specified by its center and extents in world space units.
		public Bounds bounds;

		//packed Returns true if this Sprite is packed in an atlas.
		//packingMode If Sprite is packed (see Sprite.packed), returns its SpritePackingMode.
		//packingRotation If Sprite is packed(see Sprite.packed), returns its SpritePackingRotation.
		//pivot Location of the Sprite's center point in the Rect on the original Texture, specified in pixels.
		public Vector2 pivot;

		//pixelsPerUnit The number of pixels in the sprite that correspond to one unit in world space. (Read Only)
		public float pixelsPerUnit
		{
			get
			{
				return mPixelsPerUnit;
			}
		}

		//rect Location of the Sprite on the original Texture, specified in pixels.
		//texture Get the reference to the used texture.If packed this will point to the atlas, if not packed will point to the source sprite.
		public Texture2D texture
		{
			get
			{
				return mTexture;
			}
		}

		//textureRect Get the rectangle this sprite uses on its texture.Raises an exception if this sprite is tightly packed in an atlas.
		//textureRectOffset Gets the offset of the rectangle this sprite uses on its texture to the original sprite bounds. If sprite mesh type is FullRect, offset is zero.
		//triangles Returns a copy of the array containing sprite mesh triangles.
		//uv The base texture coordinates of the sprite mesh.
		//vertices Returns a copy of the array containing sprite mesh vertex positions.

		#endregion Public Variables


		#region Private Variables

		private float mPixelsPerUnit = 100;
		private Texture2D mTexture = null;

		#endregion Private Variables


		#region Public Methods

		//OverrideGeometry	Sets up new Sprite geometry.

		#endregion Public Methods


		#region Internal Methods

		internal void SetTexture(Texture2D tex)
		{
			this.mTexture = tex;
			this.pivot = new Vector2(tex.Width / 2, tex.Height / 2);
			this.bounds = new Bounds(new Vector3(pivot.x / pixelsPerUnit,
			                                     pivot.y / pixelsPerUnit, 0),
			                         new Vector3(tex.Width / pixelsPerUnit,
			                                     tex.Height / pixelsPerUnit, 0));
		}

		#endregion Internal Methods


		#region Static Methods

		//Create	Create a new Sprite object.
		/*public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot,
		                            float pixelsPerUnit = 100.0f, uint extrude = 0,
		                            SpriteMeshType meshType = SpriteMeshType.Tight,
		                            Vector4 border = Vector4.zero)*/
		public static Sprite Create(Texture2D texture)
		{
			Sprite sprite = new Sprite();
			sprite.SetTexture(texture);
			return sprite;
		}

		#endregion Static Methods


	}
}

