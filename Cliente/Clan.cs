using System.Collections.Generic;
using System.IO;
using KarTac.IO;
using KarTac.Personajes;
using KarTac.Equipamento;
using KarTac.Recursos;
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
		public static Clan BuildStartingClan (int cant = 3)
		{
			var ret = new Clan ();
			const int personajesIniciales = 3;
			ret.Dinero = 100;
			ret.Personajes = new List<Personaje> (cant);
			for (int i = 0; i < cant; i++)
			{
				var pj = new Personaje ();
				pj.Atributos.Add (HP.BuildMulti ());

				pj.Atributos.Add (new AtributoGenérico ("Ataque", true));
				pj.Atributos.Add (new AtributoGenérico ("Defensa", true));
				pj.Atributos.Add (new AtributoGenérico ("Velocidad", true));
				pj.Atributos.Add (new AtributoGenérico ("Agilidad", true));
				pj.Atributos.Add (new Condición ());

				pj.Atributos.HP.Max = 100;
				pj.Atributos.HP.Valor = 100;
				pj.Atributos.HP.Regeneración = 60;
				pj.Atributos.Ataque.Inicial = 10;
				pj.Atributos.Ataque.CommitExpCoef = 0.3f;
				pj.Atributos.Agilidad.Inicial = 10;
				pj.Atributos.Agilidad.CommitExpCoef = 0.2f;
				pj.Atributos.Defensa.Inicial = 10;
				pj.Atributos.Defensa.CommitExpCoef = 0.35f;
				pj.Atributos.Velocidad.Inicial = 50;
				pj.Atributos.Velocidad.CommitExpCoef = 0.1f;
				pj.Atributos.Condición.Max = 120;

				pj.Nombre = "Persona " + i;

				pj.InnerSkill.Add (new Golpe (pj));

				ret.Personajes.Add (pj);
			}

			var inv = ret.Inventario;

			inv.Add (new Arco ());
			inv.Add (new Hacha ());
			inv.Add (new Lanza ());
			inv.Add (new EqEspada ());
			inv.Add (new HpPoción ());

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

		public float TotalExp
		{
			get
			{
				float ret = 0;
				foreach (var u in Personajes)
				{
					ret += u.TotalExp;
				}
				return ret;
			}
		}

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
			/*
			var count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				IItem item = KarTac.Equipamento.Lector.Cargar (reader);

				Inventario.Add (item);
			}
*/
			Personajes = new List<Personaje> ();
			IOComún.Cargar (
				Inventario,
				() => KarTac.Equipamento.Lector.Cargar (reader),
				reader);
			IOComún.Cargar (Personajes, () => Personaje.CargarReader (reader), reader);
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