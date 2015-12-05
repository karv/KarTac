using OpenTK.Input;

namespace KarTac.Cliente
{
	public static class InputManager
	{
		public static KeyboardState EstadoActualTeclado { get; private set; }

		public static KeyboardState EstadoAnteriorTeclado { get; private set; }

		public static MouseState EstadoActualMouse { get; private set; }

		public static MouseState EstadoAnteriorMouse { get; private set; }

		public static void  Update ()
		{
			EstadoAnteriorTeclado = EstadoActualTeclado;
			EstadoActualTeclado = Keyboard.GetState ();

			EstadoAnteriorMouse = EstadoActualMouse;
			EstadoActualMouse = Mouse.GetState ();
		
		}

		#region Teclado

		/// <summary>
		/// evisa si una tecla está en este momento presionada
		/// </summary>
		public static bool EstáPresionado (Key tecla)
		{
			return EstadoActualTeclado.IsKeyDown (tecla);
		}

		/// <summary>
		/// Resiva si una tecla fue presionada en este instante (módulo Update ())
		/// </summary>
		public static bool FuePresionado (Key tecla)
		{
			return EstáPresionado (tecla) && !EstadoAnteriorTeclado.IsKeyDown (tecla);
		}

		#endregion

		#region Mouse

		public static bool EstáPresionado (MouseButton botón)
		{
			return EstadoActualMouse.IsButtonDown (botón);
		}

		public static bool FuePresionado (MouseButton botón)
		{
			return EstáPresionado (botón) && !EstadoAnteriorMouse.IsButtonDown (botón);
		}

		#endregion
	}
}