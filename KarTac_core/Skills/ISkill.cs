using KarTac.Batalla;


namespace KarTac.Skills
{
	public interface ISkill
	{
		string Nombre { get; }

		void Ejecutar (Campo campo);
	}
}

