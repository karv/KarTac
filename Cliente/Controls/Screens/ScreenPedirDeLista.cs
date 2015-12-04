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
			SelecciónActual = new List<TObj> ();
		}

		List<TObj> Lista { get; }

		public TObj ObjetoEnCursor
		{
			get
			{
				return Lista [listaComponente.CursorIndex];
			}
		}

		public void Add (TObj t)
		{
			Lista.Add (t);
			listaComponente.Objetos.Add (Stringificación (t));
		}

		Lista listaComponente { get; }

		bool PuedeEntrar;

		/// <summary>
		/// Devuelve la lista de los objetos seleccionados hasta el momento
		/// </summary>
		public List<TObj> SelecciónActual { get; }

		public Func<TObj, string> Stringificación { get; set; }

		public override ListaControl Controles { get; }

		public override void Inicializar ()
		{
		}

		public override void Update (GameTime gameTime)
		{
			if (InputManager.FuePresionado (Key.Escape))
				Salir ();

			if (InputManager.FuePresionado (Key.Space))
			{
				var curObj = ObjetoEnCursor;
				if (SelecciónActual.Contains (curObj))
					SelecciónActual.Remove (curObj);
				else
					SelecciónActual.Add (curObj);
			}

			if (!PuedeEntrar && InputManager.FuePresionado (Key.Enter))
				PuedeEntrar = true;
			base.Update (gameTime);
		}
	}
}