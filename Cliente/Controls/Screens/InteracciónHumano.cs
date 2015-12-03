using KarTac.Batalla;
using Microsoft.Xna.Framework.Input;

namespace KarTac.Cliente.Controls.Screens
{
	public class InteracciónHumano : ScreenDial, IInteractor
	{
		BottomMenu menú { get; }

		public Unidad UnidadActual { get; }

		public Campo CampoBatalla { get; }

		public override ListaControl Controles { get; }

		public InteracciónHumano (IScreen ant, Unidad unid, KarTacGame game)
			: base (ant, game)
		{
			menú = new BottomMenu (this);
			menú.Include ();
			UnidadActual = unid;
			CampoBatalla = unid.CampoBatalla;
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
		}
	}
}