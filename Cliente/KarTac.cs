using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using KarTac.Cliente.Controls;
using KarTac.Cliente.Controls.Screens;
using OpenTK.Input;
using System.IO;

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Game
	,IScreen // Para poder tener controles globales (cursor)
	{
		public const string FileName = "game.sav";

		readonly Ratón mouse;

		#if FPS
		readonly Label fpsLabel;
		#endif

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
			#if FPS
			fpsLabel = new Label (this);
			fpsLabel.Texto = () => string.Format ("fps: {0}", GetDisplayMode.RefreshRate);
			fpsLabel.UseFont = @"UnitNameFont";
			fpsLabel.Color = Color.White;
			fpsLabel.Include ();
			#endif
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
			Clan unClan;
			if (File.Exists (FileName))
			{
				unClan = new Clan ();
				unClan.Cargar (FileName);
			}
			else
				unClan = Clan.BuildStartingClan ();

			var scr = new OutsideScreen (this, unClan);
			CurrentScreen = scr;
			scr.LoadContent ();

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
			graphics.GraphicsDevice.Clear (BackgroundColor);

			Batch.Begin ();

			CurrentScreen.Dibujar (gameTime);

			foreach (var x in ControlesUniversales)
			{
				x.Dibujar (gameTime);
			}

			//mouse.Dibujar (gameTime);
			Batch.End ();

		}

		public Color BackgroundColor
		{
			get
			{
				return CurrentScreen.BgColor;
			}
		}

		#region IScreen

		Color IScreen.BgColor
		{
			get
			{
				return BackgroundColor;
			}
		}

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