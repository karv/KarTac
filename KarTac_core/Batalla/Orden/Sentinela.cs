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

		public override TimeSpan Update (TimeSpan time)
		{
			var ret = base.Update (time);

			foreach (var unid in Unidad.CampoBatalla.Unidades)
			{
				if (Unidad.Equipo.EsEnemigo (unid))
				{
					OnTerminar ();
					return TimeSpan.Zero;
				}
			}
			return ret;
		}
	}
}