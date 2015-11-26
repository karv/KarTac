using Microsoft.Xna.Framework;

namespace KarTac.Buff
{
	public interface IBuff : IObjetivo
	{
		string Nombre { get; }

		/// <summary>
		/// Portador del buff
		/// </summary>
		/// <value>The portador.</value>
		IObjetivo Portador { get; }

		void Update (GameTime gameTime);

		/// <summary>
		/// Se termina, cancelando el efecto y liberándose.
		/// </summary>
		void Terminar ();

		/// <summary>
		/// Si es visible este Buff
		/// </summary>
		bool Visible { get; }

		ITagging ExpTags { get; }
	}
}