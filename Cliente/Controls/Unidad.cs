using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace KarTac.Cliente.Controls
{
	public class Unidad
	{
		public Unidad (KarTac.Batalla.Unidad unid)
		{
			UnidadBase = unid;
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

		static int grosorHpBar
		{
			get
			{
				return 3;
			}
		}

		static Point tamaño
		{
			get
			{
				return new Point (20, 20);
			}
		}

		Rectangle flagRect
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
				return new Rectangle (topleft, tamaño);
			}
		}

		Rectangle hpBar
		{
			get
			{
				return new Rectangle (area.Right - grosorHpBar, area.Top, grosorHpBar, tamaño.Y);
			}
		}

		Rectangle currHpBar
		{
			get
			{
				var hp = UnidadBase.PersonajeBase.Atributos.HP;
				var hpPct = hp.Valor / hp.Max;
				var tam = (int)(tamaño.Y * hpPct);

				return new Rectangle (area.Right - grosorHpBar, area.Bottom - tam, grosorHpBar, tam);
			}
		}

		Texture2D texturaClase;
		Texture2D rectText;

		Color FlagColor
		{
			get
			{
				return UnidadBase.Equipo.FlagColor;
			}
		}

		public void LoadContent (ContentManager content)
		{
			texturaClase = content.Load<Texture2D> ("Unidad");
			rectText = content.Load<Texture2D> ("Rect");
		}

		public void Dibujar (SpriteBatch bat, GraphicsDevice dev)
		{
			bat.Draw (texturaClase, area, Color.Black);  // Icono
			bat.Draw (rectText, flagRect, FlagColor);    // Bandera
			bat.Draw (rectText, hpBar, Color.White);     // HP background
			bat.Draw (rectText, currHpBar, Color.Red);   // HP actual
		}

	}
}