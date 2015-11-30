using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// El cursor del mouse
	/// </summary>
	public class Ratón : SBC
	{
		public Ratón (IScreen screen)
			: base (screen)
		{
			Tamaño = new Point (20, 20);
			Prioridad = 1000;
		}

		public Ratón ()
			: base (null)
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

		public override Rectangle GetBounds ()
		{
			return new Rectangle (Pos, Tamaño);
		}

		public override void LoadContent ()
		{
			Textura = Screen.Content.Load<Texture2D> (@"Icons/arrow-cursor");
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (Textura, GetBounds (), Color.WhiteSmoke);
		}
	}
}

