﻿using System;
using KarTac.Personajes;
using System.Collections.Generic;
using KarTac.Batalla.Shape;
using KarTac.Batalla;
using KarTac.Recursos;

namespace KarTac.Skills
{
	public class RayoManá : SkillTresPasosShaped
	{
		public RayoManá (Personaje usuario)
			: base (usuario)
		{
		}

		public override void AlAprender ()
		{
			Usuario.Atributos.Recs.Add (new Maná ());
		}

		Maná ManáRecurso
		{
			get
			{
				return UnidadUsuario.AtributosActuales.Recs ["Maná"] as Maná;
			}
		}

		protected override ISkillReturnType LastReturn { get; set; }

		public override string Nombre
		{
			get
			{
				return "Rayo mana";
			}
		}

		public override string Descripción
		{
			get
			{
				return "Ataque mágico de buen rango.\nRequiere 5 Maná.";
			}
		}

		public override string IconTextureName
		{
			get
			{
				return @"Icons/Skills/rayo maná";
			}
		}

		public override IEnumerable<ISkill> DesbloquearSkills ()
		{
			return new ISkill[0]; // Regresa vacío, por ahora.
		}

		public override bool PuedeAprender ()
		{
			return Usuario.Atributos.HP.Max >= 105;
		}

		public override double Rango
		{
			get
			{
				return 350;
			}
		}

		public override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, (float)Rango);
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (1);
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			//return TimeSpan.Zero;
			return TimeSpan.FromSeconds (1);
		}

		protected override int MaxSelect
		{
			get
			{
				return 1;
			}
		}

		protected override bool IgualdadEstricta
		{
			get
			{
				return true;
			}
		}

		const float UsaManá = 5;

		public override bool Usable
		{
			get
			{
				return ManáRecurso.Valor >= UsaManá;
			}
		}

		protected override bool SeleccionaTarget (Unidad u)
		{
			return u.EstáVivo && base.SeleccionaTarget (u);
		}

		protected override ISkillReturnType EffectOnTarget (Unidad unid)
		{
			// usuario ataca a selección

			const int daño = 10;

			unid.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				unid));

			ManáRecurso.Valor -= UsaManá;

			PeticiónExpAcumulada += 1.5;
			LastReturn = new SkillReturnType (
				-daño,
				unid.AtributosActuales.HP,
				unid.Pos);

			return LastReturn;
		}
	}
}