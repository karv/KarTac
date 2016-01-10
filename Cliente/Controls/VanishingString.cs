using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	public class VanishingString : SBC
	{
		public VanishingString (IScreen screen, string texto, TimeSpan duración)
			: base (screen)
		{
			_texto = texto;
			Restante = duración;
			TiempoInicial = duración;
			ColorFinal = Color.Transparent;
			Velocidad = new Vector2 (0, -20);
		}

		BitmapFont Font;
		string _texto;
		Vector2 topLeft;

		/// <summary>
		/// Velocidad de este control.
		/// </summary>
		public Vector2 Velocidad { get; set; }

		public TimeSpan Restante { get; private set; }

		public TimeSpan TiempoInicial { get; }

		public string Texto
		{
			get
			{
				return _texto;
			}
			set
			{
				_texto = value;
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

		public Color ColorInicial { get; set; }

		public Color ColorFinal { get; set; }

		public Color ColorActual
		{
			get
			{
				var t = escalarColor;
				var ret = new Color (
					          (int)(ColorInicial.R * t + ColorFinal.R * (1 - t)),
					          (int)(ColorInicial.G * t + ColorFinal.G * (1 - t)),
					          (int)(ColorInicial.B * t + ColorFinal.B * (1 - t)),
					          (int)(ColorInicial.A * t + ColorFinal.A * (1 - t))
				          );
				return ret;
			}
		}



		/// <summary>
		/// Devuelve valor en [0, 1] depende de dónde en el tiempo es el estado actual de este control (lineal)
		/// 0 si está en el punto de terminación
		/// 1 si está en el punto de inicio
		/// </summary>
		float escalarColor
		{
			get
			{
				return (float)Restante.Ticks / TiempoInicial.Ticks;
			}
		}

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.DrawString (Font, Texto, TopLeft, ColorActual);
		}

		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> ("fonts");
		}

		protected override void Dispose ()
		{
			Font = null;
			base.Dispose ();
		}

		public override void Update (GameTime gameTime)
		{
			Restante -= gameTime.ElapsedGameTime;
			TopLeft += Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (Restante < TimeSpan.Zero)
				Exclude ();
		}

		public override void Inicializar ()
		{
			calcularBounds ();
		}
	}
}