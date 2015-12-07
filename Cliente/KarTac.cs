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

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			var c = new Campo (new Point (GetDisplayMode.Width, GetDisplayMode.Height));

			var pj = new Personaje ();
			var pj2 = new Personaje ();
			CurrentScreen = new BattleScreen (this, c);
			Screens.Add (CurrentScreen);

			pj.Atributos.HP.Max = 100;
			pj.Atributos.HP.Valor = 80;
			pj.Atributos.HP.Regen = 60;
			pj.Atributos.Velocidad = 100;
			pj.Atributos.Agilidad = 30;
			pj.Nombre = "Juanito";

			pj2.Atributos.HP.Max = 120;
			pj2.Atributos.HP.Valor = 80;
			pj2.Atributos.HP.Regen = 50;
			pj2.Atributos.Velocidad = 90;
			pj2.Atributos.Agilidad = 24;
			pj2.Nombre = "Gordo";

			var unidad = new Unidad (pj, c);
			unidad.Interactor = new InteracciónHumano (unidad, this);
			unidad.PosPrecisa = new Vector2 (200, 150);
			unidad.Equipo = new Equipo (1, Color.Red);

			var unidad2 = new Unidad (pj2, c);
			unidad2.Interactor = new InteracciónHumano (unidad2, this);
			unidad2.PosPrecisa = new Vector2 (100, 100);
			unidad2.Equipo = new Equipo (2, Color.Yellow);

			var unidad3 = new Unidad (pj, c);
			unidad3.Interactor = new InteracciónHumano (unidad3, this);
			unidad3.PosPrecisa = new Vector2 (200, 180);
			unidad3.Equipo = new Equipo (1, Color.Red);

			var ord = new Perseguir (unidad, unidad2);
			unidad.OrdenActual = ord;
			//sc.UnidadActual = unidad;

			c.Unidades.Add (unidad);
			c.Unidades.Add (unidad2);
			c.Unidades.Add (unidad3);

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
			if (InputManager.EstáPresionado (Key.Escape))
			{
				Exit ();
			}

			base.Update (gameTime);
			CurrentScreen.Update (gameTime);

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
			foreach (var cu in ControlesUniversales)
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