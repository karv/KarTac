using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls.Screens
{
	public class Screen : IScreen
	{
		KarTacGame Game { get; }

		public ListaControl Controles { get; }

		public Screen (KarTacGame game)
			: this ()
		{
			Game = game;
		}

		Screen ()
		{
			Controles = new ListaControl ();
		}

		public void Dibujar (GameTime gameTime)
		{
			Game.GraphicsDevice.Clear (Color.Green);

			//base.Draw (gameTime);


			Batch.Begin ();
			foreach (var x in Controles)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();
		}

		public void LoadContent ()
		{
			Batch = new SpriteBatch (Game.GraphicsDevice);
			foreach (var x in Controles)
			{
				x.LoadContent ();
			}
		}

		public void Update (GameTime gameTime)
		{
			foreach (var x in Controles)
			{
				x.Update (gameTime);
			}
			LastMouseState = Mouse.GetState ();
			LastKeyboardState = Keyboard.GetState ();

		}

		public void UnloadContent ()
		{
			throw new NotImplementedException ();
		}

		public ContentManager Content
		{
			get
			{
				return Game.Content;
			}
		}

		public MouseState LastMouseState { get; private set; }

		public KeyboardState LastKeyboardState { get; private set; }

		public SpriteBatch Batch { get; private set; }

	}
}