using KarTac.Cliente.Controls.Screens;
using KarTac.Cliente.Controls.Primitivos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using OpenTK.Input;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : SBC
	{
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			Texto = "";
		}

		#region Estado

		public string Texto { get; set; }

		/// <summary>
		/// Si el control debe responder a el estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		#endregion

		public Point Pos
		{
			get
			{
				return Bounds.Location;
			}
			set
			{
				Bounds = new Rectangle (value, Bounds.Size);
			}
		}

		public Color ColorContorno = Color.White;
		public Color ColorTexto = Color.White;
		Texture2D contornoTexture;
		BitmapFont fontTexture;

		public Rectangle Bounds { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			Formas.DrawRectangle (bat, GetBounds (), ColorContorno, contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos.ToVector2 (), ColorTexto);
		}

		public override void LoadContent ()
		{
			contornoTexture = Screen.Content.Load<Texture2D> ("Rect");
			fontTexture = Screen.Content.Load<BitmapFont> ("fonts");
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

			if (CatchKeys)
			{
				for (var i = Key.A; i < Key.Z; i++)
				{
					if (InputManager.FuePresionado (i))
					{
						var tx = i.ToString ();
						if (!InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftLeft) && !InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftRight))
						{
							tx = tx.ToLower ();
						}
						Texto += tx;
					}
				}
				if (InputManager.FuePresionado (Key.Space))
				{
					Texto += " ";
				}
				if (InputManager.FuePresionado (Key.Back))
				{
					if (Texto.Length > 0)
						Texto = Texto.Remove (Texto.Length - 1);
				}
			}
		}

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}
	}
}