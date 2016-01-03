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
		/// devuelve el tiempo que requirió en hacerlo, 
		/// (ret leq time implica que terminó)
		/// </summary>
		TimeSpan Update (TimeSpan time);

		event Action AlTerminar;
	}
}