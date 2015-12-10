using System.Collections.Generic;
using KarTac.Personajes;
using System;

namespace KarTac
{
	/// <summary>
	/// Clase principal de el estado de un juego, en forma global.
	/// </summary>
	public class Clan
	{
		/// <summary>
		/// Personajes
		/// </summary>
		public List<Personaje> Personajes;

		/// <summary>
		/// Fondos del clan
		/// </summary>
		public int Dinero { get; set; }

		public void Guardar ()
		{
			throw new NotImplementedException ();
		}

		public void Cargar ()
		{
			throw new NotImplementedException ();
		}
	}
}