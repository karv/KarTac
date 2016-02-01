using System;

namespace KarTac
{
	public interface IModificador
	{
		/// <summary>
		/// Atributo que modifica
		/// </summary>
		string Atributo { get; }

		/// <summary>
		/// Cuánto modifica
		/// </summary>
		float Delta { get; }
	}
}