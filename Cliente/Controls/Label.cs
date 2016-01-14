﻿using System;
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

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			var txt = Texto ();
			bat.DrawString (font, txt, Posición.ToVector2 (), Color);
		}

		public Point Posición { get; set; }

		public Color Color { get; set; }

		public override Rectangle GetBounds ()
		{
			return font.GetStringRectangle (Texto (), Posición.ToVector2 ());
		}

		public override void LoadContent ()
		{
			font = Screen.Content.Load<BitmapFont> (UseFont);
		}

		protected override void Dispose ()
		{
			font = null;
			base.Dispose ();
		}
	}
}