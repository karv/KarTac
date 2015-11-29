using System;
using System.Collections.Generic;
using OpenTK.Platform.Windows;
using MonoGame.Extended.BitmapFonts;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla de pedir desde lista
	/// </summary>
	public class ScreenPedirDeLista<TObj> : ScreenDial
	{
		const string fontTexture = "UnitNameFont";

		KarTacGame Juego { get; }

		public ScreenPedirDeLista (IScreen anterior, KarTacGame game)
			: base (anterior)
		{
			Juego = game;
			Lista = new List<TObj> ();
		}

		public List<TObj> Lista { get; }

		BitmapFont Fuente { get; }

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

		public override void LoadContent ()
		{
			base.LoadContent ();
			Fuente = Content.Load<BitmapFont> (fontTexture);
		}
	}
}