using KarTac.Batalla.Exp;
using KarTac.Batalla;

namespace KarTac.Buff
{
	public interface IBuff : IObjetivo, ICampoTick
	{
		string Nombre { get; }

		/// <summary>
		/// Portador del buff
		/// </summary>
		/// <value>The portador.</value>
		IObjetivo Portador { get; }

		void Insertar ();

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