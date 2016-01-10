using System.Collections.Generic;
using KarTac.Personajes;
using KarTac.IO;
using KarTac.Recursos;
using System.IO;
using KarTac.Skills;

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
			ret.Dinero = 100;
			ret.Personajes = new List<Personaje> (personajesIniciales);
			for (int i = 0; i < personajesIniciales; i++)
			{
				var pj = new Personaje ();
				pj.Atributos.Recs.Add (new HP ());

				pj.Atributos.Recs.Add (new AtributoGenérico ("Ataque"));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Defensa"));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Velocidad"));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Agilidad"));

				pj.Atributos.HP.Max = 100;
				pj.Atributos.HP.Valor = 100;
				pj.Atributos.HP.Regeneración = 60;
				pj.Atributos.Ataque.Inicial = 10;
				pj.Atributos.Agilidad.Inicial = 10;
				pj.Atributos.Defensa.Inicial = 10;
				pj.Atributos.Velocidad.Inicial = 50;

				pj.Nombre = "Persona " + i;
				pj.Atributos.Recs.Add (new Maná ());
				pj.Skills.Add (new RayoManá (pj));

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

		public void Guardar (BinaryWriter writer)
		{
			writer.Write (Dinero);

			IOComún.Guardar (Personajes, writer);
		}

		public void Cargar (BinaryReader reader)
		{
			Dinero = reader.ReadInt32 ();
			Personajes = new List<Personaje> ();
			IOComún.Cargar (Personajes, () => new Personaje (), reader);
		}

		public void Guardar (string archivo)
		{
			var rd = new BinaryWriter (File.Open (archivo, FileMode.Create));
			Guardar (rd);
			rd.Flush ();
			rd.Close ();
		}

		public void Cargar (string archivo)
		{
			var rd = new BinaryReader (File.Open (archivo, FileMode.Open));
			Cargar (rd);
			rd.Close ();
		}

		#endregion
	}
}