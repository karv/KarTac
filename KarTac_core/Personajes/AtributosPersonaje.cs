using KarTac.Recursos;
using KarTac.IO;

namespace KarTac.Personajes
{
	public class AtributosPersonaje : IGuardable
	{
		public int Ataque { get; set; }

		public int Defensa { get; set; }

		/// <summary>
		/// Que tan rápido recorre terreno.
		/// Pixeles por segundo.
		/// </summary>
		public int Velocidad { get; set; }

		/// <summary>
		/// Qué tan rápido y agil es para usar algunas habilidades
		/// </summary>
		public int Agilidad { get; set; }

		public Empuje Empuje { get; set; }

		/// <summary>
		/// Max HP
		/// </summary>
		public HP HP
		{
			get
			{
				return Recs ["HP"] as HP;
			}
		}

		/// <summary>
		/// Lista de recursos
		/// </summary>
		/// <value>The recursos.</value>
		public ListaRecursos Recs { get; }

		public AtributosPersonaje ()
		{
			Recs = new ListaRecursos ();
		}

		/// <summary>
		/// Reestablece los atributos a sus valores default
		/// </summary>
		public void Inicializar ()
		{
			foreach (var x in Recs)
			{
				x.Reestablecer ();
			}
		}

		public AtributosPersonaje Clonar ()
		{
			return MemberwiseClone () as AtributosPersonaje;
		}

		#region IGuardable

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (Ataque);
			writer.Write (Defensa);
			writer.Write (Velocidad);
			writer.Write (Agilidad);
			Empuje.Guardar (writer);
			Recs.Guardar (writer);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Ataque = reader.ReadInt32 ();
			Defensa = reader.ReadInt32 ();
			Velocidad = reader.ReadInt32 ();
			Agilidad = reader.ReadInt32 ();
			Empuje.Cargar (reader);
			Recs.Cargar (reader);
		}

		#endregion
	}
}