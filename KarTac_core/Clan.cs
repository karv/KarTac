using System.Collections.Generic;
using KarTac.Personajes;
using System;
using KarTac.IO;

namespace KarTac
{
	/// <summary>
	/// Clase principal de el estado de un juego, en forma global.
	/// </summary>
	public class Clan : IGuardable
	{
		/// <summary>
		/// Personajes
		/// </summary>
		public List<Personaje> Personajes { get; private set; }

		/// <summary>
		/// Fondos del clan
		/// </summary>
		public int Dinero { get; set; }

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

		#region Guardable

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (Dinero);
			(Personajes as ICollection<IGuardable>).Guardar (writer);
		}

		public TObj Cargar<TObj> (System.IO.BinaryReader reader)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}