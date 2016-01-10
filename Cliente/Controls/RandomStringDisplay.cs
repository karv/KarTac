using System;
using KarTac.Cliente.Controls.Screens;
using System.Collections.Generic;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Control que muestra un string de una lista;
	/// cambia con el tiempo
	/// </summary>
	public class RandomStringDisplay: SBCC
	{
		public RandomStringDisplay (IScreen screen, string fontName = "fonts")
			: base (screen)
		{
			Mostrables = new List<string> ();
			TiempoEntreCambios = TimeSpan.FromSeconds (1);
			fontString = fontName;
			NumEntradasMostrar = 1;
			EspacioEntreLineas = 4;
		}

		public List<string> Mostrables { get; }

		BitmapFont Font;

		string fontString { get; }

		public Color Color = Color.White * 0.8f;

		public Vector2 Pos;

		/// <summary>
		/// Número de entradas que se muestran;
		/// </summary>
		/// <value>The number entradas mostrar.</value>
		public int NumEntradasMostrar { get; set; }

		public int EspacioEntreLineas { get; set; }

		[Obsolete ("Usar Actual")]
		public string StringActual
		{
			get
			{
				return índiceActualString < Mostrables.Count ? Mostrables [índiceActualString] : "";
			}
		}

		public string[] Actual
		{
			get
			{
				var ret = new string[NumEntradasMostrar];
				for (int i = 0; i < NumEntradasMostrar; i++)
				{
					ret [i] = Mostrables [(índiceActualString + i) % Mostrables.Count];
				}
				return ret;
			}
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			var ht = Font.LineHeight + EspacioEntreLineas;
			var strs = Actual;
			for (int i = 0; i < NumEntradasMostrar; i++)
			{
				bat.DrawString (Font,
				                strs [i],
				                Pos + new Vector2 (0, ht * i),
				                Color);
				
			}
		}

		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> (fontString);
		}

		protected override void Dispose ()
		{
			Font = null;
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + NumEntradasMostrar) % Mostrables.Count;
		}

		public override Rectangle GetBounds ()
		{
			int ht;
			int wd = 0;
			// Altura
			ht = NumEntradasMostrar * Font.LineHeight + (NumEntradasMostrar - 1) * EspacioEntreLineas;
			// Grosor
			foreach (var str in Actual)
			{
				wd = Math.Max (wd, Font.GetStringRectangle (str, Vector2.Zero).Width);
			}
			return new Rectangle ((int)Pos.X, (int)Pos.Y, wd, ht);
		}

		protected override void OnChrono ()
		{
			StringSiguiente ();
			base.OnChrono ();
		}

		public override void Inicializar ()
		{
			base.Inicializar ();
			Mostrables.Clear ();
		}
	}
}