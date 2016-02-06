using System;

namespace KarTac.Batalla.Objetos
{
	public interface IObjetoCampo
	{
		Campo Campo { get; }
	}

	/// <summary>
	/// Represnta un objeto de campo que puede interactuar
	/// Ofrece métodos Update y Draw
	/// </summary>
	public interface IObjeto : IObjetoCampo
	{
		void Dibujar ();

		void Update (TimeSpan tme);
	}
}