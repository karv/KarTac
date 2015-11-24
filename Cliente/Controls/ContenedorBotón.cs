using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace KarTac.Cliente.Controls
{
	public class ContenedorBotón : SBC
	{
		public enum TipoOrdenEnum
		{
			ColumnaPrimero,
			FilaPrimero
		}

		public struct MargenType
		{
			public int Top;
			public int Bot;
			public int Left;
			public int Right;
		}

		public ContenedorBotón (KarTacGame juego)
			: base (juego)
		{
			TamañoBotón = new Point (30, 30);
			Filas = 3;
			Columnas = 10;
			Prioridad = -5;
			controles = new List<Botón> (Filas * Columnas);
		}

		public Botón BotónEnÍndice (int index)
		{
			return controles [index];
		}

		List<Botón> controles { get; }

		Texture2D texturafondo;
		public Color BgColor = Color.DarkBlue;

		public int Filas { get; set; }

		public int Columnas { get; set; }

		public MargenType Márgenes { get; set; }

		public Point TamañoBotón { get; set; }

		public Point Posición { get; set; }

		public TipoOrdenEnum TipoOrden { get; set; }

		public Botón Add ()
		{
			Rectangle bounds;
			var bb = GetBounds (); // Coda del contenedor
			Point locGrid;
			int orden = Count;
			locGrid = TipoOrden == TipoOrdenEnum.ColumnaPrimero ? 
				new Point (orden / Filas, orden % Filas) : 
				new Point (orden / Columnas, orden % Columnas);
			bounds = new Rectangle (bb.Left + Márgenes.Left + TamañoBotón.X * locGrid.X,
			                        bb.Top + Márgenes.Top + TamañoBotón.Y * locGrid.Y,
			                        TamañoBotón.X, TamañoBotón.Y);
			var ret = new Botón (Game, bounds);
			controles.Add (ret);
			ret.Include ();
			return ret;
		}

		public void Remove (Botón control)
		{
			controles.Remove (control);
			control.Exclude ();
		}

		public int Count
		{
			get
			{
				return controles.Count;
			}
		}

		public override void Include ()
		{
			// Incluye a cada botón en Game
			foreach (var x in controles)
			{
				x.Include ();
			}

			base.Include ();
		}

		public override void Exclude ()
		{
			// Excluye a cada botón de Game
			foreach (var x in controles)
			{
				x.Exclude ();
			}

			base.Exclude ();
		}

		public override void Dibujar (GameTime gameTime)
		{
			Game.Batch.Draw (texturafondo, GetBounds (), BgColor);
		}

		public override void LoadContent ()
		{
			texturafondo = Game.Content.Load<Texture2D> ("Rect");
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (Posición,
			                      new Point (
				                      Márgenes.Left + Márgenes.Right + Columnas * TamañoBotón.X,
				                      Márgenes.Top + Márgenes.Bot + Filas * TamañoBotón.Y));
		}
	}
}