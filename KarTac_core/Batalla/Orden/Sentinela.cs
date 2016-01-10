using System;

namespace KarTac.Batalla.Orden
{
	public class Sentinela : Quieto
	{
		/// <summary>
		/// El rango de alerta de sentinela
		/// Se 'despierta' cuando una unidad enemiga está a distancia menor que este valor.
		/// </summary>
		public float RangoAlerta { get; set; }

		public Sentinela (Unidad unidad,
		                  TimeSpan tiempo,
		                  float rangoAlerta = 0)
			: base (unidad, tiempo)
		{
			RangoAlerta = rangoAlerta;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			var ret = base.Update (time);

			foreach (var unid in Unidad.CampoBatalla.UnidadesVivas)
			{
				if (Unidad.Equipo.EsEnemigo (unid))
				{
					var dist = (unid.PosPrecisa - Unidad.PosPrecisa).Length ();
					if (dist < RangoAlerta)
					{
						OnTerminar ();
						return new UpdateReturnType (time, ret.TiempoUsado);
					}
				}
			}
			return ret;
		}
	}
}