using System;
using Microsoft.Xna.Framework;
using PolyEngine;
using PolyPhys;

namespace TestXNA4
{
	public class SampleGame : PolyGame
	{

		PolyGameObject player;

		protected override void init()
		{
			/*player = new PolyGameObject("player", new PolySprite("face"));
			player.SetCollider(new Circle(player.Sprite.Texture.Width/2));
			player.Physics.Velocity = new Vector2(60f, 140f);
			player.Scale = 1f;
			player.Physics.SetPosition(new Vector2(100, 200));
			player.Physics.Restitution = 1f;
			player.Physics.Mass = 1;
			AddGameObject(player);

			PolyGameObject playerb = new PolyGameObject("playerb", new PolySprite("face"));
			playerb.SetCollider(new Circle(playerb.Sprite.Texture.Width/2));
			playerb.Physics.Velocity = new Vector2(-120f, 80f);
			playerb.Scale = 1f;
			playerb.Physics.SetPosition(new Vector2(400, 200));
			playerb.Physics.Restitution = 1f;
			playerb.Physics.Mass = 1;
			AddGameObject(playerb);

			PolyGameObject playerc = new PolyGameObject("playerc", new PolySprite("face"));
			playerc.SetCollider(new Circle(playerc.Sprite.Texture.Width/2));
			playerc.Physics.Velocity = new Vector2(130f, 70f);
			playerc.Scale = 1f;
			playerc.Physics.SetPosition(new Vector2(200, 300));
			playerc.Physics.Restitution = 1;
			playerc.Physics.Mass = 1;
			AddGameObject(playerc);

			PolyGameObject playerz = new PolyGameObject("playerz", new PolySprite("face"));
			playerz.SetCollider(new Circle(playerz.Sprite.Texture.Width / 2));
			playerz.Physics.Velocity = new Vector2(130f, 70f);
			playerz.Scale = 2f;
			playerz.Physics.SetPosition(new Vector2(200, 300));
			playerz.Physics.Restitution = 1;
			playerz.Physics.Mass = 1;
			AddGameObject(playerz);

			PolyGameObject playery = new PolyGameObject("playery", new PolySprite("doge"));
			playery.Physics.Velocity = new Vector2(130f, 70f);
			playery.Scale = 0.1f;
			playery.Physics.SetPosition(new Vector2(200, 300));
			playery.Physics.Restitution = 1;
			playery.Physics.Mass = 1;
			AddGameObject(playery);

			PolyGameObject playerd = new PolyGameObject("playerd", new PolySprite("doge"));
			playerd.Physics.Velocity = new Vector2(-60f, -140f);
			playerd.Scale = 0.2f;
			playerd.Physics.SetPosition(new Vector2(400, 300));
			playerd.Physics.Restitution = 1;
			playerd.Physics.Mass = 4;
			AddGameObject(playerd);

			PolyGameObject playere = new PolyGameObject("playere", new PolySprite("doge"));
			playere.Physics.Velocity = new Vector2(50f, -150f);
			playere.Scale = 0.05f;
			playere.Physics.SetPosition(new Vector2(270, 350));
			playere.Physics.Restitution = 1;
			playere.Physics.Mass = .25f;
			AddGameObject(playere);

			// ------

			PolyGameObject player2 = new PolyGameObject("player2", new PolySprite("doge"));
			player2.Physics.SetPosition( new Vector2(300, GFX.ScreenHeight+75));
			player2.Inert = true;
			player2.Physics.Restitution = 1f;
			player2.Physics.Mass = 0;
			AddGameObject(player2);

			PolyGameObject player3 = new PolyGameObject("player3", new PolySprite("doge"));
			player3.Physics.SetPosition(new Vector2(800, GFX.ScreenHeight - 275));
			player3.Inert = true;
			player3.Physics.Restitution = 1f;
			player3.Physics.Mass = 0;
			AddGameObject(player3);

			PolyGameObject player4 = new PolyGameObject("player4", new PolySprite("doge"));
			player4.Physics.SetPosition(new Vector2(-200, 200));
			player4.Inert = true;
			player4.Physics.Restitution = 1f;
			player4.Physics.Mass = 0;
			AddGameObject(player4);

			PolyGameObject player5 = new PolyGameObject("player5", new PolySprite("doge"));
			player5.Physics.SetPosition(new Vector2(300, -100));
			player5.Inert = true;
			player5.Physics.Restitution = 1f;
			player5.Physics.Mass = 0;
			AddGameObject(player5);*/
		}

		protected override void update()
		{
			//player.Rotation += 1 * elapsedTime;
		}

		protected override void draw()
		{
			//GFX.DrawSprite("doge", spritePosition);
		}

		//MAKE GAMEOBJECT
	}
}

