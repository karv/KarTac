using System;
using KarTac.Cliente.Controls.Screens;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls
{
	public class Label : SBC
	{
		public Label (IScreen screen)
			: base (screen)
		{
		}

		public string UseFont { get; set; }

		BitmapFont font;

		public Func<string> Texto;

		public override void Dibujar (Microsoft.Xna.Framework.GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.DrawString (font, Texto (), Posición.ToVector2 (), Color);

		}

		public Point Posición { get; set; }

		public Color Color { get; set; }

		public override Microsoft.Xna.Framework.Rectangle GetBounds ()
		{
			return font.GetStringRectangle (Texto (), Posición.ToVector2 ());
		}

		public override void LoadContent ()
		{
			font = Screen.Content.Load<BitmapFont> (UseFont);
		}

	}
}

