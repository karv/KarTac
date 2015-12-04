using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla de pedir desde lista
	/// </summary>
	public class ScreenPedirDeLista<TObj> : ScreenDial
	{
		const string fontTexture = "UnitNameFont";

		public ScreenPedirDeLista (IScreen anterior, KarTacGame game)
			: base (anterior, game)
		{
			Controles = new ListaControl ();
			Lista = new List<TObj> ();
			listaComponente = new Lista (this);
			listaComponente.Bounds = new Rectangle (0, 0, GetDisplayMode.Width, GetDisplayMode.Height);
			listaComponente.ColorBG = Color.Blue * 0.4f;
			Controles.Add (listaComponente);
			Stringificación = x => x.ToString ();
		}

		List<TObj> Lista { get; }

		public void Add (TObj t)
		{
			listaComponente.Objetos.Add (Stringificación (t));
		}

		Lista listaComponente { get; }

		public Func<TObj, string> Stringificación { get; set; }

		public override ListaControl Controles { get; }

		public override void Inicializar ()
		{
		}

		public override void Update (GameTime gameTime)
		{
			if (InputManager.FuePresionado (Key.Escape))
				Salir ();

			base.Update (gameTime);
		}
	}
}