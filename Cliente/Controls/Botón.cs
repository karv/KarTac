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

		public Texture2D Textura { get; set; }

		public override void Dibujar ()
		{
			GameBase.Batch.Draw (Textura, Bounds, Color.Gray);
		}

		public override void LoadContent ()
		{
			Textura = GameBase.Content.Load<Texture2D> ("Unidad"); //TODO "Unidad" = temporal
		}
	}
}

