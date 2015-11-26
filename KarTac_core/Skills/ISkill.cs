using KarTac.Batalla;


namespace KarTac.Skills
{
	public interface ISkill
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
	}
}