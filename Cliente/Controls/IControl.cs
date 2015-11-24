using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls
{
	public interface IControl
	{
		KarTacGame Game { get; }

		void Dibujar (GameTime gameTime);

		void LoadContent ();

		void Update (GameTime gameTime);

		void Include ();

		void Exclude ();

		int Prioridad { get; }
	}
}