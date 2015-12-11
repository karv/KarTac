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
		public List<Personaje> Personajes { get; private set; }

		/// <summary>
		/// Fondos del clan
		/// </summary>
		public int Dinero { get; set; }

		#region IO

		public void Guardar ()
		{
			throw new NotImplementedException ();
		}

		public void Cargar ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		/// <summary>
		/// Devuelve un clan de estado inicial.
		/// </summary>
		public static Clan BuildStartingClan ()
		{
			var ret = new Clan ();
			const int personajesIniciales = 3;
			ret.Personajes = new List<Personaje> (personajesIniciales);
			for (int i = 0; i < personajesIniciales; i++)
			{
				var pj = new Personaje ();

				pj.Atributos.HP.Max = 100;
				pj.Atributos.HP.Valor = 100;
				pj.Atributos.HP.Regeneración = 60;
				pj.Atributos.Velocidad = 100;
				pj.Atributos.Agilidad = 30;
				pj.Nombre = "Persona " + i;

				ret.Personajes.Add (pj);
			}

			return ret;
		}

		public void Reestablecer ()
		{
			foreach (var u in Personajes)
			{
				u.Atributos.Inicializar ();
			}
		}
	}
}