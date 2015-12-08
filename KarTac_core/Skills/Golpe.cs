﻿using KarTac.Batalla;
using System.Linq;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;
using KarTac.Batalla.Orden;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public class Golpe : ISkill
	{
		public Golpe (Personaje usuario)
		{
			Usuario = usuario;
		}

		public Personaje Usuario { get; }

		public Unidad Unidad
		{
			get
			{
				return Usuario.Unidad;
			}
		}

		public double TotalExp { get; private set; }

		public string Nombre
		{
			get
			{
				return "Golpe";
			}
		}

		public string IconTextureName
		{
			get
			{
				return @"Icons/Skills/punch";
			}
		}

		public double PeticiónExpAcumulada { get; private set; }

		public IEnumerable<ISkill> DesbloquearSkills ()
		{
			return new ISkill[0]; // Regresa vacío, por ahora.

		}

		public bool PuedeAprender ()
		{
			return true; // Skill básico, siempre se puede aprender
		}

		public void Ejecutar (Campo campo)
		{
			var selector = campo.SelectorTarget;
			var área = new Círculo (Unidad.Pos, 100);

			selector.MaxSelect = 1;
			selector.PosiblesBlancos = new List<Unidad> (campo.Unidades.Where (x => área.Contiene (x.Pos)).OrderBy (x => Unidad.Equipo.EsAliado (x)));
			selector.IgualdadEstricta = true;
			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += delegate (SelecciónRespuesta obj)
			{
				estado_Seleccionado (obj);	
				selector.ClearStatus (); // Limpia el cache temporal
			};

			selector.Selecciona (Unidad);
		}

		/// <summary>
		/// Calcula el tiempo de uso (post) de esta habilidad.
		/// </summary>
		static TimeSpan CalcularTiempoUso (Unidad unidad)
		{
			return TimeSpan.FromSeconds (3.0 / unidad.AtributosActuales.Agilidad);
		}

		public bool Usable (Campo campo)
		{
			return true; //Siempre me puedo golpear solo :3
		}

		public void CommitExp (double exp)
		{
			TotalExp += exp;
			PeticiónExpAcumulada = 0;
		}

		void estado_Seleccionado (SelecciónRespuesta resp)
		{
			var selección = resp.Selección [0];
			// usuario ataca a selección

			var dañoBloqueado = Math.Max (
				                    Unidad.AtributosActuales.Ataque - selección.AtributosActuales.Defensa,
				                    0);
			var daño = Math.Max (20 - dañoBloqueado, 1);

			selección.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				Unidad,
				daño,
				selección));

			PeticiónExpAcumulada += 1;
			OnTerminar ();

		}

		void OnTerminar ()
		{
			var ordQuieto = new Quieto (Unidad, CalcularTiempoUso (Unidad));
			Unidad.OrdenActual = ordQuieto;
		}
	}
}