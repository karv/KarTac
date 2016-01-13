using System;
using KarTac.Equipamento;
using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace KarTac.Cliente.Controls.Screens
{
	public class TiendaScreen : ScreenDial
	{
		public TiendaScreen (KarTacGame juego, Tienda tienda, InventarioClan inv)
			: base (juego)
		{
			Compras = new Compras (tienda, inv);
		}

		public override void Inicializar ()
		{
			Mostrables = new Lista<Compras.EntradaUnificada> (this);
			Mostrables.Bounds = new Rectangle (20, 20, 400, 200);
			Mostrables.Stringificación = x => string.Format ("{0:000}x {1}", x.Marcadas, x.Nombre);

			updateCompras ();

			Mostrables.Include ();

			base.Inicializar ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (Key.Escape))
				Salir ();
			if (InputManager.FuePresionado (Key.Enter))
				Compras.Commit ();
		}

		void updateCompras ()
		{
			Mostrables.Clear ();
			foreach (var x in Compras.MisCompras)
			{
				
			}
		}


		public Tienda Tienda
		{
			get
			{
				return Compras.Tienda;
			}
		}

		public Compras Compras { get; }

		Lista<Compras.EntradaUnificada> Mostrables;

		public override bool DibujarBase
		{
			get
			{
				return false;
			}
		}
	}
}