using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls.Screens
{
	public interface IScreen
	{
		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		void Dibujar (GameTime gameTime);

		/// <summary>
		/// Cargar contenido
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime);

		/// <summary>
		/// Descargar contenido
		/// </summary>
		void UnloadContent ();

		/// <summary>
		/// La lista de controles de esta Screen
		/// </summary>
		ListaControl Controles { get; }

		/// <summary>
		/// El manejador de contenido
		/// </summary>
		/// <value>The content.</value>
		ContentManager Content { get; }

		/// <summary>
		/// El estado del ratón del último ciclo
		/// </summary>
		MouseState LastMouseState { get; }

		/// <summary>
		/// El estado del teclado del último ciclo.
		/// </summary>
		KeyboardState LastKeyboardState { get; }

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		SpriteBatch Batch { get; }

		DisplayMode GetDisplayMode { get; }
	}
}