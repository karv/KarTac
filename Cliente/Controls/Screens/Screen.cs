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
		public KarTacGame Game { get; }

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

		public virtual void Inicializar ()
		{
			foreach (var x in Controles)
			{
				x.Inicializar ();
			}
		}

		public virtual void Dibujar (GameTime gameTime)
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

		public SpriteBatch GetNewBatch ()
		{
			return Game.GetNewBatch ();
		}

		public virtual void Update (GameTime gameTime)
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

		public DisplayMode GetDisplayMode
		{
			get
			{
				return Game.GetDisplayMode;
			}
		}

		public GraphicsDevice Device
		{
			get
			{
				return Game.GraphicsDevice;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[{0}]\nGame: {1}", GetType (), Game);
		}
	}
}