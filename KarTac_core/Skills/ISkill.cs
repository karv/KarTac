using KarTac.Batalla.Exp;
using System.Collections.Generic;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public interface ISkill: IExp
	{
		string Nombre { get; }

		Personaje Usuario { get; }

		void Ejecutar ();

		/// <summary>
		/// Determina si es posible usar este skill.
		/// </summary>
		bool Usable { get; }

		string IconTextureName { get; }

		/// <summary>
		/// Se ejecuta después de asignar experiencia.
		/// Debe devolver los skills a la lista de 'podría aprender' de la unidad.
		/// </summary>
		IEnumerable<ISkill> DesbloquearSkills ();

		/// <summary>
		/// Revisa si una Unidad cumple los requicitos para aprender este Skill.
		/// </summary>
		bool PuedeAprender ();

		/// <summary>
		/// Se ejecuta al aprender
		/// </summary>
		void AlAprender ();
	}
}