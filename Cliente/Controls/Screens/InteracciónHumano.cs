using KarTac.Batalla;
using KarTac.Batalla.Orden;
using OpenTK.Input;

namespace KarTac.Cliente.Controls.Screens
{
	public class InteracciónHumano : ScreenDial, IInteractor
	{
		BottomMenu menú { get; }

		public Unidad UnidadActual
		{
			get
			{
				return menú.UnidadActual;
			}
		}

		public Campo CampoBatalla
		{
			get
			{
				return UnidadActual.CampoBatalla;
			}
		}

		//public override ListaControl Controles { get; }

		public InteracciónHumano (Unidad unid, KarTacGame game)
			: base (game)
		{
			menú = new BottomMenu (this);
			menú.UnidadActual = unid;
			menú.Include ();
		}

		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update (gameTime);

			if (InputManager.FuePresionado (Key.Down))
			{
				menú.ÍndiceSkillSel++;
			}
			if (InputManager.FuePresionado (Key.Up))
			{
				menú.ÍndiceSkillSel--;
			}
			if (InputManager.FuePresionado (Key.Enter))
			{
				menú.SkillSeleccionado.Ejecutar (UnidadActual, CampoBatalla);
			}

			if (InputManager.FuePresionado (MouseButton.Left))
			{
				var ord = new Movimiento (UnidadActual);
				ord.Destino = new Microsoft.Xna.Framework.Point (InputManager.EstadoActualMouse.X,
				                                                 InputManager.EstadoActualMouse.Y);
				UnidadActual.OrdenActual = ord;
				Salir (); // Devuelve el control a la pantalla anterior
			}
		
		}

		public override void Inicializar ()
		{
			menú.Inicializar ();
		}

		#region IInteractor

		Unidad IInteractor.Unidad
		{
			get
			{
				return UnidadActual;
			}
		}

		#endregion
	}
}