using Microsoft.Xna.Framework;


namespace KarTac.Batalla
{
	public class Unidad : IObjetivo, IExp
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

		/// <summary>
		/// Mete la experiencia en su bolsa
		/// </summary>
		public void RecibirExp (float exp)
		{
			if (PuedeRecibirExp)
			{
				BolsaExp += exp;
			}
		}

		/// <summary>
		/// Convierte la bolsa de exp en experiencia real para sus IExp
		/// </summary>
		void commitExp ()
		{
			//TODO
		}

		public float BolsaExp { get; private set; }

	}
}