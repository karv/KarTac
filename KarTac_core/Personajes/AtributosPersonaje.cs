﻿using KarTac.Recursos;
using KarTac.IO;

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
		public AtributoGenérico Ataque
		{
			get
			{
				return Recs ["Ataque"] as AtributoGenérico;
			}
		}

		public AtributoGenérico Defensa
		{
			get
			{
				return Recs ["Defensa"] as AtributoGenérico;
			}
		}

		public AtributoGenérico Velocidad
		{
			get
			{
				return Recs ["Velocidad"] as AtributoGenérico;
			}
		}

		public AtributoGenérico Agilidad
		{
			get
			{
				return Recs ["Agilidad"] as AtributoGenérico;
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
			foreach (var x in Recs.Values)
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