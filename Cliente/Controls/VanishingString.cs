using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	public class VanishingString :SBC
	{
		public VanishingString (IScreen screen)
			: base (screen)
		{
		}

		public VanishingString (IScreen screen, string texto, TimeSpan duración)
			: base (screen)
		{
			Texto = texto;
			Restante = duración;
		}

		BitmapFont Font;
		string texto;
		Vector2 topLeft;

		public TimeSpan Restante { get; private set; }

		public string Texto
		{
			get
			{
				return texto;
			}
			set
			{
				texto = value;
				calcularBounds ();
			}
		}

		void calcularBounds ()
		{
			Bounds = Font.GetStringRectangle (Texto, TopLeft);
		}

		public Vector2 TopLeft
		{
			get
			{
				return topLeft;
			}
			set
			{
				topLeft = value;
				calcularBounds ();
			}
		}

		public Vector2 Centro
		{
			get
			{
				return Bounds.Center.ToVector2 ();
			}
			set
			{
				var altura = Bounds.Height;
				var grosor = Bounds.Width;
				topLeft = value - new Vector2 (grosor / 2.0f, altura / 2.0f);
				calcularBounds ();
			}
		}

		public Rectangle Bounds { get; private set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		public Color Color { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.DrawString (Font, Texto, TopLeft, Color);
		}

		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> ("UnitNameFont");
		}

		public override void Update (GameTime gameTime)
		{
			Restante -= gameTime.ElapsedGameTime;
			if (Restante < TimeSpan.Zero)
				Exclude ();
		}
	}
}