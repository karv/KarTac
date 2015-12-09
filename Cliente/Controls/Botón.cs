using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	public class Botón : SBC
	{
		public Botón (IScreen screen, Rectangle bounds)
			: base (screen)
		{
			Bounds = bounds;
			Color = Color.White;
		}

		public Rectangle Bounds { get; set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		public Texture2D TexturaInstancia { get; protected set; }

		public Color Color { get; set; }

		public string Textura { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (TexturaInstancia, Bounds, Color);
		}

		public override void LoadContent ()
		{
			Textura = Textura ?? "Rect";
			TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
		}

		public bool Habilidato { get; set; }
	}
}