namespace KarTac.Batalla
{
	public class Unidad
	{
		public Personaje PersonajeBase { get; }

		public Unidad (Personaje personaje)
		{
			PersonajeBase = personaje;
		}

		public Equipo Equipo { get; set; }
	}
}