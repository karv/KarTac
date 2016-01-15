using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class Rodear : OrdenMovCampoComún
	{
		public Rodear (Unidad unidad, double dist = 0)
			: base (unidad)
		{
			Distancia = dist;
		}

		/// <summary>
		/// Distancia al objetivo para terminar la ordem
		/// </summary>
		public double Distancia { get; set; }

		public override Vector2 VectorDeMuro (KarTac.Batalla.Objetos.Pared pared)
		{
			var norm = pared.Normal (Unidad.PosPrecisa);
			return norm / norm.LengthSquared ();
		}

		public override Vector2 VectorDeUnidad (Unidad unidad)
		{
			if (Unidad.Equals (unidad))
				return Vector2.Zero;
			var vect = (Unidad.PosPrecisa - unidad.PosPrecisa);
			vect /= vect.LengthSquared ();
			if (Unidad.Equipo.EsAliado (unidad))
			{
				// Alejarse un poco para rodear
				return vect * 1f;
			}
			return Vector2.Zero;
		}

		public override Vector2 VectorMovimiento ()
		{
			var másCercana = UnidadEnemigoMásCercana ();
			var ret = base.VectorMovimiento ();
			if (másCercana != null)
			{
				var vect = (Unidad.PosPrecisa - másCercana.PosPrecisa);
				vect *= -3 / vect.LengthSquared ();
				ret += vect;
			}

			return ret;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			var másCercana = UnidadEnemigoMásCercana ();
			if (másCercana == null)
			{
				OnTerminar ();
				return new UpdateReturnType (time, TimeSpan.Zero);
			}

			var lastDistSq = (másCercana.PosPrecisa - Unidad.PosPrecisa).LengthSquared ();
			if (lastDistSq < Distancia * Distancia)
			{
				OnTerminar ();
				return new UpdateReturnType (time, TimeSpan.Zero);
			}

			EjecutarMov (time);
			return new UpdateReturnType (time);
		}
	}
}