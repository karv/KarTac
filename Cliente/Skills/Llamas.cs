using System;
using KarTac.Batalla.Shape;
using KarTac.Batalla;
using KarTac.Recursos;

namespace KarTac.Skills
{
	public class Llamas: SkillTresPasosShaped
	{
		public Llamas (KarTac.Personajes.Personaje usuario)
			: base (usuario)
		{
		}


		public Llamas (Unidad usuario)
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
				return 100 + 10 * TotalExp;
			}
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (0.5);
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			return TimeSpan.FromSeconds (0.1);
		}

		protected override bool SeleccionaTarget (Unidad u)
		{
			return u.EstáVivo && base.SeleccionaTarget (u);
		}

		public override bool Usable
		{
			get
			{
				return ManáRecurso.Valor >= UsaManá;
			}
		}

		protected override ISkillReturnType EffectOnTarget (Unidad unid)
		{
			var atrPM = UnidadUsuario.AtributosActuales ["Poder mágico"];
			var atrFuego = UnidadUsuario.AtributosActuales ["Poder fuego"];
			var atrDefFuego = unid.AtributosActuales ["Defensa fuego"];
			var coef = 8 + 2 * TotalExp;


			float daño = (float)DamageUtils.CalcularDaño (
				             atrPM + atrFuego,
				             atrDefFuego,
				             coef);

			unid.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				unid));

			ManáRecurso.Valor -= UsaManá;

			PeticiónExpAcumulada += 1;
			UnidadUsuario.AtributosActuales.GetRecursoBase ("Poder mágico").AcumularExp (0.4);
			UnidadUsuario.AtributosActuales.GetRecursoBase ("Poder fuego").AcumularExp (0.4);
			UnidadUsuario.AtributosActuales.GetRecursoBase ("Defensa mágico").AcumularExp (0.4);
			LastReturn = new SkillReturnType (
				-daño,
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

		public override System.Collections.Generic.IEnumerable<ISkill> DesbloquearSkills ()
		{
			yield break;
		}

		public override string Descripción
		{
			get
			{
				return "Abrasa a un enemigo";
			}
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
				return "Llamas";
			}
		}

		public override bool PuedeAprender ()
		{
			return UnidadUsuario.AtributosActuales ["Poder mágico"] > 2 && ManáRecurso.Max > 7;
		}

		Maná ManáRecurso
		{
			get
			{
				return UnidadUsuario.AtributosActuales.GetRecursoBase ("Maná") as Maná;
			}
		}

		const float UsaManá = 1.2f;

	}
}