using System;
using KarTac.Cliente.Controls.Screens;
using KarTac.Batalla;

namespace KarTac.Cliente
{
	/// <summary>
	/// Interactor humano
	/// </summary>
	public class Interactor : IInteractor
	{
		public BattleScreen Screen { get; set; }

		public Unidad Unidad { get; set; }

		public void Ejecutar ()
		{
			var r = new InteracciónHumano (Screen, Unidad, Screen.Game);
			r.Ejecutar ();
		}
	}
}

