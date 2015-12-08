using System;
using System.Collections.Generic;
using KarTac.Skills;
using KarTac.Batalla;

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

		public Unidad Unidad { get; private set; }

		public Unidad ConstruirUnidadUnidad (Campo campoBatalla)
		{
			var ret = new Unidad (this, campoBatalla);
			Unidad = ret;
			return ret;
		}

		public void LimpiarUnidad ()
		{
			Unidad = null;
		}

		public Personaje ()
		{
			Atributos = new AtributosPersonaje ();
			Atributos.Empuje = new Empuje (300, 100, 30);
			Skills = new List<ISkill> ();

			// Agregar defaults
			Skills.Add (new Golpe (this));
			Desbloqueables = new HashSet<ISkill> ();
		}

		public IList<ISkill> Skills { get; }

		public HashSet<ISkill> Desbloqueables { get; }

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