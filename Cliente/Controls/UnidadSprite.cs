using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.BitmapFonts;
using System;
using KarTac.Cliente.Controls.Screens;
using Moggle.Controles;
using Moggle.Screens;
using Moggle;

namespace KarTac.Cliente.Controls
{
	public class UnidadSprite: SBC
	{
		public UnidadSprite (IScreen screen, KarTac.Batalla.Unidad unid)
			: base (screen)
		{
			UnidadBase = unid;
			unid.PersonajeBase.AlMorir += delegate
			{
				texturaClase = screen.Content.Load<Texture2D> (@"Icons/Unidades/dead");
			};
		}

		public KarTac.Batalla.Unidad UnidadBase { get; }

		Point topleft
		{
			get
			{
				return VP.CampoAPantalla (new Point (UnidadBase.Pos.X - tamaño.X / 2, UnidadBase.Pos.Y - tamaño.Y / 2));
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
				return new Rectangle (área.Left, área.Bottom - flagSize.Y, flagSize.X, flagSize.Y);
			}
		}

		Rectangle área
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
				return new Rectangle (área.Right - grosorHpBar, área.Top, grosorHpBar, tamaño.Y);
			}
		}

		Rectangle currHpBar
		{
			get
			{
				var hp = UnidadBase.PersonajeBase.Atributos.HP;
				var hpPct = hp.Valor / hp.Max;
				var tam = (int)(tamaño.Y * hpPct);

				return new Rectangle (área.Right - grosorHpBar, área.Bottom - tam, grosorHpBar, tam);
			}
		}

		Texture2D texturaClase;
		Texture2D texturaRect;
		BitmapFont font;

		/// <summary>
		/// Devuelve si esta unidad debe aparecer como 'marcada' para algún tipo de selección
		/// </summary>
		public bool Marcado
		{
			get
			{
				return UnidadBase.CampoBatalla.UnidadActual == UnidadBase;
			}
		}

		public override Rectangle GetBounds ()
		{
			return área;
		}

		Color FlagColor
		{
			get
			{
				return UnidadBase.Equipo.FlagColor;
			}
		}

		public override void LoadContent ()
		{
			var content = Screen.Content;
			texturaClase = content.Load<Texture2D> (@"Icons/Unidades/default");
			//texturaClase = content.Load<Texture2D> ("Unidad");
			texturaRect = content.Load<Texture2D> ("Rect");
			font = content.Load<BitmapFont> (@"UnitNameFont");
		}

		protected override void Dispose ()
		{
			texturaClase = null;
			texturaRect = null;
			font = null;
			base.Dispose ();
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (texturaClase, área, Color.Black * (UnidadBase.EstáVivo ? 1 : 0.3f));  // Icono
			bat.Draw (texturaRect, flagRect, FlagColor); // Bandera
			bat.Draw (texturaRect, hpBar, Color.White);  // HP background
			bat.Draw (texturaRect, currHpBar, Color.Red);// HP actual

			if (Marcado)
			{
				// Dibujar un rectángulo alrededor
				Moggle.Primitivos.DrawRectangle (bat, GetBounds (), Color.Yellow * 0.8f, texturaRect);
			}

			// Nombre
			var nombre = UnidadBase.PersonajeBase.Nombre;
			var rect = font.GetStringRectangle (nombre, Vector2.Zero);
			var hSize = rect.Width;
			var ySize = rect.Height;

			bat.DrawString (font,
			                UnidadBase.PersonajeBase.Nombre,
			                new Vector2 (área.Center.X - hSize / 2, área.Top - ySize - 2),
			                Color.White);
		}

		public ManejadorVP VP
		{
			get
			{
				return (Screen as BattleScreen).ManejadorVista;
			}
		}
	}
}