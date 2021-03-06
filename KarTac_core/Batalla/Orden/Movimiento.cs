﻿using Microsoft.Xna.Framework;
using System;

namespace KarTac.Batalla.Orden
{
	/// <summary>
	/// Orden de movimiento con destino fijo
	/// </summary>
	public class Movimiento : IOrden
	{
		/// <summary>
		/// Distancia al destino para considerar que llegó
		/// </summary>
		const float _distanciaCercano = 3;

		public Point Destino { get; set; }

		public Unidad Unidad { get; }

		bool IOrden.EsCancelable
		{
			get
			{
				return true;
			}
		}

		public Movimiento (Unidad unidad)
		{
			Unidad = unidad;
		}

		public virtual UpdateReturnType Update (TimeSpan time)
		{
			var movDir = (Destino - Unidad.Pos).ToVector2 ();
			if (movDir.Length () < _distanciaCercano)
			{
				Unidad.PosPrecisa = Destino.ToVector2 ();
				OnTerminar ();
				return new UpdateReturnType (time, TimeSpan.Zero);
			}
			Unidad.Mover (movDir, time);
			return new UpdateReturnType (time);
		}

		public virtual void OnTerminar ()
		{
			if (Unidad.OrdenActual == this)
				Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public event Action AlTerminar;
	}
}