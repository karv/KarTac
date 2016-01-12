using System.Collections.Generic;
using KarTac.Personajes;
using KarTac.IO;
using KarTac.Recursos;
using System.IO;
using KarTac.Equipamento;
using KarTac.Skills;

namespace KarTac
{
	/// <summary>
	/// Clase principal de el estado de un juego, en forma global.
	/// </summary>
	public class Clan : IGuardable
	{
		public Clan ()
		{
			Inventario = new InventarioClan ();
		}

		/// <summary>
		/// Personajes
		/// </summary>
		public List<Personaje> Personajes { get; private set; }

		/// <summary>
		/// Fondos del clan
		/// </summary>
		public int Dinero
		{
			get
			{
				return Inventario.Dinero;
			}
			set
			{
				Inventario.Dinero = value;
			}
		}

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

				pj.Atributos.Recs.Add (new AtributoGenérico ("Ataque", true));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Defensa", true));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Velocidad", true));
				pj.Atributos.Recs.Add (new AtributoGenérico ("Agilidad", true));
				pj.Atributos.Recs.Add (new Condición ());

				pj.Atributos.HP.Max = 100;
				pj.Atributos.HP.Valor = 100;
				pj.Atributos.HP.Regeneración = 60;
				pj.Atributos.Ataque.Inicial = 10;
				pj.Atributos.Agilidad.Inicial = 10;
				pj.Atributos.Defensa.Inicial = 10;
				pj.Atributos.Velocidad.Inicial = 50;
				pj.Atributos.Condición.Max = 120;

				pj.Nombre = "Persona " + i;

				pj.InnerSkill.Add (new Golpe (pj));
				pj.InnerSkill.Add (new LanzaRoca (pj));

				var eq = new EqEspada ();
				eq.EquiparEn (pj);

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

		public InventarioClan Inventario { get; }

		#region Guardable

		public void Guardar (BinaryWriter writer)
		{
			writer.Write (Dinero);
			IOComún.Guardar (Inventario, writer);
			IOComún.Guardar (Personajes, writer);
		}

		public void Cargar (BinaryReader reader)
		{
			Dinero = reader.ReadInt32 ();
			var count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				IItem item = KarTac.Equipamento.Lector.Cargar (reader);
				/*
				var tipo = reader.ReadString ();
				switch (tipo)
				{
					default:
						// TODO: cargar por clase
						item = null;
						break;
				}
				*/
				Inventario.Add (item);
			}
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