using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using KarTac.Cliente.Controls;
using KarTac;
using MonoGame.Extended.BitmapFonts;
using KarTac.Cliente.Controls.Screens;
using System;

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Game
	,IScreen // Para poder tener controles globales (cursor)
	{
		Ratón mouse;

		public ListaControl ControlesUniversales { get; }

		public IScreen BattleScreen;

		public List<IScreen> Screens { get; }

		readonly GraphicsDeviceManager graphics;

		public SpriteBatch Batch { get; private set; }

		public readonly List<Unidad> Unidades = new List<Unidad> ();

		public KarTacGame ()
		{
			ControlesUniversales = new ListaControl ();
			Screens = new List<IScreen> ();
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			mouse = new Ratón (this);
			mouse.Include ();

			BattleScreen = new Screen (this);
			Screens.Add (BattleScreen);
		}

		public KeyboardState LastKeyboardState { get; protected set; }

		public MouseState LastMouseState { get; protected set; }

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			var pj = new Personaje ();
			var unidad = new KarTac.Batalla.Unidad (pj);
			unidad.Pos = new Point (100, 100);
			unidad.PersonajeBase.Atributos.HP.Max = 100;
			unidad.PersonajeBase.Atributos.HP.Valor = 80;
			var unidSpr = new Unidad (BattleScreen, unidad);
			//unidad.PersonajeBase.AlMorir += Exit;
			unidad.PersonajeBase.Nombre = "Juanito";
			unidad.Equipo = new KarTac.Batalla.Equipo (1, Color.Red);
			unidSpr.Include ();

			var bt = new Botón (BattleScreen, new Rectangle (200, 200, 300, 300));
			bt.Include ();

			bt.AlClick += delegate
			{
				foreach (var u in Unidades)
				{
					u.UnidadBase.PersonajeBase.Nombre = "Noname0525";
				}
			};

			var listaSkills = new ContenedorBotón (this);
			listaSkills.Posición = new Point (0, 0);
			listaSkills.BgColor = Color.Yellow;
			listaSkills.Filas = 1;
			listaSkills.Add ().Color = Color.Red;
			listaSkills.Add ().Color = Color.Green;
			listaSkills.Add ().Color = Color.Blue;
			listaSkills.Include ();
			listaSkills.TipoOrden = ContenedorBotón.TipoOrdenEnum.FilaPrimero;
			listaSkills.BotónEnÍndice (0).AlClick += Exit;
			listaSkills.BotónEnÍndice (1).AlClick += delegate
			{
				listaSkills.Filas = (listaSkills.Filas % 2) + 1;
				System.Console.WriteLine (listaSkills.Filas);
			};
			listaSkills.BotónEnÍndice (2).AlClick += 
				() => listaSkills.TipoOrden = listaSkills.TipoOrden == ContenedorBotón.TipoOrdenEnum.ColumnaPrimero ? 
				ContenedorBotón.TipoOrdenEnum.FilaPrimero : 
				ContenedorBotón.TipoOrdenEnum.ColumnaPrimero;

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
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape))
			{
				Exit ();
			}
			#endif

			base.Update (gameTime);
			var delta = gameTime.ElapsedGameTime;
			foreach (var x in Screens)
			{
				x.Update (gameTime);
				/* var oldPos = x.UnidadBase.Pos;
				x.UnidadBase.Pos = new Point (oldPos.X, oldPos.Y + (int)(delta.TotalSeconds * 100));
				x.UnidadBase.PersonajeBase.Atributos.HP.Valor -= (int)(delta.TotalSeconds * 100); */
			}

			LastKeyboardState = Keyboard.GetState ();
			LastMouseState = Mouse.GetState ();
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Green);
		
			//base.Draw (gameTime);

			foreach (var x in Screens)
			{
				x.Dibujar (gameTime);
			}

			Batch.Begin ();
			foreach (var x in ControlesUniversales)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();
		}

		#region IScreen

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

		#endregion
	}
} 