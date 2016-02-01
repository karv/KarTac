using KarTac.Batalla;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public class Golpe : SkillTresPasosShaped
	{
		public override double Rango
		{
			get
			{
				return 40;
			}
		}

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

		public override string Descripción
		{
			get
			{
				return "Causa daño a un sólo enemigo cercano.\nNo gasta recurso alguno.";
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
			return TimeSpan.FromSeconds (6.0f / UnidadUsuario.AtributosActuales ["Agilidad"]);
		}

		public override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, (float)Rango);
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

		protected override ISkillReturnType LastReturn { get; set; }

		protected override bool SeleccionaTarget (Unidad u)
		{
			return u.EstáVivo && base.SeleccionaTarget (u);
		}

		protected override ISkillReturnType EffectOnTarget (Unidad unid)
		{
			float daño = (float)DamageUtils.CalcularDaño (
				             UnidadUsuario.AtributosActuales.Ataque.Valor,
				             unid.AtributosActuales.Defensa.Valor,
				             2);

			unid.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				unid));

			PeticiónExpAcumulada += 1;
			UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.3;
			unid.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;

			LastReturn = new  SkillReturnType (
				-daño,
				unid.AtributosActuales.HP,
				unid.Pos);

			return LastReturn;
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