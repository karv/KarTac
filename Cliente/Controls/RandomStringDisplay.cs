﻿using System;
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
		public List<string> Mostrables { get; }

		public RandomStringDisplay (IScreen screen)
			: base (screen)
		{
			Mostrables = new List<string> ();
			TiempoEntreCambios = TimeSpan.FromSeconds (1);
		}

		BitmapFont Font;
		public Color Color = Color.White * 0.8f;

		public Vector2 Pos;

		public string StringActual
		{
			get
			{
				return índiceActualString < Mostrables.Count ? Mostrables [índiceActualString] : "";
			}
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			string infoStr = StringActual;
			bat.DrawString (Font,
			                infoStr,
			                Pos,
			                Color);
		}

		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> ("fonts");
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + 1) % Mostrables.Count;
		}

		public override Rectangle GetBounds ()
		{
			return Font.GetStringRectangle (StringActual, Pos);
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