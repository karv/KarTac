﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using KarTac.Recursos;
using System;
using KarTac.Batalla.Orden;
using KarTac.Personajes;
using KarTac.Batalla.Exp;

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

		Vector2 _posPrecisa;

		public Vector2 PosPrecisa
		{
			get
			{
				return _posPrecisa;
			}
			set
			{
				float x = Math.Min (Math.Max (value.X, 0), CampoBatalla.Área.Width);
				float y = Math.Min (Math.Max (value.Y, 0), CampoBatalla.Área.Height);
				_posPrecisa = new Vector2 (x, y);
			}
		}

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
		public void CommitExp ()
		{
			// Hacer diccionario de IExp s con sus pesos y normalizarlo
			foreach (var petit in PeticiónExpNormalizado)
			{
				petit.Key.RecibirExp (petit.Value * BolsaExp);
			}

			BolsaExp = 0;
		}

		/// <summary>
		/// Mueve la unidad una dirección específica
		/// </summary>
		/// <param name="movDir">Dirección</param>
		/// <param name="time">durante el tiempo</param>
		public void Mover (Vector2 movDir, TimeSpan time)
		{
			movDir.Normalize ();
			movDir *= AtributosActuales.Velocidad * (float)time.TotalSeconds;
			PosPrecisa += movDir;

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

		public override string ToString ()
		{
			return PersonajeBase.Nombre;
		}
	}
}