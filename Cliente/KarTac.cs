using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using KarTac.Cliente.Controls;
using KarTac;
using MonoGame.Extended.BitmapFonts;
using OpenTK;

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Game
	{
		Ratón mouse;

		public List<IControl> Controles { get; }

		readonly GraphicsDeviceManager graphics;

		public SpriteBatch Batch { get; private set; }

		public readonly List<Unidad> Unidades = new List<Unidad> ();

		public KarTacGame ()
		{
			Controles = new List<IControl> ();
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			mouse = new Ratón (this);
			mouse.Include ();
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
			var unidSpr = new Unidad (this, unidad);
			unidad.PersonajeBase.AlMorir += Exit;
			unidad.PersonajeBase.Nombre = "Juanito";
			unidad.Equipo = new KarTac.Batalla.Equipo (1, Color.Red);
			unidSpr.Include ();

			var bt = new Botón (this, new Rectangle (200, 200, 300, 300));
			bt.Include ();

			bt.AlClick += delegate()
			{
				foreach (var u in Unidades)
				{
					u.UnidadBase.PersonajeBase.Nombre = "Noname0525";
				}
			};


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

			Content.Load<Texture> ("Unidad");
			Content.Load<BitmapFont> (@"Fonts/fonts");


			foreach (var x in Controles)
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
			foreach (var x in Controles)
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


			Batch.Begin ();
			foreach (var x in Controles)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();
		}
	}
} 