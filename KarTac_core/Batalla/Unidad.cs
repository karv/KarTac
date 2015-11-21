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

		/// <summary>
		/// Devuelve si esta unidad puede recibir experiencia
		/// </summary>
		public bool PuedeRecibirExp
		{
			get
			{
				return EstáVivo;
			}
		}

		/// <summary>
		/// Devuelve si esta unidad está viva
		/// </summary>
		public bool EstáVivo
		{
			get
			{
				return PersonajeBase.Atributos.HP.Valor > 0;
			}
		}

		public Equipo Equipo { get; set; }
	}
}