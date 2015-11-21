using System;

namespace KarTac.Batalla
{
	public class Unidad
	{
		public Personaje PersonajeBase { get; }

		public Unidad (Personaje personaje)
		{
			PersonajeBase = personaje;
		}
	}
}