using Microsoft.Xna.Framework;
using System.Collections.Generic;
using KarTac.Recursos;
using System;


namespace KarTac.Batalla
{
	public class Unidad : IObjetivo
	{
		public Point Pos
		{
			get
			{
				return PosPrecisa.ToPoint ();
			}
		}

		public Vector2 PosPrecisa { get; set; }

		public Campo CampoBatalla { get; }

		public Personaje PersonajeBase { get; }

		public IInteractor Interactor { get; set; }

		public IOrden OrdenActual { get; set; }

		public AtributosPersonaje AtributosActuales { get; private set; }

		public Unidad (Personaje personaje, Campo campo)
		{
			PersonajeBase = personaje;
			CampoBatalla = campo;
			AtributosActuales = PersonajeBase.Atributos.Clonar ();
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
			foreach (var petit in PeticiónExpNormalizado)
			{
				petit.Key.RecibirExp (petit.Value * BolsaExp);
			}

			BolsaExp = 0;
		}



		/// <summary>
		/// Devuelve un diccionario de las peticiones de experiencia ya normalizadas
		/// </summary>
		DictionaryTag PeticiónExpNormalizado = new DictionaryTag ();

		void NormalizarPetición ()
		{
			double suma = 0;
			foreach (var x in PeticiónExpNormalizado.Values)
			{
				suma += x;
			}

			// Normalizar
			if (suma > 0)
			{
				foreach (var x in PeticiónExpNormalizado.Keys)
				{
					PeticiónExpNormalizado [x] /= suma;
				}
			}
		}

		public void AcumularPetición (TimeSpan time)
		{
			foreach (var x in PersonajeBase.Atributos.Recs)
			{
				x.PedirExp (time, CampoBatalla);
			}
			(PersonajeBase.Atributos.HP as IRecurso).PedirExp (time, CampoBatalla);
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