using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace KarTac.Cliente.Controls
{
	public class Unidad
	{
		public Unidad (KarTac.Batalla.Unidad unid)
		{
			UnidadBase = unid;
			FlagColor = Color.Aqua;
		}

		public KarTac.Batalla.Unidad UnidadBase { get; }

		Point topleft
		{
			get
			{
				return new Point (UnidadBase.Pos.X - texturaClase.Width / 2, UnidadBase.Pos.Y - texturaClase.Height / 2);
			}
		}

		static Point flagSize
		{
			get
			{
				return new Point (6, 4);
			}
		}

		Rectangle FlagRect
		{
			get
			{
				return new Rectangle (area.Left, area.Bottom - flagSize.Y, flagSize.X, flagSize.Y);
			}
		}

		Rectangle area
		{
			get
			{
				return new Rectangle (topleft, new Point (20, 20));
			}
		}


		Texture2D texturaClase;
		Texture2D rectText;

		Color FlagColor { get; set; }

		public void LoadContent (ContentManager content)
		{
			texturaClase = content.Load<Texture2D> ("Unidad");
			rectText = content.Load<Texture2D> ("Rect");
		}

		public void Dibujar (SpriteBatch bat, GraphicsDevice dev)
		{
			bat.Draw (texturaClase, area, Color.Black);
			bat.Draw (rectText, FlagRect, FlagColor);
		}

	}
}