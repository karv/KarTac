
namespace KarTac.Batalla.Exp
{
	/// <summary>
	/// Un objeto que puede recibir experiencia
	/// </summary>
	public interface IExp
	{
		/// <summary>
		/// Recive la experiencia ya procesada y neta.
		/// </summary>
		void CommitExp (double exp);

		/// <summary>
		/// Devuelve número no negativo especificando cuánto peso de lo que recibe en exp
		/// </summary>
		double PeticiónExpAcumulada { get; }

		/// <summary>
		/// Acumula experiencia
		/// </summary>
		void AcumularExp (double exp);

		void ResetExp ();
	}
}