using KarTac.Batalla;
using System;

namespace KarTac
{
	public interface IInteractor
	{
		Unidad Unidad { get; }

		void Ejecutar ();

		event Action AlTerminar;
	}
}