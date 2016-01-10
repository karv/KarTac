using System.Collections.Generic;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	/// <summary>
	/// Representa el conjunto de objetos equipados de una unidad
	/// </summary>
	public class ConjuntoEquipamento : List<IEquipamento>
	{
		public ConjuntoEquipamento (Personaje portador)
		{
			Portador = portador;
		}

		public Personaje Portador { get; }
	}
}