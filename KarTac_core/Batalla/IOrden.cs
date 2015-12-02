namespace KarTac.Batalla
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
		bool Update ();
	}
}