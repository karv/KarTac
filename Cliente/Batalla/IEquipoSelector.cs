namespace KarTac.Batalla
{
	public interface IEquipoSelector
	{
		/// <summary>
		/// Revisa si una unidad es aliada
		/// </summary>
		bool EsAliado (Unidad unidad);

		/// <summary>
		/// Revisa si una unidad es enemiga
		/// </summary>
		bool EsEnemigo (Unidad unidad);
	}
}