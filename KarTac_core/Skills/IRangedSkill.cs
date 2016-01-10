namespace KarTac.Skills
{
	/// <summary>
	/// Una habilidad que tiene rango de uso
	/// </summary>
	public interface IRangedSkill : ISkill
	{
		double Rango { get; }
	}
}