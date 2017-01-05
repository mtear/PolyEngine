using System;
using System.Collections.Generic;
using XNA = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PolyEngine.Rendering
{
	internal class GraphicsEngine
	{


		#region Private Variables

		internal static List<Renderer> renderComponents = new List<Renderer>();
		internal static List<Renderer> willRender = new List<Renderer>();

		internal static GraphicsDeviceManager graphicsManager;

		internal static SpriteBatch spriteBatch;

		internal static Dictionary<string, Texture2D> textures2D = new Dictionary<string, Texture2D>();

		internal static List<Camera> cameras = new List<Camera>();
		internal static Camera currentCamera = null;

		#endregion Private Variables


		#region Public Static Methods

		internal static void AddCamera(Camera c)
		{
			cameras.Add(c);
			cameras.Sort((x, y) => x.depth.CompareTo(y.depth));
		}

		internal static void AddRenderComponent(Renderer c)
		{ //TODO get a way to remove them
			renderComponents.Add(c);
		}

		public static void DrawCircle(Vector2 position, float radius, Color color)
		{
			float screensize = currentCamera.orthographicSize * 2f;
			float worldratio = currentCamera.pixelHeight / screensize; //todo put in camera

			float scale = (radius * worldratio) / 50f;

			Vector2 p = new Vector2(position.x, -position.y) * worldratio;
			p.x += currentCamera.pixelRect.x + currentCamera.pixelWidth / 2;
			p.y += currentCamera.pixelRect.y + currentCamera.pixelHeight / 2;

			spriteBatch.Draw(
				GetInternalSprite("CIRCLE"),
				p.ToXNA(),
				null,
				color,
				0,
				new XNA.Vector2(50, 50),
				new XNA.Vector2(scale, scale),
				SpriteEffects.None,
				0
			);
		}

		public static void DrawRectangle(Rect coords, Color color)
		{
			spriteBatch.Draw(GetInternalSprite("RECT"), coords.XNARectangle, color);
		}

		public static void DrawLinePixels(Vector2 start, Vector2 end)
		{
			Vector2 edge = end - start;
			// calculate angle to rotate line
			float angle =
				(float)Math.Atan2(edge.y, edge.x);


			spriteBatch.Draw(GetInternalSprite("RECT"),
				new Rectangle(// rectangle defines shape of line and position of start of line
					(int)start.x,
					(int)start.y,
			                  (int)edge.magnitude, //sb will strech the texture to fill this rectangle
					1), //width of line, change this to make thicker line
				null,
				Color.Red, //colour of line
				angle,     //angle of line (calulated above)
				new XNA.Vector2(0, 0), // point in line about which to rotate
				SpriteEffects.None,
				0);

		}

		public static void DrawLine(Vector2 start, Vector2 end)
		{ //todo account for camera positioning
			Vector2 s = new Vector2(start.x, -start.y);
			Vector2 e = new Vector2(end.x, -end.y);

			float screensize = currentCamera.orthographicSize * 2f;
			float worldratio = currentCamera.pixelHeight / screensize;

			s *= worldratio; e *= worldratio;

			s.x += currentCamera.pixelRect.x + currentCamera.pixelWidth / 2;
			s.y += currentCamera.pixelRect.y + currentCamera.pixelHeight / 2;
			e.x += currentCamera.pixelRect.x + currentCamera.pixelWidth / 2;
			e.y += currentCamera.pixelRect.y + currentCamera.pixelHeight / 2;
			DrawLinePixels(s, e);
		}

		public static void DrawSprite(Sprite sprite, Color color, Transform transform)
		{ //todo account for camera positioning
			float screensize = currentCamera.orthographicSize * 2f;
			float worldheight = sprite.bounds.size.y;
			float a = currentCamera.pixelHeight / screensize;
			float ratio = worldheight / screensize;
			float scalevalue = (currentCamera.pixelHeight * ratio) / sprite.texture.Height;

			Vector3 mScale = new Vector3(transform.lossyScale.x, transform.lossyScale.y, 1);
			mScale *= scalevalue;

			Vector3 mPosition = new Vector3(transform.position.x, -transform.position.y, 1);
			mPosition *= a;
			mPosition.x += currentCamera.pixelRect.x + currentCamera.pixelWidth / 2;
			mPosition.y += currentCamera.pixelRect.y + currentCamera.pixelHeight / 2;

			spriteBatch.Draw(sprite.texture, mPosition.To2DXNAVector(), null,
			                 color, (360 - transform.eulerAngles.z) * Mathf.Deg2Rad, sprite.pivot.ToXNA(),
							 mScale.To2DXNAVector(), SpriteEffects.None, 0); //TODO look at destination rectangle?
		}

		public static Texture2D GetSprite(string name)
		{
			if (textures2D.ContainsKey(name)) return textures2D[name];
			else {
				LoadSprite(name);
				return GetSprite(name);
			}
		}

		internal static Texture2D GetInternalSprite(string name)
		{
			if (textures2D.ContainsKey("#INTERNAL_" + name)) return textures2D["#INTERNAL_" + name];
			else {
				return null;
			}
		}

		public static void LoadSprite(string name)
		{
			if (name.Contains("#"))
				throw new Exception("# character not allowed in sprite names!");
			
			if (!textures2D.ContainsKey(name))
				textures2D.Add(name, XNABridge.gameInstance.Content.Load<Texture2D>(name));
		}

		internal static void StoreInternalSprite(string name, Texture2D texture)
		{
			if (!textures2D.ContainsKey("#INTERNAL_" + name))
				textures2D.Add("#INTERNAL_" + name, texture);
		}

		public static void Reset()
		{
			renderComponents.Clear();
			textures2D.Clear();
			cameras.Clear();
			currentCamera = null;
		}

		public static void Cull()
		{
			float screensize = currentCamera.orthographicSize * 2f;
			float a = currentCamera.pixelHeight / screensize;
			foreach (Renderer r in renderComponents)
			{
				if (willRender.Contains(r)) continue;

				if (!r.enabled || !r.gameObject.activeInHeiarchy)
				{
					r.SetVisible(false);
					continue;
				}

				Bounds bounds = r.bounds;
				if (bounds.size.x == 0) continue;

				float worldheight = r.bounds.size.y;
				float ratio = worldheight / screensize;
				float scalevalue = (currentCamera.pixelHeight * ratio) / r.bounds.size.y;

				Vector3 mScale = new Vector3(r.gameObject.transform.localScale.x,
				                             r.gameObject.transform.localScale.y, 1);
				mScale *= scalevalue;

				Vector3 mPosition = new Vector3(r.gameObject.transform.position.x,
				                                r.gameObject.transform.position.y, 1);
				mPosition *= a;
				mPosition.x += currentCamera.pixelRect.x + currentCamera.pixelWidth / 2;
				mPosition.y += currentCamera.pixelRect.y + currentCamera.pixelHeight / 2;

				float maxsize = Mathf.Max(r.bounds.size.x * mScale.x, r.bounds.size.y * mScale.y);

				Rect maxrect = new Rect(mPosition.x - maxsize / 2, mPosition.y - maxsize / 2, maxsize, maxsize);

				if (maxrect.Overlaps(currentCamera.pixelRect))
				{
					r.SetVisible(true);
					willRender.Add(r);
				}
				else {
					r.SetVisible(false);
				}
			}
		}

		public static void Render()
		{

			//Cull
			willRender.Clear();
			for (int i = 0; i < cameras.Count; i++)
			{
				currentCamera = cameras[i];
				Cull();
			}

			//graphicsManager.GraphicsDevice.Clear(cameras[0].backgroundColor);
			graphicsManager.GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			//draw
			for (int i = 0; i < cameras.Count; i++)
			{
				currentCamera = cameras[i];

				//Will render
				foreach (Renderer r in willRender)
				{
					foreach (MonoBehaviour script in r.gameObject.scripts)
					{
						script.OnWillRenderObject();
					}
				}

				//Clear background color of each camera
				//if (i > 0)
				//{
					DrawRectangle(cameras[i].pixelRect, cameras[i].backgroundColor);
				//}

				//Pre render
				foreach (Renderer r in willRender)
				{
					foreach (MonoBehaviour script in r.gameObject.scripts)
					{
						script.OnPreRender();
					}
				}

				//Draw stuff
				foreach (Renderer r in willRender)
				{
					r.OnRender();
					foreach (MonoBehaviour script in r.gameObject.scripts)
					{
						script.OnRenderObject();
					}
				}

				//Post render
				foreach (Renderer r in willRender)
				{
					foreach (MonoBehaviour script in r.gameObject.scripts)
					{
						script.OnPostRender();
					}
				}
			}

			//spriteBatch.Draw(pgo.Sprite.Texture, pgo.Physics.TopLeft(), null, Color.White,
			//               0, new Vector2(0,0), pgo.Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(GetSprite(name), position, Color.White);
			//spriteBatch.Draw(GetSprite("doge"), new Microsoft.Xna.Framework.Vector2(0, 0), Color.White);

			//Render image
			foreach (Renderer r in willRender)
			{
				foreach (MonoBehaviour script in r.gameObject.scripts)
				{
					script.OnRenderImage();
				}
			}

			Physics2D.RenderColliders();

			spriteBatch.End();
		}

		public static void Unload()
		{
			spriteBatch.Dispose();
			foreach (string k in textures2D.Keys)
			{
				textures2D[k].Dispose();
			}
			textures2D.Clear();
		}

		public static Texture2D CreateCircle(GraphicsDevice graphics, int radius)
		{
			int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
			Texture2D texture = new Texture2D(graphics, outerRadius, outerRadius);

			Color[] data = new Color[outerRadius * outerRadius];

			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++)
				data[i] = Color.Transparent;

			// Work out the minimum step necessary using trigonometry + sine approximation.
			double angleStep = 1f / radius;

			for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
			{
				// Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
				int x = (int)Math.Round(radius + radius * Math.Cos(angle));
				int y = (int)Math.Round(radius + radius * Math.Sin(angle));

				data[y * outerRadius + x + 1] = Color.White;
			}

			texture.SetData(data);
			return texture;
		}

		#endregion Public Static Methods

	}
}

