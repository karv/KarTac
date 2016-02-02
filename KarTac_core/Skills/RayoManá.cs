using System;
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
			Usuario.Atributos.Add (new Maná ());
			UnidadUsuario.AtributosActuales.Add (
				new AtributoGenérico (
					"Poder mágico",
					true));	
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
			yield return new Curación (Usuario);
		}

		public override bool PuedeAprender ()
		{
			return Usuario.Atributos.HP.Max >= 105;
		}

		public override double Rango
		{
			get
			{
				return 90;
			}
		}

		public override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, (float)Rango);
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (0.5);
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			//return TimeSpan.Zero;
			return TimeSpan.FromSeconds (0.1);
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

		const float UsaManá = 1;

		public override bool Usable
		{
			get
			{
				return UnidadUsuario.AtributosActuales ["Maná"] >= UsaManá;
			}
		}

		protected override bool SeleccionaTarget (Unidad u)
		{
			return u.EstáVivo && base.SeleccionaTarget (u);
		}

		protected override ISkillReturnType EffectOnTarget (Unidad unid)
		{
			var coef = 8 + 2 * TotalExp;
			float daño = (float)DamageUtils.CalcularDaño (
				             UnidadUsuario.AtributosActuales ["Poder mágico"],
				             unid.AtributosActuales.Defensa.Valor / 10,
				             coef);

			unid.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				unid));

			UnidadUsuario.AtributosActuales.GetRecursoBase ("Maná").Valor -= UsaManá;

			PeticiónExpAcumulada += 1.5;
			UnidadUsuario.AtributosActuales.GetRecursoBase ("Maná").AcumularExp (0.4f);
			LastReturn = new SkillReturnType (
				-daño,
				unid.AtributosActuales.HP,
				unid.Pos);
			return LastReturn;
		}

	}
}