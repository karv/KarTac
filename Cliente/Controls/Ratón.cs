using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// El cursor del mouse
	/// </summary>
	public class Ratón:SBC
	{
		public Ratón (KarTacGame juego)
			: base (juego)
		{
			Tamaño = new Point (15, 15);
			Prioridad = 1000;
		}

		public Texture2D Textura { get; protected set; }

		public Point Pos
		{
			get
			{
				return Mouse.GetState ().Position;
			}
			set
			{
				Mouse.SetPosition (value.X, value.Y);
			}
		}

		public readonly Point Tamaño;

		public override Rectangle Bounds
		{
			get
			{
				return new Rectangle (Pos, Tamaño);
			}
		}


		public override void LoadContent ()
		{
			Textura = GameBase.Content.Load<Texture2D> ("Rect");
		}

		public override void Dibujar (GameTime gameTime)
		{
			GameBase.Batch.Draw (Textura, Bounds, Color.White);
		}
	}
}

