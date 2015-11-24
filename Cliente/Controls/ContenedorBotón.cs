﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
			Prioridad = -5;
			controles = new List<Botón> (Filas * Columnas);
		}

		List<Botón> controles { get; }

		Texture2D texturafondo;
		public Color BgColor = Color.DarkBlue;

		int filas = 3;
		int columnas = 10;
		MargenType márgenes;
		Point tamañobotón = new Point (30, 30);
		Point posición;
		TipoOrdenEnum tipoOrden;

		public TipoOrdenEnum TipoOrden
		{
			get
			{
				return tipoOrden;
			}
			set
			{
				tipoOrden = value;
				OnRedimensionar ();
			}
		}

		public Point Posición
		{
			get
			{
				return posición;
			}
			set
			{
				posición = value;
				OnRedimensionar ();
			}
		}

		public Point TamañoBotón
		{
			get
			{
				return tamañobotón;
			}
			set
			{
				tamañobotón = value;
				OnRedimensionar ();
			}
		}

		public MargenType Márgenes
		{
			get
			{
				return márgenes;
			}
			set
			{
				márgenes = value;
				OnRedimensionar ();
			}
		}

		public int Columnas
		{
			get
			{
				return columnas;
			}
			set
			{
				columnas = value;
				OnRedimensionar ();
			}
		}

		public int Filas
		{
			get
			{
				return filas;
			}
			set
			{
				filas = value;
				OnRedimensionar ();
			}
		}

		public Botón Add ()
		{
			var ret = new Botón (Game, CalcularPosición (Count));
			controles.Add (ret);
			ret.Include ();
			return ret;
		}

		protected void OnRedimensionar ()
		{
			for (int i = 0; i < Count; i++)
			{
				var bot = BotónEnÍndice (i);
				bot.Bounds = CalcularPosición (i);
			}
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

		public Botón BotónEnÍndice (int index)
		{
			return controles [index];
		}

		protected Rectangle CalcularPosición (int index)
		{
			Rectangle bounds;
			var bb = GetBounds (); // Coda del contenedor
			Point locGrid;
			int orden = index;
			locGrid = TipoOrden == TipoOrdenEnum.ColumnaPrimero ? 
				new Point (orden / Filas, orden % Filas) : 
				new Point (orden % Columnas, orden / Columnas);
			bounds = new Rectangle (bb.Left + Márgenes.Left + TamañoBotón.X * locGrid.X,
			                        bb.Top + Márgenes.Top + TamañoBotón.Y * locGrid.Y,
			                        TamañoBotón.X, TamañoBotón.Y);
			return bounds;
		}
	}

}