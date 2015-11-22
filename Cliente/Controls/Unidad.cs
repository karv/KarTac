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

		Rectangle area
		{
			get
			{
				return new Rectangle (topleft, new Point (20, 20));
			}
		}


		Texture2D texturaClase;

		Color FlagColor { get; set; }

		public void LoadContent (ContentManager content)
		{
			texturaClase = content.Load<Texture2D> ("Unidad");
		}

		public void Dibujar (SpriteBatch bat, GraphicsDevice dev)
		{
			bat.Draw (texturaClase, area, Color.Black);
			bat.Draw (CreateCircle (5, dev), area, FlagColor);
		}

		public Texture2D CreateCircle (int radius, GraphicsDevice dev)
		{
			int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
			var texture = new Texture2D (dev, outerRadius, outerRadius);

			var data = new Color[outerRadius * outerRadius];

			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++)
				data [i] = Color.Transparent;

			double angleStep = 1f / radius;

			for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
			{
				int x = (int)Math.Round (radius + radius * Math.Cos (angle));
				int y = (int)Math.Round (radius + radius * Math.Sin (angle));

				data [y * outerRadius + x + 1] = Color.White;
			}

			texture.SetData (data);
			return texture;
		}
	}
}