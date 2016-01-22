using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla.Objetos;

namespace KarTac.Batalla.Orden
{
	public abstract class OrdenMovCampoComún : IOrden
	{
		protected OrdenMovCampoComún (Unidad unidad)
		{
			Unidad = unidad;
		}

		public Unidad Unidad { get; }

		public Rectangle Tamaño
		{
			get
			{
				return Unidad.CampoBatalla.Área;
			}
		}

		bool IOrden.EsCancelable
		{
			get
			{
				return true;
			}
		}

		public abstract Vector2 VectorDeUnidad (Unidad unidad);

		public abstract Vector2 VectorDeMuro (Pared pared);

		/// <summary>
		/// Devuelve la suma de los vectores de unidad y de muro.
		/// No se normaliza.
		/// </summary>
		public virtual Vector2 VectorMovimiento ()
		{
			var vectorMov = new Vector2 ();
			foreach (var u in Unidad.CampoBatalla.UnidadesVivas)
			{
				vectorMov += VectorDeUnidad (u);
			}

			// Sumar las paredes
			foreach (var pared in Unidad.CampoBatalla.Paredes)
			{
				vectorMov += VectorDeMuro (pared);
			}

			return vectorMov;
		}

		protected virtual void EjecutarMov (TimeSpan time)
		{
			var vectorMov = VectorMovimiento ();
			Unidad.Mover (vectorMov, time);
		}

		protected virtual void OnTerminar ()
		{
			Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public abstract UpdateReturnType Update (TimeSpan time);

		/// <summary>
		/// Devuelve la unidad viva más cercana que satisface un predicado
		/// </summary>
		protected Unidad UnidadMás (Predicate<Unidad> pred)
		{
			Unidad másCercana = null;
			double lastDistSq = double.PositiveInfinity;
			foreach (var x in Unidad.CampoBatalla.UnidadesVivas)
			{
				if (pred (x))
				{
					var distSq = (x.PosPrecisa - Unidad.PosPrecisa).LengthSquared ();
					if (distSq < lastDistSq)
					{
						lastDistSq = distSq;
						másCercana = x;
					}
				}
			}
			return másCercana;
		}

		/// <summary>
		/// Devuelve el enemigo vivo más cecano
		/// </summary>
		/// <returns>The enemigo más cercana.</returns>
		protected Unidad UnidadEnemigoMásCercana ()
		{
			return UnidadMás (Unidad.Equipo.EsEnemigo);
		}

		/// <summary>
		/// Devuelve el aliado vivo más cercano.
		/// </summary>
		/// <returns>The aliado más cercana.</returns>
		protected Unidad UnidadAliadoMásCercana ()
		{
			return UnidadMás (u => !Unidad.Equals (u) && Unidad.Equipo.EsAliado (u));
		}

		public event Action AlTerminar;
	}
}