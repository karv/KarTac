using System;
using KarTac.Batalla;

namespace KarTac.Recursos
{
	public class HP : RecursoAcotado
	{

		public override string Nombre
		{
			get
			{
				return "HP";
			}
		}


		/// <summary>
		/// HP regenerada por minuto
		/// </summary>
		public float Regen { get; set; }

		public override void CommitExp (double exp)
		{
			Max += (float)exp; //TODO Aquí no creo que termine siendo así de simple.
			PeticiónExpAcumulada = 0;
		}

		protected override void PedirExp (TimeSpan time, Campo campo)
		{
			var pct = Valor / Max;

			PeticiónExpAcumulada += (1 - pct) * time.Minutes;
		}


	}
}