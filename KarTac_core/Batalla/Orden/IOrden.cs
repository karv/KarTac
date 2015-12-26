using System;

namespace KarTac.Batalla.Orden
{
	/// <summary>
	/// Da la orden de qué hacer en cada Update de Unidad,
	/// en términos de primitivos
	/// </summary>
	public interface IOrden
	{
		Unidad Unidad { get; }

		/// <summary>
		/// Realiza la orden a la unidad,
		/// devuelve true si terminó la orden
		/// </summary>
		bool Update (TimeSpan time);

		event Action AlTerminar;
	}
}