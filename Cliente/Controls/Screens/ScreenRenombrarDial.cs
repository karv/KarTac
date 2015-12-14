using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Screens
{
	public class ScreenRenombrarDial : ScreenDial
	{
		readonly EntradaTexto input;

		public ScreenRenombrarDial (KarTacGame juego, IScreen screen)
			: base (juego, screen)
		{
			input = new EntradaTexto (screen);
			input.Pos = new Point (100, 100);
		}

		public string Texto { get; private set; }

		public override void Update (GameTime gameTime)
		{
			if (InputManager.FuePresionado (OpenTK.Input.Key.Enter))
			{
				Texto = input.Texto;
				Salir ();
			}
			base.Update (gameTime);
		}
	}
}