using System;

namespace KarTac.Recursos
{
	public class Maná : RecursoAcotadoRegenerativo
	{
		public override void CommitExp (double exp)
		{
			Max += (float)exp; //TODO Aquí no creo que termine siendo así de simple.
			PeticiónExpAcumulada = 0;
		}

		public override string Nombre
		{
			get
			{
				return "MP";
			}
		}

		protected override void PedirExp (TimeSpan time, KarTac.Batalla.Campo campo)
		{
			var pct = Valor / Max;

			PeticiónExpAcumulada += (1 - pct) * time.Minutes;
		}

		protected override float Regen
		{
			get
			{
				return 10;
			}
		}
	}
}