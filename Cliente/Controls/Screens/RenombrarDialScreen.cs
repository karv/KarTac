using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using Moggle.Controles;

namespace KarTac.Cliente.Controls.Screens
{
	public class RenombrarDialScreen : DialScreen
	{
		readonly EntradaTexto input;
		readonly Etiqueta display;

		public RenombrarDialScreen (KarTacGame juego, IScreen screen)
			: base (juego, screen)
		{
			Bounds = new Rectangle (100, 100, 500, 400);
			input = new EntradaTexto (this);
			input.Bounds = new Rectangle (300, 200, 200, 40);
			display = new Etiqueta (this);
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

		protected override void TeclaPresionada (OpenTK.Input.Key key)
		{
			base.TeclaPresionada (key);
			if (key == OpenTK.Input.Key.Enter)
			{
				Texto = input.Texto;
				Salir ();
			}
		}

		public override void LoadContent ()
		{
			base.LoadContent ();
			bgTexture = Content.Load<Texture2D> ("Rect");
		}

		protected override void EntreBatches (GameTime gameTime)
		{
			Batch.Draw (bgTexture, Bounds, boxColor);
			base.EntreBatches (gameTime);
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