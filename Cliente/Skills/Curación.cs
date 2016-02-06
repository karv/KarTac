using System;
using KarTac.Batalla.Shape;

namespace KarTac.Skills
{
	public class Curación : SkillTresPasosShaped
	{
		public Curación (KarTac.Personajes.Personaje usuario)
			: base (usuario)
		{
		}


		public Curación (KarTac.Batalla.Unidad usuario)
			: base (usuario)
		{
		}

		public override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, (float)Rango);
		}

		public override double Rango
		{
			get
			{
				return 80;
			}
		}

		protected override bool SeleccionaTarget (KarTac.Batalla.Unidad u)
		{
			return u.Equipo.EsAliado (UnidadUsuario) && u.EstáVivo && base.SeleccionaTarget (u);
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			return TimeSpan.FromMilliseconds (1200);
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.Zero;
		}

		protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
		{
			var coef = 1 + TotalExp;
			float cura = (float)DamageUtils.CalcularDaño (
				             UnidadUsuario.AtributosActuales ["Poder mágico"],
				             0,
				             coef) + 20;

			unid.AtributosActuales.HP.Valor += cura;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} cura {1} HP a {2}",
				UnidadUsuario,
				cura,
				unid));

			PeticiónExpAcumulada += 1;

			LastReturn = new  SkillReturnType (
				cura,
				unid.AtributosActuales.HP,
				unid.Pos);

			return LastReturn;
		}

		protected override bool IgualdadEstricta
		{
			get
			{
				return true;
			}
		}

		protected override ISkillReturnType LastReturn { get; set; }

		protected override int MaxSelect
		{
			get
			{
				return 1;
			}
		}

		public override string Descripción
		{
			get
			{
				return "Recupera HP de un aliado.";
			}
		}

		public override System.Collections.Generic.IEnumerable<ISkill> DesbloquearSkills ()
		{
			yield break;
		}

		public override string IconTextureName
		{
			get
			{
				return "Rect"; //TODO
			}
		}

		public override string Nombre
		{
			get
			{
				return "Curación";
			}
		}

		public override bool PuedeAprender ()
		{
			return Usuario.Atributos ["Poder mágico"] >= 1;
		}

		public override bool Usable
		{
			get
			{
				return Usuario.Atributos ["Maná"] >= uso_maná;
			}
		}

		protected override void OnTerminar (ISkillReturnType returnInfo)
		{
			Usuario.Atributos.GetRecursoBase ("Maná").Valor -= uso_maná;
			base.OnTerminar (returnInfo);
		}

		const float uso_maná = 1.4f;
	}
}