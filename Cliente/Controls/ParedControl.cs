using KarTac.Batalla.Objetos;
using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KarTac.Cliente.Controls.Primitivos;

namespace KarTac.Cliente.Controls
{
	public class ParedControl : SBC
	{
		public ParedControl (Pared pared, IScreen scr)
			: base (scr)
		{
			Pared = pared;
		}

		public Pared Pared { get; }

		Texture2D textura { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			Formas.DrawLine (Screen.Batch, Pared.P0, Pared.P1, Color.Black, textura);
		}

		protected override void Dispose ()
		{
			textura = null;
		}

		public override Rectangle GetBounds ()
		{
			return Rectangle.Empty;
		}

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
		}
	}
}

