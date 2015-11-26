using Microsoft.Xna.Framework;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace KarTac.Batalla
{
	public class Unidad : IObjetivo
	{
		public Point Pos { get; set; }

		public Personaje PersonajeBase { get; }

		public Unidad (Personaje personaje)
		{
			PersonajeBase = personaje;
		}

		/// <summary>
		/// Devuelve si esta unidad puede recibir experiencia
		/// </summary>
		public bool PuedeRecibirExp
		{
			get
			{
				return EstáVivo;
			}
		}

		/// <summary>
		/// Devuelve si esta unidad está viva
		/// </summary>
		public bool EstáVivo
		{
			get
			{
				return PersonajeBase.Atributos.HP.Valor > 0;
			}
		}

		public Equipo Equipo { get; set; }

		/// <summary>
		/// Mete la experiencia en su bolsa
		/// </summary>
		public void RecibirExp (double exp)
		{
			if (PuedeRecibirExp)
			{
				BolsaExp += exp;
			}
		}

		/// <summary>
		/// Convierte la bolsa de exp en experiencia real para sus IExp
		/// </summary>
		void commitExp ()
		{
			// Hacer diccionario de IExp s con sus pesos y normalizarlo
			foreach (var petit in PeticiónExpNormalizado())
			{
				petit.Key.RecibirExp (petit.Value * BolsaExp);
			}

			BolsaExp = 0;
		}

		/// <summary>
		/// Devuelve un diccionario de las peticiones de experiencia ya normalizadas
		/// </summary>
		DictionaryTag PeticiónExpNormalizado ()
		{
			var ret = new DictionaryTag ();
			double suma = 0;
			foreach (var x in Experienciables ())
			{
				var s = x.PedirExp ();
				if (s > 0) // Sólo agrega los que sí piden
				{
					ret.Add (x, x.PedirExp ());
					suma += s;
				}
			}

			// Normalizar
			if (suma > 0)
			{
				foreach (var x in ret.Keys)
				{
					ret [x] /= suma;
				}
			}

			return ret;
		}

		/// <summary>
		/// Devuelve una lista de sus experienciables
		/// </summary>
		List<IExp> Experienciables ()
		{
			var ret = new List<IExp> ();

			// Los IExp son ISkill y IRecurso
			ret.Add (PersonajeBase.Atributos.HP);
			foreach (var x in PersonajeBase.Atributos.Recs)
			{
				ret.Add (x);
			}

			foreach (var x in PersonajeBase.Skills)
			{
				ret.Add (x);
			}

			return ret;
		}

		public double BolsaExp { get; private set; }

	}
}