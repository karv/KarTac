
namespace KarTac
{
	/// <summary>
	/// Un objeto que puede recibir experiencia
	/// </summary>
	public interface IExp
	{
		void RecibirExp (double exp);

		/// <summary>
		/// Devuelve número no negativo especificando cuánto peso de lo que recibe en exp
		/// </summary>
		double PeticiónExpAcumulada { get; }

	}
}