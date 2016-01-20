using System;
using KarTac.Cliente.Controls.Screens;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Representa un objeto que mantiene el control sobre la cercanía y presión del cursor del ratón a los extremos de la pantalla
	/// </summary>
	public class SensorialExtremos : IControl
	{
		public SensorialExtremos (IScreen screen)
		{
			Screen = screen;
		}

		Rectangle screenRectangle;

		public int CercaníaXMin = 10;
		public int CercaníaXMax = 10;
		public int CercaníaYMin = 10;
		public int CercaníaYMax = 10;

		public IScreen Screen { get; }

		public void Include ()
		{
			Screen.Controles.Add (this);
		}

		public void Exclude ()
		{
			Screen.Controles.Remove (this);
		}

		void IControl.Dibujar (GameTime gameTime)
		{
		}

		void IControl.Update (GameTime gameTime)
		{
			Vector2 v = Vector2.Zero;
			if (InputManager.EstadoActualMouse.X < CercaníaXMin)
				v += new Vector2 (-1, 0);
			else
			if (InputManager.EstadoActualMouse.X > screenRectangle.Right - CercaníaXMax)
				v += new Vector2 (1, 0);

			if (InputManager.EstadoActualMouse.Y < CercaníaYMin)
				v += new Vector2 (0, -1);
			else
			if (InputManager.EstadoActualMouse.Y > screenRectangle.Bottom - CercaníaYMax)
				v += new Vector2 (0, 1);

			if (v != Vector2.Zero)
				AlHacerPresión?.Invoke (v * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
		}

		public void LoadContent ()
		{
		}

		public void Inicializar ()
		{
			screenRectangle = new Rectangle (0, 0, Screen.GetDisplayMode.Width, Screen.GetDisplayMode.Height);
		}

		public void Dispose ()
		{
		}

		int IControl.Prioridad
		{
			get
			{
				return 0;
			}
		}

		/// <summary>
		/// Ocurre cuando se hace presión con el ratón sobre la orilla de la pantalla,
		/// devuelve la presión hecha.
		/// </summary>
		public event Action<Vector2> AlHacerPresión;
	}
}