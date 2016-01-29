using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

		void TeclaPresionada (OpenTK.Input.Key key)
		{
			foreach (var x in Controles)
			{
				x.CatchKey (key);
			}
		}

		public bool Escuchando
		{
			set
			{
				if (value)
					InputManager.AlSerActivado += TeclaPresionada;
				else
					InputManager.AlSerActivado -= TeclaPresionada;
			}
		}

		public virtual Color BgColor
		{
			get
			{
				return Color.Black;
			}
		}

		public virtual void Inicializar ()
		{
			foreach (var x in Controles)
			{
				x.Inicializar ();
			}
			InputManager.AlSerActivado += TeclaPresionada;
		}

		public virtual void Dibujar (GameTime gameTime)
		{
			//base.Draw (gameTime);

			//var Batch = GetNewBatch ();
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
			foreach (var x in new List<IControl> (Controles))
				x.Update (gameTime);
		}

		public void UnloadContent ()
		{
			foreach (var x in new List<IControl> (Controles))
			{
				x.Dispose ();
			}
			InputManager.AlSerActivado -= TeclaPresionada;
		}

		public ContentManager Content
		{
			get
			{
				return Game.Content;
			}
		}

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
			return string.Format ("[{0}]", GetType ());
		}
	}
}