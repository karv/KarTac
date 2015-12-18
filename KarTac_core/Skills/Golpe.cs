using KarTac.Batalla;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public class Golpe : SkillTresPasosShaped
	{
		public const float Alcance = 40;

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
			return TimeSpan.FromSeconds (6.0f / UnidadUsuario.AtributosActuales.Recs ["Agilidad"].Valor);
		}

		public override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, Alcance);
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

		public override void Terminal (SelecciónRespuesta obj)
		{
			estado_Seleccionado (obj);
		}

		protected override bool SeleccionaTarget (Unidad u)
		{
			return u.EstáVivo && base.SeleccionaTarget (u);
		}

		void estado_Seleccionado (SelecciónRespuesta resp)
		{
			var selección = resp.Selección [0];
			// usuario ataca a selección

			var dañoBloqueado = Math.Max (
				                    UnidadUsuario.AtributosActuales.Recs ["Ataque"].Valor - selección.AtributosActuales.Recs ["Defensa"].Valor,
				                    0);
			var daño = Math.Max (20 - dañoBloqueado, 1);

			selección.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				selección));

			PeticiónExpAcumulada += 1;
			UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.3;
			selección.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;

			LastReturn = new  SkillReturnType (
				-daño,
				selección.AtributosActuales.HP,
				selección.Pos);
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