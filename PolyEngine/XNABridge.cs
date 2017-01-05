using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PolyEngine.Rendering;

namespace PolyEngine
{
	public class XNABridge : Game
	{

		internal static Game gameInstance = null;

		internal bool mpaused = false;

		public XNABridge()
		{
			XNABridge.gameInstance = this;
			GraphicsEngine.graphicsManager = new GraphicsDeviceManager(this);
			GraphicsEngine.graphicsManager.PreferredBackBufferWidth = 960;
			GraphicsEngine.graphicsManager.PreferredBackBufferHeight = 540;
			//TODO manage this better, Settings?
			Resolution res = new Resolution();
			res.width = 960;
			res.height = 540;
			res.refreshRate = 60; //TODO
			Screen.SetCurrentResolution(res);

			Time.fixedDeltaTime = 1f / 60f;

			Content.RootDirectory = "Content";

		}

		protected override void LoadContent() //TODO load scene content
		{
			GraphicsEngine.spriteBatch = new SpriteBatch(GraphicsDevice);
			Texture2D whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
			whiteRectangle.SetData(new[] { Color.White });
			GraphicsEngine.StoreInternalSprite("RECT", whiteRectangle);
			GraphicsEngine.StoreInternalSprite("CIRCLE", GraphicsEngine.CreateCircle(GraphicsDevice, 50));

			GraphicsEngine.LoadSprite("doge"); //TODO

			SceneManagement.SceneManager.LoadScene("scene1"); //TODO
		}

		protected override void Update(GameTime gameTime)
		{
			if (mpaused) return; //TODO only full screen

			Time.realTime = (float)gameTime.TotalGameTime.TotalSeconds;

			Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; //TODO fix readonly
			Time.time += Time.deltaTime;

			while (Time.fixedTime < Time.time)
			{
				Time.fixedTime += Time.deltaTime;
				SceneManagement.SceneManager.currentScene.FixedUpdateAll();
				//More physics collisions
				Physics2D.UpdateAll();
			}

			Input.UpdateInputs();
			//Input triggers here
			SceneManagement.SceneManager.currentScene.UpdateAll();
			//Animation Update here
			SceneManagement.SceneManager.currentScene.LateUpdateAll();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			SceneManagement.SceneManager.currentScene.PreCullAll();
			GraphicsEngine.Render();
			base.Draw(gameTime);
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
			GraphicsEngine.Unload();
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			mpaused = false;
			if (SceneManagement.SceneManager.currentScene.gameObjects != null)
			{
				foreach (GameObject g in SceneManagement.SceneManager.currentScene.gameObjects)
				{
					if (!g.activeInHeiarchy) continue;
					foreach (MonoBehaviour script in g.scripts)
					{
						if (!script.isActiveAndEnabled) continue;
						script.OnApplicationPause(false);
					}
				}
			}
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			mpaused = true;
			if (SceneManagement.SceneManager.currentScene.gameObjects != null)
			{
				foreach (GameObject g in SceneManagement.SceneManager.currentScene.gameObjects)
				{
					if (!g.activeInHeiarchy) continue;
					foreach (MonoBehaviour script in g.scripts)
					{
						if (!script.isActiveAndEnabled) continue;
						script.OnApplicationPause(true);
					}
				}
			}
			base.OnDeactivated(sender, args);
		}
	}
}

