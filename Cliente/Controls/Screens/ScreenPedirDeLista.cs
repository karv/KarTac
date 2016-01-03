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
		public struct TipoSalida
		{
			public enum EnumTipoSalida
			{
				Cancelación,
				Aceptación
			}

			public TipoSalida (EnumTipoSalida tipo, IEnumerable<TObj> selección)
			{
				Tipo = tipo;
				Selección = selección;
			}

			public EnumTipoSalida Tipo;
			public IEnumerable<TObj> Selección;
		}

		public TipoSalida Salida { get; private set; }

		const string fontTexture = "UnitNameFont";

		public ScreenPedirDeLista (KarTacGame game)
			: base (game)
		{
			ListaComponente = new Lista<TObj> (this);
			ListaComponente.Bounds = new Rectangle (0, 0, GetDisplayMode.Width, GetDisplayMode.Height);
			ListaComponente.ColorBG = Color.Blue * 0.4f;
			Controles.Add (ListaComponente);
			ListaComponente.Stringificación = x => x.ToString ();
			SelecciónActual = new List<TObj> ();
		}

		public override void Ejecutar ()
		{
//			if (!(Juego.CurrentScreen is BattleScreen))
//				throw new Exception ("No se puede abrir este control desde fuera de un BattleScreen");
			base.Ejecutar ();
		}

		public TObj ObjetoEnCursor
		{
			get
			{
				return ListaComponente.Objetos [ListaComponente.CursorIndex].Objeto;
			}
		}

		public void Add (TObj t, Color color)
		{			
			ListaComponente.Objetos.Add (new Lista<TObj>.Entrada (t, color));
		}

		public Lista<TObj> ListaComponente { get; }

		/// <summary>
		/// Devuelve la lista de los objetos seleccionados hasta el momento
		/// </summary>
		public List<TObj> SelecciónActual { get; }

		public override void Inicializar ()
		{
		}

		public override void Update (GameTime gameTime)
		{
			if (InputManager.FuePresionado (Key.Escape))
			{
				Salida = new TipoSalida (TipoSalida.EnumTipoSalida.Cancelación, SelecciónActual);
				Salir ();
			}

			if (InputManager.FuePresionado (Key.Space))
			{
				var curObj = ObjetoEnCursor;
				if (SelecciónActual.Contains (curObj))
					SelecciónActual.Remove (curObj);
				else
					SelecciónActual.Add (curObj);
			}

			if (InputManager.FuePresionado (Key.Enter))
			{
				Salida = new TipoSalida (TipoSalida.EnumTipoSalida.Aceptación, SelecciónActual);
				Salir ();
			}
				
			base.Update (gameTime);
		}
	}
}