﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using KarTac.Recursos;
using System;
using KarTac.Batalla.Orden;
using KarTac.Personajes;
using KarTac.Batalla.Exp;

namespace KarTac.Batalla
{
	public class Unidad : IObjetivo, IAccionable, IMóvil
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

		public IEnumerable<IExp> Expables
		{
			get
			{
				foreach (var x in PersonajeBase.Skills)
				{
					yield return x;
				}
				foreach (var x in AtributosActuales.Recs.Values)
				{
					yield return x;
				}
			}
		}

		public Campo CampoBatalla { get; }

		public Personaje PersonajeBase { get; }

		public IInteractor Interactor { get; set; }

		public IOrden OrdenActual { get; set; }

		public AtributosPersonaje AtributosActuales
		{
			get
			{
				return PersonajeBase.Atributos;
			}
		}

		public Unidad (Personaje personaje, Campo campo)
		{
			PersonajeBase = personaje;
			CampoBatalla = campo;
			PosPrecisa = new Vector2 ();
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
			// Obtener suma
			double suma = 0;
			foreach (var x in Expables)
			{
				suma += x.PeticiónExpAcumulada;
			}

			if (suma > 0)
			{
				foreach (var petit in Expables)
				{
					petit.CommitExp (BolsaExp / suma);
				}
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
			movDir *= AtributosActuales.Recs ["Velocidad"].Valor * (float)time.TotalSeconds;
			PosPrecisa += movDir;
			AtributosActuales.Velocidad.PeticiónExpAcumulada += time.TotalSeconds * 1;
		}

		public void AcumularPetición (TimeSpan time)
		{
			foreach (var x in PersonajeBase.Atributos.Recs.Values)
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
			foreach (var x in PersonajeBase.Atributos.Recs.Values)
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

		public void AvanzarTiempo (TimeSpan time)
		{
			// TODO
			if (OrdenActual == null)
				OrdenActual = null;
			else
				OrdenActual?.Update (time);
		}

		public void Mover (Vector2 dirección)
		{
			throw new NotImplementedException ();
		}

		Point IMóvil.Posición
		{
			get
			{
				return Pos;
			}
		}

		public override string ToString ()
		{
			return PersonajeBase.Nombre;
		}
	}
}