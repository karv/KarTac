using System.Collections.Generic;
using KarTac.Recursos;
using NUnit.Framework;

namespace KarTac
{
	public class AtributosPersonaje
	{
		public int Ataque { get; set; }

		public int Defensa { get; set; }

		/// <summary>
		/// Max HP
		/// </summary>
		public HP HP { get; set; }

		/// <summary>
		/// Lista de recursos
		/// </summary>
		/// <value>The recursos.</value>
		public List<IRecurso> Recs { get; }

		public AtributosPersonaje ()
		{
			Recs = new List<IRecurso> ();
			HP = new HP ();
		}
	}
}