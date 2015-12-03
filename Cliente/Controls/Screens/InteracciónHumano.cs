using KarTac.Batalla;
using Microsoft.Xna.Framework.Input;

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

		public override ListaControl Controles { get; }

		public InteracciónHumano (Unidad unid, KarTacGame game)
			: base (game.CurrentScreen, game)
		{
			Controles = new ListaControl ();
			menú = new BottomMenu (this);
			menú.UnidadActual = unid;
			menú.Include ();


		}

		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update (gameTime);

			var keyb = Keyboard.GetState ();
			if (keyb.IsKeyDown (Keys.Down))
			{
				menú.ÍndiceSkillSel++;
			}
			if (keyb.IsKeyDown (Keys.Up))
			{
				menú.ÍndiceSkillSel--;
			}
			if (keyb.IsKeyDown (Keys.Enter))
			{
				menú.SkillSeleccionado.Ejecutar (UnidadActual, CampoBatalla);
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