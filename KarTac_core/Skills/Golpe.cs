﻿using KarTac.Batalla;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public class Golpe : SkillTresPasosShaped
	{
		public Golpe (Personaje usuario)
			: base (usuario)
		{
		}

		public override string Nombre
		{
			get
			{
				return "Golpe";
			}
		}

		public override string IconTextureName
		{
			get
			{
				return @"Icons/Skills/punch";
			}
		}

		public override IEnumerable<ISkill> DesbloquearSkills ()
		{
			yield return new RayoManá (Usuario);
		}

		public override bool PuedeAprender ()
		{
			return false; // Skill básico, ya está aprendido
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			return TimeSpan.Zero;
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (3.0 / UnidadUsuario.AtributosActuales.Agilidad);
		}

		protected override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, 60);
		}

		protected override bool IgualdadEstricta
		{
			get
			{
				return true;
			}
		}

		protected override int MaxSelect
		{
			get
			{
				return 1;
			}
		}

		public override void Terminal (SelecciónRespuesta obj)
		{
			estado_Seleccionado (obj);
		}

		void estado_Seleccionado (SelecciónRespuesta resp)
		{
			var selección = resp.Selección [0];
			// usuario ataca a selección

			var dañoBloqueado = Math.Max (
				                    UnidadUsuario.AtributosActuales.Ataque - selección.AtributosActuales.Defensa,
				                    0);
			var daño = Math.Max (20 - dañoBloqueado, 1);

			selección.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				selección));

			PeticiónExpAcumulada += 1;
		}

		public override bool Usable
		{
			get
			{
				return true;
			}
		}
	}
}