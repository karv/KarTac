using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using KarTac.Cliente.Controls;
using KarTac;
using MonoGame.Extended.BitmapFonts;

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Game
	{
		readonly GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		public MonoGame.Extended.BitmapFonts.BitmapFont Font;

		readonly List<Unidad> Unidades = new List<Unidad> ();

		public KarTacGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;

			var pj = new Personaje ();
			var unidad = new KarTac.Batalla.Unidad (pj);
			unidad.Pos = new Point (100, 100);
			unidad.PersonajeBase.Atributos.HP.Max = 100;
			unidad.PersonajeBase.Atributos.HP.Valor = 80;
			var unidSpr = new Unidad (unidad);
			unidad.PersonajeBase.AlMorir += Exit;
			unidad.Equipo = new KarTac.Batalla.Equipo (1, Color.Red);
			Unidades.Add (unidSpr);

			Components.Add (new GameComponent (this));

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();

				


		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			//spriteBatch.DrawString(new SpriteFont)

			Font = Content.Load<BitmapFont> ("fonts");

			//TODO: use this.Content to load your game content here 
			foreach (var x in Unidades)
			{
				x.LoadContent (Content);
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
			// TODO: Add your update logic here			
			base.Update (gameTime);

			var delta = gameTime.ElapsedGameTime;
			foreach (var x in Unidades)
			{
				var oldPos = x.UnidadBase.Pos;
				x.UnidadBase.Pos = new Point (oldPos.X, oldPos.Y + (int)(delta.TotalSeconds * 100));
				x.UnidadBase.PersonajeBase.Atributos.HP.Valor -= (int)(delta.TotalMilliseconds / 10);
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Green);
		
			//TODO: Add your drawing code here
            
			base.Draw (gameTime);

			spriteBatch.Begin ();
			foreach (var x in Unidades)
			{
				x.Dibujar (spriteBatch, graphics.GraphicsDevice);
			}
			spriteBatch.End ();
		}
	}
} 