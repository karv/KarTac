using System.Collections.Generic;
using System;
using KarTac.Personajes;

namespace KarTac.Equipos
{
	public interface IEquipamento
	{
		Personaje Portador { get; set; }

		IEnumerable<string> Tags { get; }

		event Action AlEquipar;
		event Action ElDesequipar;
	}
}