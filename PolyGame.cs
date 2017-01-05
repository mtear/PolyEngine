using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PolyPhys;

namespace PolyEngine
{
	public abstract class PolyGame : Game
	{
		private GraphicsDeviceManager graphicsManager;
		private SpriteBatch spriteBatch;

		public PolyGraphics GFX;

		protected float elapsedTime = 0;

		List<PolyGameObject> gameObjects = new List<PolyGameObject>();
		List<Tuple<PolyGameObject, PolyGameObject>> gameObjectCombinations
			= new List<Tuple<PolyGameObject, PolyGameObject>>();

		//Rectangle for VIEWPORT

		public PolyGame()
		{
			graphicsManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			//Poly._Game = this;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			init();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			GFX = new PolyGraphics(graphicsManager, spriteBatch);
			//Texture2D myTexture = Content.Load<Texture2D>("doge");
		}

		protected abstract void init();

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif
			elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			UpdatePhysics();
			update();

			base.Update(gameTime);
		}

		protected abstract void update();

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphicsManager.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

			// Draw the sprite.
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			DrawGameObjects();
			draw();
			spriteBatch.End();

			base.Draw(gameTime);
		}

		protected abstract void draw();

		public void AddGameObject(PolyGameObject pgo)
		{
			gameObjects.Add(pgo);
			RecalculateCombinations();
		}

		private void RecalculateCombinations()
		{
			gameObjectCombinations.Clear();
			for (int i = 0; i < gameObjects.Count; i++)
			{
				for (int a = i + 1; a < gameObjects.Count; a++)
				{
					if(!(gameObjects[i].Inert && gameObjects[a].Inert))
						gameObjectCombinations.Add(Tuple.Create(gameObjects[i],
						                                    gameObjects[a]));
				}
			}
		}

		private void UpdatePhysics()
		{
			foreach (PolyGameObject pgo in gameObjects)
			{
				pgo.UpdatePhysics(elapsedTime);
			}

			Manifold m = new Manifold();
			foreach (Tuple<PolyGameObject, PolyGameObject> t in gameObjectCombinations)
			{
				PhysObj o1 = t.Item1.Physics, o2 = t.Item2.Physics;

				if (o1.GetType() == typeof(AABB)
					&& o2.GetType() == typeof(AABB)) //AABB vs AABB
				{
					if (CollisionResolution.GenerateManifold_AABBxAABB(
						(AABB)o1, (AABB)o2, ref m))
					{
						CollisionResolution.ResolveCollision(m);
					}
				}
				else if (o1.GetType() == typeof(Circle)
						&& o2.GetType() == typeof(Circle)) //Circle vs Circle
				{
					if (CollisionResolution.GenerateManifold_CirclexCircle(
						(Circle)o1, (Circle)o2, ref m))
					{
						CollisionResolution.ResolveCollision(m);
					}
				}
				else if (o1.GetType() == typeof(AABB)
				         && o2.GetType() == typeof(Circle))
				{
					if (CollisionResolution.GenerateManifold_AABBxCircle(
						(AABB)o1, (Circle)o2, ref m))
					{
						CollisionResolution.ResolveCollision(m);
					}
				}else if (o1.GetType() == typeof(Circle)
				          && o2.GetType() == typeof(AABB))
				{
					if (CollisionResolution.GenerateManifold_AABBxCircle(
						(AABB)o2, (Circle)o1, ref m))
					{
						CollisionResolution.ResolveCollision(m);
					}
				}

			}
		}

		private void DrawGameObjects()
		{ //VIEWPORT
			foreach (PolyGameObject pgo in gameObjects)
			{
				GFX.DrawSprite(pgo);
			}

			//GFX.DrawTexture("doge", new Vector2(62.5f, 49.625f));
		}

	}

}

