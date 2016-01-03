using System;

namespace KarTac
{
	/// <summary>
	/// Un objeto que genera acción cada cierto tiempo
	/// </summary>
	public interface IAccionable
	{
		void AvanzarTiempo (TimeSpan time);
	}
}