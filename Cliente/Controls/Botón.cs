using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls
{
	public class Botón : SBC
	{
		public Botón (KarTacGame game, Rectangle bounds)
			: base (game)
		{
			Bounds = bounds;
		}

		public Rectangle Bounds { get; set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		public Texture2D TexturaInstancia { get; protected set; }

		public Color Color { get; set; }

		public string Textura { set; get; }

		public override void Dibujar (GameTime gameTime)
		{
			GameBase.Batch.Draw (TexturaInstancia, Bounds, Color);
		}

		public override void LoadContent ()
		{
			Textura = Textura ?? "Rect";
			TexturaInstancia = GameBase.Content.Load<Texture2D> (Textura); //TODO "Unidad" = temporal
		}
	}
}