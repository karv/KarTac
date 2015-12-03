﻿using Microsoft.Xna.Framework;

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
		const float _distanciaCercano = 10;

		public Point Destino;

		public Unidad Unidad { get; }

		public Movimiento (Unidad unidad)
		{
			Unidad = unidad;
		}

		public bool Update (GameTime time)
		{
			var movDir = (Destino - Unidad.Pos).ToVector2 ();
			if (movDir.Length () < _distanciaCercano)
			{
				Unidad.Pos = Destino;
				OnTerminar ();
				return true;
			}
			movDir.Normalize ();
			movDir *= Unidad.AtributosActuales.Velocidad * (float)time.ElapsedGameTime.TotalSeconds;
			Unidad.Pos = Unidad.Pos + movDir.ToPoint (); //TODO: ¿Será mejor hacer que Unidad.Pos sea vector, con un getter obtener su Point?
			return false;
		}

		public virtual void OnTerminar ()
		{
			if (Unidad.OrdenActual == this)
				Unidad.OrdenActual = null;
		}
	}
}