using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// El menú "en pausa"
	/// </summary>
	public class BottomMenu : SBC
	{
		public BottomMenu (IScreen screen)
			: base (screen)
		{
		}

		public int TamañoY = 200;

		Texture2D textura;
		public Color BgColor = Color.Blue;

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
		}

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (textura, GetBounds (), BgColor);
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (0, Screen.GetDisplayMode.Height - TamañoY,
			                      Screen.GetDisplayMode.Width, TamañoY);
		}
	}
}