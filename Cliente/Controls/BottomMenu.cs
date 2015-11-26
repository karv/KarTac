using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;
using MonoGame.Extended.BitmapFonts;
using System.Text;

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
			Prioridad = -20;
		}

		public KarTac.Batalla.Unidad UnidadActual { get; set; }

		public int TamañoY = 200;

		Texture2D textura;
		BitmapFont InfoFont;
		public Color BgColor = Color.Blue;

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
			InfoFont = Screen.Content.Load<BitmapFont> ("fonts");
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.GetNewBatch ();
			bat.Begin ();
			bat.Draw (textura, GetBounds (), BgColor);

			if (UnidadActual != null)
			{
				string infoStr = string.Format ("Nombre: {0}",
				                                UnidadActual.PersonajeBase.Nombre);
				bat.DrawString (InfoFont,
				                infoStr,
				                new Vector2 (20, GetBounds ().Top + 20),
				                Color.Red * 0.8f);
			}
			bat.End ();
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (0, Screen.GetDisplayMode.Height - TamañoY,
			                      Screen.GetDisplayMode.Width, TamañoY);
		}
	}
}