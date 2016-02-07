using System;

namespace KarTac.Batalla
{
	/// <summary>
	/// Ofrece métodos para que un campo piueda actualizarlo durante el Tick
	/// </summary>
	public interface ICampoTick
	{
		void Tick (TimeSpan time);
	}
}