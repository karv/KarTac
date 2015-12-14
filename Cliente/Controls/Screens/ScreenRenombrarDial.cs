using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls.Screens
{
	public class ScreenRenombrarDial : ScreenDial
	{
		readonly EntradaTexto input;
		readonly RandomStringDisplay display;

		public ScreenRenombrarDial (KarTacGame juego, IScreen screen)
			: base (juego, screen)
		{
			Bounds = new Rectangle (100, 100, 500, 400);
			input = new EntradaTexto (this);
			input.Bounds = new Rectangle (300, 200, 200, 40);
			display = new RandomStringDisplay (this);
			display.Mostrables.Add ("");
			display.Pos = new Vector2 (110, 110);

			input.Include ();
			display.Include ();

		}

		public string TextoPreg
		{
			get
			{
				return display.Mostrables [0];
			}
			set
			{
				display.Mostrables [0] = value;
			}
		}

		public Rectangle Bounds { get; set; }

		public string Texto { get; private set; }

		Texture2D bgTexture;
		Color boxColor = Color.Gray;

		public override void Update (GameTime gameTime)
		{
			if (InputManager.FuePresionado (OpenTK.Input.Key.Enter))
			{
				Texto = input.Texto;
				Salir ();
			}
			base.Update (gameTime);
		}

		public override void LoadContent ()
		{
			base.LoadContent ();
			bgTexture = Content.Load<Texture2D> ("Rect");
		}

		public override void Dibujar (GameTime gametime, SpriteBatch bat)
		{
			bat.Draw (bgTexture, Bounds, boxColor);
		}

		public override void UnloadContent ()
		{
			base.UnloadContent ();
			bgTexture = null;
		}

		public override void Inicializar ()
		{
		}
	}
}