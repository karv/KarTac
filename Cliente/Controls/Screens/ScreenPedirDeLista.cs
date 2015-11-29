using System;
using System.Collections.Generic;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla de pedir desde lista
	/// </summary>
	public class ScreenPedirDeLista<TObj> : ScreenDial
	{
		KarTacGame Juego { get; }

		public ScreenPedirDeLista (IScreen anterior, KarTacGame game)
			: base (anterior)
		{
			Juego = game;
			Lista = new List<TObj> ();
		}

		public List<TObj> Lista { get; }

		public Func<TObj, string> Stringificación { get; set; }

		public void Ejecutar ()
		{
			Juego.CurrentScreen = this;
		}

		public void Salir ()
		{
			Juego.CurrentScreen = ScreenBase;
		}

		public override ListaControl Controles { get; }

		public override void Inicializar ()
		{
		}
	}
}

