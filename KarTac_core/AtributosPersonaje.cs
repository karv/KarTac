﻿using System.Collections.Generic;
using KarTac.Recursos;

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
		SortedSet<IRecurso> Recursos { get; }

		public AtributosPersonaje ()
		{
			Recursos = new SortedSet<IRecurso> ();
		}
	}
}