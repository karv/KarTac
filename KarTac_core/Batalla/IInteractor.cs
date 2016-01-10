using KarTac.Batalla;
using System;

namespace KarTac.Batalla
{
	public interface IInteractor
	{
		Unidad Unidad { get; }

		ISelectorTarget Selector { get; }

		void Ejecutar ();

		event Action AlTerminar;
	}
}