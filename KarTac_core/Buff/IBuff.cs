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
	}
}