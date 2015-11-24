using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.BitmapFonts;

namespace KarTac.Cliente.Controls
{
	public class Unidad: SBC
	{
		public Unidad (KarTacGame juego, KarTac.Batalla.Unidad unid)
			: base (juego)
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
		Texture2D texturaRect;
		BitmapFont font;

		Color FlagColor
		{
			get
			{
				return UnidadBase.Equipo.FlagColor;
			}
		}

		public override void LoadContent ()
		{
			var content = GameBase.Content;
			texturaClase = content.Load<Texture2D> ("Unidad");
			texturaRect = content.Load<Texture2D> ("Rect");
			font = content.Load<BitmapFont> (@"UnitNameFont");
		}

		public override void Dibujar ()
		{
			var bat = GameBase.Batch;
			bat.Draw (texturaClase, area, Color.Black);  // Icono
			bat.Draw (texturaRect, flagRect, FlagColor);    // Bandera
			bat.Draw (texturaRect, hpBar, Color.White);     // HP background
			bat.Draw (texturaRect, currHpBar, Color.Red);   // HP actual

			// Nombre
			var nombre = UnidadBase.PersonajeBase.Nombre;
			var rect = font.GetStringRectangle (nombre, Vector2.Zero);
			var hSize = rect.Width;
			var ySize = rect.Height;

			bat.DrawString (font,
			                UnidadBase.PersonajeBase.Nombre,
			                new Vector2 (area.Center.X - hSize / 2, area.Top - ySize - 2),
			                Color.White);
			//font.GetStringRectangle ("Huehue", Vector2.Zero);
		}

		public override void Include ()
		{
			base.Include ();
			GameBase.Unidades.Add (this);
		}

		public override void Exclude ()
		{
			base.Exclude ();
			GameBase.Unidades.Remove (this);
		}
	}
}