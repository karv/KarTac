﻿using KarTac.Recursos;

namespace KarTac.Personaje
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
		public HP HP
		{
			get
			{
				return Recs ["HP"] as HP;
			}
		}

		/// <summary>
		/// Lista de recursos
		/// </summary>
		/// <value>The recursos.</value>
		public ListaRecursos Recs { get; }

		public AtributosPersonaje ()
		{
			Recs = new ListaRecursos ();
			Recs.Add (new HP ());
		}

		public AtributosPersonaje Clonar ()
		{
			return MemberwiseClone () as AtributosPersonaje;
		}
	}
}