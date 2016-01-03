using KarTac.Batalla;
using System;

namespace KarTac.Batalla.Orden
{
	public class OrdenAtacar : IOrden
	{
		public OrdenAtacar (Unidad unid, double distancia)
		{
			Unidad = unid;
			Distancia = distancia;
		}

		public Unidad Unidad { get; }

		public double Distancia { get; set; }

		public event Action AlTerminar;

		public TimeSpan Update (TimeSpan time)
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

			if (lastDistSq < Distancia * Distancia)
			{
				Unidad.OrdenActual = null;
				AlTerminar?.Invoke ();
				return time;
			}
			else
			{
				Unidad.Mover (
					másCercana.PosPrecisa - Unidad.PosPrecisa,
					time);

				return TimeSpan.Zero;
			}
		}
	}
}