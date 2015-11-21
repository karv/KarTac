using System.Collections.Generic;
using System;
using System.Linq;
using KarTac.Batalla.Exp;

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

		public ISelectorTarget SelectorTarget { get; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

		public Campo ()
		{
			Unidades = new List<Unidad> ();
			ExpDict = new Dictionary<Unidad, KarTac.Batalla.Exp.TotalExp> ();
		}

		public Dictionary<Unidad, Exp.TotalExp> ExpDict { get; }


		public void Tick (TimeSpan delta)
		{
			RecibirExp (delta);
		}

		/// <summary>
		/// Unidades vivas reciben exp
		/// </summary>
		void RecibirExp (TimeSpan delta)
		{
			var mins = (float)delta.TotalMinutes;
			foreach (var uni in Unidades.Where (x => x.PuedeRecibirExp))
			{
				TotalExp exp;
				ExpDict.TryGetValue (uni, out exp);
				exp.ExpVivo += mins * ExpPorMinuto;
			}
		}
	}
}