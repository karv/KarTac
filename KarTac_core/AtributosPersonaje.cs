using System.Collections.Generic;
using KarTac.Recursos;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace KarTac
{
	public class AtributosPersonaje
	{
		public int Ataque { get; set; }

		public int Defensa { get; set; }

		/// <summary>
		/// Que tan rápido recorre terreno.
		/// Pixeles por segundo.
		/// </summary>
		public int Velocidad { get; set; }

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
			Recs.Add (HP);
		}

		public AtributosPersonaje Clonar ()
		{
			return MemberwiseClone () as AtributosPersonaje;
		}
	}
}