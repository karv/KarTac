using Microsoft.Xna.Framework;

namespace KarTac
{
	/// <summary>
	/// Representa un objeto de Campo que se puede mover
	/// </summary>
	public interface IMóvil
	{
		/// <summary>
		/// Devuelve la posición del objeto
		/// </summary>
		Point Posición { get; }

		/// <summary>
		/// Mueve el objeto hacia una dirección dada
		/// </summary>
		void Mover (Vector2 dirección);
	}
}