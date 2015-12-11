using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using KarTac.Cliente.Controls;
using KarTac.Cliente.Controls.Screens;
using System;
using KarTac.Personajes;
using OpenTK.Input;
using KarTac.Batalla;
using KarTac.Batalla.Orden;

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Game
	,IScreen // Para poder tener controles globales (cursor)
	{
		readonly Ratón mouse;

		public ListaControl ControlesUniversales { get; }

		public IScreen CurrentScreen;

		public List<IScreen> Screens { get; }

		readonly GraphicsDeviceManager graphics;

		public SpriteBatch Batch { get; private set; }

		public KarTacGame ()
		{
			ControlesUniversales = new ListaControl ();
			Screens = new List<IScreen> ();
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			mouse = new Ratón (this);
			mouse.Include ();

		}

		static Vector2 randomPointInRectangle (Rectangle rect, Random r)
		{
			return new Vector2 (
				rect.Left + (float)r.NextDouble () * rect.Width,
				rect.Top + (float)r.NextDouble () * rect.Height
			);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			var c = new Campo (new Point (GetDisplayMode.Width, GetDisplayMode.Height));
			CurrentScreen = new BattleScreen (this, c);
			Screens.Add (CurrentScreen);

			Huir.Tamaño = c.Área;

			var r = new Random ();

			var equipos = new Equipo[2];
			var clanes = new Clan[2];
			equipos [0] = new Equipo (1, Color.Red);
			equipos [1] = new Equipo (2, Color.Yellow);

			for (int i = 0; i < 2; i++)
			{
				clanes [i] = Clan.BuildStartingClan ();
				foreach (var pj in clanes[i].Personajes)
				{
					var unid = pj.ConstruirUnidad (c);
					unid.Interactor = new InteracciónHumano (unid, this);
					unid.PosPrecisa = randomPointInRectangle (c.Área, r);
					unid.Equipo = equipos [i];

					c.AñadirUnidad (unid);
				}
			}

			c.SelectorTarget = new Selector (this);

			CurrentScreen.Inicializar ();

			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			Batch = new SpriteBatch (GraphicsDevice);
			//spriteBatch.DrawString(new SpriteFont)

			foreach (var x in Screens)
			{
				x.LoadContent ();
			}

			foreach (var x in ControlesUniversales)
			{
				x.LoadContent ();
			}
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			#if DEBUG
			if (InputManager.EstáPresionado (Key.Escape) && InputManager.EstáPresionado (Key.ControlLeft))
			{
				Exit ();
			}
			#endif

			base.Update (gameTime);
			CurrentScreen.Update (gameTime);
			(this as IScreen).Update (gameTime);

			InputManager.Update ();
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Green);
			CurrentScreen.Dibujar (gameTime);

			Batch.Begin ();
			foreach (var x in ControlesUniversales)
			{
				x.Dibujar (gameTime);
			}

			//mouse.Dibujar (gameTime);
			Batch.End ();
		}

		#region IScreen

		public SpriteBatch GetNewBatch ()
		{
			return new SpriteBatch (GraphicsDevice);
		}

		public GraphicsDevice Device
		{
			get
			{
				return GraphicsDevice;
			}
		}

		void IScreen.Dibujar (GameTime gameTime)
		{
			Draw (gameTime);
		}

		ListaControl IScreen.Controles
		{
			get
			{
				return ControlesUniversales;
			}
		}

		/// <summary>
		/// Carga contenido de controles universales
		/// </summary>
		void IScreen.LoadContent ()
		{
			foreach (var cu in ControlesUniversales)
			{
				cu.LoadContent ();
			}
		}

		void IScreen.Update (GameTime gametime)
		{
			foreach (var cu in new List<IControl> (ControlesUniversales))
			{
				cu.Update (gametime);
			}
		}

		void IScreen.UnloadContent ()
		{
			throw new NotImplementedException ();
		}

		public DisplayMode GetDisplayMode
		{
			get
			{
				return GraphicsDevice.Adapter.CurrentDisplayMode;
			}
		}

		void IScreen.Inicializar ()
		{
			Initialize ();
		}

		#endregion
	}
}