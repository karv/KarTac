using Microsoft.Xna.Framework;


namespace KarTac.Batalla
{
	public class Unidad
	{
		public Point Pos { get; set; }

		public Personaje PersonajeBase { get; }

		public Unidad (Personaje personaje)
		{
			PersonajeBase = personaje;
		}

		public Equipo Equipo { get; set; }
	}
}