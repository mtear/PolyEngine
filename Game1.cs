using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestXNA4
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// This is a texture we can render.
		Texture2D myTexture;

		// Set the coordinates to draw the sprite at.
		Vector2 spritePosition = Vector2.Zero;

		// Store some information about the sprite's motion.
		Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			myTexture = Content.Load<Texture2D>("doge");

			//TODO: use this.Content to load your game content here 
		}

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

			// TODO: Add your update logic here

			// Move the sprite around.
			UpdateSprite(gameTime);

			base.Update(gameTime);
		}

		void UpdateSprite(GameTime gameTime)
		{
			// Move the sprite by speed, scaled by elapsed time.
			spritePosition +=
				spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			int MaxX =
				graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
			int MinX = 0;
			int MaxY =
				graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
			int MinY = 0;

			// Check for bounce.
			if (spritePosition.X > MaxX)
			{
				spriteSpeed.X *= -1;
				spritePosition.X = MaxX;
			}

			else if (spritePosition.X < MinX)
			{
				spriteSpeed.X *= -1;
				spritePosition.X = MinX;
			}

			if (spritePosition.Y > MaxY)
			{
				spriteSpeed.Y *= -1;
				spritePosition.Y = MaxY;
			}

			else if (spritePosition.Y < MinY)
			{
				spriteSpeed.Y *= -1;
				spritePosition.Y = MinY;
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			// Draw the sprite.
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			spriteBatch.Draw(myTexture, spritePosition, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

