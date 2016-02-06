using System;
using KarTac.Batalla.Shape;
using KarTac.Batalla;
using KarTac.Recursos;
using KarTac.Controls.Objetos;
using Moggle.Controles;

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
			var coef = 8 + 2 * TotalExp;

			// TODO: No hacer que esta clase herede a SkillTresPasos, esos son sólo para targets unidad
			var ef = new EfectoDaño (CampoBatalla.BattleScreen);
			ef.Centro = unid.Pos;
			ef.Radio = 30;
			ef.PoderDaño = 1 + atrFuego;
			ef.DuraciónRestante = TimeSpan.FromSeconds (atrPM);
			ef.Coef = coef;
			ef.PoderDefensivo = u => u.AtributosActuales ["Defensa fuego"];
			ef.Include ();
			LastReturn = new SkillReturnType (
				0,
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