using System;
using System.Collections.Generic;
using KarTac.Skills;

namespace KarTac.Personajes
{
	public class Personaje
	{
		/// <summary>
		/// Nombre del personaje
		/// </summary>
		/// <value>The nombre.</value>
		public string Nombre { get; set; }

		/// <summary>
		/// Atributos del personaje
		/// </summary>
		/// <value>The atributos.</value>
		public AtributosPersonaje Atributos { get; }

		public Personaje ()
		{
			Atributos = new AtributosPersonaje ();
			Skills = new List<ISkill> ();

			// Agregar defaults
			Skills.Add (new Golpe ());
		}

		public IList<ISkill> Skills { get; }

		public event Action AlMorir
		{
			add
			{
				Atributos.HP.AlValorCero += value;
			}
			remove
			{
				Atributos.HP.AlValorCero -= value;
			}
		}

		//	public InteracciónHumano
	}
}