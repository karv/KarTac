using System;

namespace KarTac.Batalla.Orden
{
	public class MantenerDistancia : IOrden
	{
		public MantenerDistancia (Unidad unidad, double minDist, double maxDist)
		{
			Unidad = unidad;
			MinDist = minDist < 30 ? 0 : minDist;
			MaxDist = maxDist;
		}

		public Campo Campo
		{
			get
			{
				return Unidad.CampoBatalla;
			}
		}

		public event Action AlTerminar;

		IOrden suborden;

		public UpdateReturnType Update (TimeSpan time)
		{
			if (suborden != null)
				return suborden.Update (time);
			
			var cerca = Campo.UnidadMásCercana (
				            Unidad.PosPrecisa,
				            Unidad.Equipo.EsEnemigo);

			if (cerca != null)
			{
				var dist = (Unidad.PosPrecisa - cerca.PosPrecisa).Length ();
				// Analysis disable ConvertIfStatementToConditionalTernaryExpression
				if (dist > MaxDist)
				{
					suborden = new Rodear (Unidad, MaxDist);
					suborden.AlTerminar += Terminal;
					return suborden.Update (time);
				}
				suborden = new Huir (Unidad, time);
				suborden.Update (time);
				suborden = null;
				
				// Analysis restore ConvertIfStatementToConditionalTernaryExpression
				return new UpdateReturnType (
					time,
					time);
			}
			return new UpdateReturnType ();
		}

		void Terminal ()
		{
			Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public Unidad Unidad { get; }

		public bool EsCancelable
		{
			get
			{
				return suborden?.EsCancelable ?? true;
			}
		}

		public double MinDist { get; }

		public double MaxDist { get; }

	}
}

