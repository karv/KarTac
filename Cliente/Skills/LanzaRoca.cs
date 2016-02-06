using System;
using KarTac.Personajes;
using KarTac.Batalla.Shape;

namespace KarTac.Skills
{
	public class LanzaRoca : SkillTresPasosShaped
	{
		public LanzaRoca (Personaje usuario)
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
				return 150;
			}
		}

		protected override bool SeleccionaTarget (KarTac.Batalla.Unidad u)
		{
			return base.SeleccionaTarget (u) && u.EstáVivo;
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
			return TimeSpan.FromSeconds (6.0f / UnidadUsuario.AtributosActuales.Agilidad.Valor);
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (24.0f / UnidadUsuario.AtributosActuales.Agilidad.Valor);
		}

		protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
		{
			var dañoBloqueado = Math.Max (
				                    unid.AtributosActuales ["Defensa"] - UnidadUsuario.AtributosActuales ["Ataque"],
				                    0);
			var daño = Math.Max (15 - dañoBloqueado, 1);

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
				return "Lanza una piedra al enemigo.\nPoco daño pero buen rango.\nBueno en persecuciones.";
			}
		}

		public override string IconTextureName
		{
			get
			{
				return @"Icons/Skills/roca";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Lanzar piedra";
			}
		}

		public override bool PuedeAprender ()
		{
			return false;
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

