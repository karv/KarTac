using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls.Screens
{
	public class ScreenRenombrarDial : ScreenDial
	{
		readonly EntradaTexto input;
		readonly Label display;

		public ScreenRenombrarDial (KarTacGame juego, IScreen screen)
			: base (juego, screen)
		{
			Bounds = new Rectangle (100, 100, 500, 400);
			input = new EntradaTexto (this);
			input.Bounds = new Rectangle (300, 200, 200, 40);
			display = new Label (this);
			display.Posición = new Point (110, 110);
			display.UseFont = "fonts";
			display.Texto = () => TextoPreg;

			input.Include ();
			display.Include ();

		}

		public override bool DibujarBase
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Texto que aparece como título de la pregunta.
		/// </summary>
		public string TextoPreg { get; set; }

		/// <summary>
		/// Límites de el área del rectángulo donde se muestra este "diálogo"
		/// </summary>
		public Rectangle Bounds { get; set; }

		/// <summary>
		/// Texto que el usuario introduce.
		/// </summary>
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