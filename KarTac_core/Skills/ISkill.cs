using KarTac.Batalla;
using KarTac.Batalla.Exp;
using System.Collections.Generic;
using KarTac.Personajes;

namespace KarTac.Skills
{
	public interface ISkill: IExp
	{
		string Nombre { get; }

		void Ejecutar (Unidad usuario, Campo campo);

		/// <summary>
		/// Determina si es posible usar este skill.
		/// </summary>
		/// <param name="campo">Campo.</param>
		/// <param name="usuario">Usuario</param>
		bool Usable (Unidad usuario, Campo campo);

		string IconTextureName { get; }

		/// <summary>
		/// Tags de experiencia
		/// </summary>
		ITagging ExpTags { get; }

		/// <summary>
		/// Se ejecuta después de asignar experiencia.
		/// Debe devolver los skills a la lista de 'podría aprender' de la unidad.
		/// </summary>
		IEnumerable<ISkill> DesbloquearSkills (Personaje  personaje);

		/// <summary>
		/// Revisa si una Unidad cumple los requicitos para aprender este Skill.
		/// </summary>
		bool PuedeAprender (Personaje persona);
	}
}