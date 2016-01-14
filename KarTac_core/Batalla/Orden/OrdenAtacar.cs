using KarTac.Batalla;
using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class OrdenAtacar : OrdenMovCampoComún
	{
		public OrdenAtacar (Unidad unid, double distancia)
			: base (unid)
		{
			Distancia = distancia;
		}

		/// <summary>
		/// Distancia al objetivo para terminar la ordem
		/// </summary>
		public double Distancia { get; set; }


		public override Vector2 VectorDeMuro ()
		{
			return Vector2.Zero;
		}

		public override Vector2 VectorDeUnidad (Unidad unidad)
		{
			return Vector2.Zero;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			var másCercana = UnidadEnemigoMásCercana ();
			if (másCercana != null)
			{
				var lastDistSq = (másCercana.PosPrecisa - Unidad.PosPrecisa).LengthSquared ();
				if (lastDistSq < Distancia * Distancia)
				{
					OnTerminar ();
					return new UpdateReturnType (time, TimeSpan.Zero);
				}
				Unidad.Mover (
					másCercana.PosPrecisa - Unidad.PosPrecisa,
					time);
			}
			return new UpdateReturnType (time);
		}
	}
}