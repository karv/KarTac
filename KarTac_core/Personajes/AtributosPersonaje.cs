using KarTac.Recursos;
using KarTac.IO;
using System.Collections.Generic;
using NUnit.Framework;

namespace KarTac.Personajes
{
	public class AtributosPersonaje : IGuardable
	{
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

		// Atributos
		public readonly AtributoGenérico Ataque;
		public readonly AtributoGenérico Defensa;
		public readonly AtributoGenérico Velocidad;
		public readonly AtributoGenérico Agilidad;

		/// <summary>
		/// Lista de recursos
		/// </summary>
		/// <value>The recursos.</value>
		public ListaRecursos Recs { get; }

		public AtributosPersonaje ()
		{
			Recs = new ListaRecursos ();

			Ataque = new AtributoGenérico ("Ataque");
			Defensa = new AtributoGenérico ("Defensa");
			Velocidad = new AtributoGenérico ("Velocidad");
			Agilidad = new AtributoGenérico ("Agilidad");

			Recs.Add (Ataque);
			Recs.Add (Defensa);
			Recs.Add (Velocidad);
			Recs.Add (Agilidad);
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
			Empuje.Guardar (writer);
			Recs.Guardar (writer);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Empuje.Cargar (reader);
			Recs.Cargar (reader);
		}

		#endregion
	}
}