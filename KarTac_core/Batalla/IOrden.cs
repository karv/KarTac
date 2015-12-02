namespace KarTac.Batalla
{
	/// <summary>
	/// Da la orden de qué hacer en cada Update de Unidad,
	/// en términos de primitivos
	/// </summary>
	public interface IOrden
	{
		Unidad Unidad { get; }

		void Update ();
	}
}