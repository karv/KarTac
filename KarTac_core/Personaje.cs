using System;
using System.Collections.Generic;
using KarTac.Skills;

namespace KarTac
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
			Skills = new HashSet<ISkill> ();

			// Agregar defaults
			Skills.Add (new Golpe ());
		}

		public ICollection<ISkill> Skills { get; }

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
	}
}