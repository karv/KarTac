using KarTac.Batalla;


namespace KarTac
{
	public interface IInteractor
	{
		Unidad Unidad { get; }

		void Ejecutar ();
	}
}