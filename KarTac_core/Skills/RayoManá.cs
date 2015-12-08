using System;
using KarTac.Personajes;
using System.Collections.Generic;
using KarTac.Batalla.Shape;
using KarTac.Batalla;

namespace KarTac.Skills
{
	public class RayoManá:SkillTresPasosShaped
	{
		public RayoManá (Personaje usuario)
			: base (usuario)
		{
		}

		public override string Nombre
		{
			get
			{
				return "Rayo mana";
			}
		}

		public override string IconTextureName
		{
			get
			{
				return "Rect";
			}
		}

		public override IEnumerable<ISkill> DesbloquearSkills ()
		{
			return new ISkill[0]; // Regresa vacío, por ahora.
		}

		public override bool PuedeAprender ()
		{
			return false; // Skill básico, ya está aprendido
		}

		protected override IShape GetÁrea ()
		{
			return new Círculo (UnidadUsuario.Pos, 350);
		}

		protected override TimeSpan CalcularTiempoUso ()
		{
			return TimeSpan.FromSeconds (1);
		}

		protected override TimeSpan CalcularTiempoPreparación ()
		{
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

		public override bool Usable ()
		{
			return true;
		}

		public override void Terminal (SelecciónRespuesta obj)
		{
			var selección = obj.Selección [0];
			// usuario ataca a selección

			const int daño = 10;

			selección.AtributosActuales.HP.Valor -= daño;
			System.Diagnostics.Debug.WriteLine (string.Format (
				"{0} causa {1} daño HP a {2}",
				UnidadUsuario,
				daño,
				selección));

			PeticiónExpAcumulada += 1.5;
		}

	}
}