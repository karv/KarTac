using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls
{
	public interface IControl
	{
		KarTacGame Game { get; }

		void Dibujar ();

		void LoadContent ();

		void Update ();

		void Include ();

		void Exclude ();

	}
}