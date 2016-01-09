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

		public override Vector2 VectorDeMuro ()
		{
			var ret = new Vector2 ();
			ret += new Vector2 (1 / (Unidad.PosPrecisa.X - Tamaño.Left), 0);
			ret += new Vector2 (1 / (Unidad.PosPrecisa.X - Tamaño.Right), 0);
			ret += new Vector2 (0, 1 / (Unidad.PosPrecisa.Y - Tamaño.Top));
			ret += new Vector2 (0, 1 / (Unidad.PosPrecisa.Y - Tamaño.Bottom));

			return ret;
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
				return vect * 0.1f;
			}
			return vect * -1;
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
			else
			{
				EjecutarMov (time);
				return new UpdateReturnType (time);
			}
		}
	}
}