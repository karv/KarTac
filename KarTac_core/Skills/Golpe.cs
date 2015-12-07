﻿using KarTac.Batalla;
using System.Linq;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;
using KarTac.Batalla.Exp;
using KarTac.Batalla.Orden;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public class Golpe : ISkill
	{
		public Golpe ()
		{
			ExpTags = new DictionaryTag ();
		}

		public double TotalExp { get; private set; }

		public string Nombre
		{
			get
			{
				return "Golpe";
			}
		}

		public ITagging ExpTags { get; }

		public string IconTextureName
		{
			get
			{
				return @"Icons/Skills/punch";
			}
		}

		public double PeticiónExpAcumulada { get; private set; }

		public IEnumerable<ISkill> DesbloquearSkills (Personaje persona)
		{
			return new ISkill[0]; // Regresa vacío, por ahora.

		}

		public bool PuedeAprender (Personaje unidad)
		{
			return true; // Skill básico, siempre se puede aprender
		}

		public void Ejecutar (Unidad usuario, Campo campo)
		{
			var selector = campo.SelectorTarget;
			var área = new Círculo (usuario.Pos, 100);

			selector.MaxSelect = 1;
			selector.PosiblesBlancos = new List<Unidad> (campo.Unidades.Where (x => área.Contiene (x.Pos)).OrderBy (x => usuario.Equipo.EsAliado (x)));
			selector.IgualdadEstricta = true;
			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += delegate (SelecciónRespuesta obj)
			{
				estado_Seleccionado (obj, usuario);	
				selector.ClearStatus (); // Limpia el cache temporal
			};

			selector.Selecciona (usuario);
		}

		/// <summary>
		/// Calcula el tiempo de uso (post) de esta habilidad.
		/// </summary>
		static TimeSpan CalcularTiempoUso (Unidad unidad)
		{
			return TimeSpan.FromSeconds (3.0 / unidad.AtributosActuales.Agilidad);
		}

		public bool Usable (Unidad usuario, Campo campo)
		{
			return true; //Siempre me puedo golpear solo :3
		}

		public void RecibirExp (double exp)
		{
			TotalExp += exp;
			PeticiónExpAcumulada = 0;
		}

		void estado_Seleccionado (SelecciónRespuesta resp,
		                          Unidad usuario)
		{
			var selección = resp.Selección [0];
			// usuario ataca a selección

			var dañoBloqueado = Math.Max (
				                    usuario.AtributosActuales.Ataque - selección.AtributosActuales.Defensa,
				                    0);
			var daño = Math.Max (20 - dañoBloqueado, 1);

			selección.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				usuario,
				daño,
				selección));

			PeticiónExpAcumulada += 1;
			OnTerminar (usuario);

		}

		static void OnTerminar (Unidad usuario)
		{
			var ordQuieto = new Quieto (usuario, CalcularTiempoUso (usuario));
			usuario.OrdenActual = ordQuieto;
		}
	}
}