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

		public double Distancia { get; set; }


		Unidad UnidadMáxCercana ()
		{
			Unidad másCercana = null;
			double lastDistSq = double.PositiveInfinity;
			foreach (var x in Unidad.CampoBatalla.UnidadesVivas)
			{
				var distSq = (x.PosPrecisa - Unidad.PosPrecisa).LengthSquared ();
				if (Unidad.Equipo.EsEnemigo (x) && distSq < lastDistSq)
				{
					lastDistSq = distSq;
					másCercana = x;
				}
			}
			return másCercana;
		}

		public override Microsoft.Xna.Framework.Vector2 VectorDeMuro ()
		{
			return Vector2.Zero;
		}

		public override Vector2 VectorDeUnidad (Unidad unidad)
		{
			return Vector2.Zero;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			var másCercana = UnidadMáxCercana ();
			var lastDistSq = (másCercana.PosPrecisa - Unidad.PosPrecisa).LengthSquared ();
			if (lastDistSq < Distancia * Distancia)
			{
				OnTerminar ();
				return new UpdateReturnType (time, TimeSpan.Zero);
			}
			else
			{
				if (másCercana != null)
					Unidad.Mover (
						másCercana.PosPrecisa - Unidad.PosPrecisa,
						time);

				return new UpdateReturnType (time);
			}
		}
	}
}