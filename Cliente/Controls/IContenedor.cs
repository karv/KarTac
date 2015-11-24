using System.Collections.Generic;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Contenedor de controles
	/// </summary>
	public interface IContenedor : IControl
	{
		ICollection<IControl> Controles { get; }
	}
}

