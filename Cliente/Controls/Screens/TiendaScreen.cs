﻿using System;
using KarTac.Equipamento;
using Microsoft.Xna.Framework;
using OpenTK.Input;
using Moggle.Screens;
using Moggle.Controles.Listas;
using Moggle.Controles;

namespace KarTac.Cliente.Controls.Screens
{
	public class TiendaScreen : DialScreen
	{
		public TiendaScreen (Moggle.Game juego, Tienda tienda, InventarioClan inv)
			: base (juego)
		{
			Compras = new Compras (tienda, inv);
		}

		public override void Inicializar ()
		{
			Mostrables = new Lista<Compras.EntradaUnificada> (this);
			Mostrables.Bounds = new Rectangle (20, 20, 700, 200);
			Mostrables.Stringificación = x => string.Format ("{0:000}x {1} ({2:C0})  = {3:C0}",
			                                                 x.Marcadas,
			                                                 x.Nombre,
			                                                 x.PrecioUnitario,
			                                                 x.Precio);

			DineroDisponible = new Etiqueta (this);
			DineroDisponible.Color = Color.White;
			DineroDisponible.Posición = new Point (100, 250);
			DineroDisponible.UseFont = "fonts";
			DineroDisponible.Texto = Compras.DineroDisponible.ToString;

			updateCompras ();

			Mostrables.Include ();
			DineroDisponible.Include ();

			base.Inicializar ();
		}

		protected override void TeclaPresionada (Key key)
		{
			base.TeclaPresionada (key);
			if (key == Key.Escape)
				Salir ();
			if (key == Key.Enter)
			{
				Compras.Commit ();
				updateCompras ();
			}
			if (key == Key.Right)
			{
				Compras.Add (Mostrables.ObjetoEnCursor.Objeto);
				updateCompras ();
			}
			if (key == Key.Left)
			{
				Compras.Add (Mostrables.ObjetoEnCursor.Objeto, -1);
				updateCompras ();
			}
		}

		void updateCompras ()
		{
			Mostrables.Clear ();
			//var z = Compras.MisCompras ();
			foreach (var x in Compras.MisCompras())
			{
				Mostrables.Add (x);
			}
			DineroDisponible.Texto = Compras.DineroDisponible.ToString;
		}


		public Tienda Tienda
		{
			get
			{
				return Compras.Tienda;
			}
		}

		public Compras Compras { get; }

		Lista<Compras.EntradaUnificada> Mostrables;
		Etiqueta DineroDisponible;

		public override bool DibujarBase
		{
			get
			{
				return false;
			}
		}
	}
}