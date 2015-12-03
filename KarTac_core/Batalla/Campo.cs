using System.Collections.Generic;
using System;
using System.Linq;

namespace KarTac.Batalla
{
	/// <summary>
	/// Representa un campo de batalla
	/// </summary>
	public class Campo
	{
		/// <summary>
		/// Experiencia por minuto
		/// </summary>
		public float ExpPorMinuto = 1;

		public ISelectorTarget SelectorTarget { get; set; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

		public Campo ()
		{
			Unidades = new List<Unidad> ();
		}


		public void Tick (TimeSpan delta)
		{
			RecibirExp (delta);
			foreach (var x in Unidades)
			{
				x.AcumularPetición (delta);
			}
		}

		/// <summary>
		/// Unidades vivas reciben exp
		/// </summary>
		void RecibirExp (TimeSpan delta)
		{
			var mins = delta.TotalMinutes;
			foreach (var uni in Unidades.Where (x => x.PuedeRecibirExp))
			{
				uni.RecibirExp (mins * ExpPorMinuto);
			}
		}
	}
}