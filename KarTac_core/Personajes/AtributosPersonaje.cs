using KarTac.Recursos;
using KarTac.IO;
using System.Collections.Generic;

namespace KarTac.Personajes
{
	public class AtributosPersonaje : IGuardable
	{
		public Empuje Empuje
		{
			get
			{
				return new Empuje (
					this ["empuje_enemigo"],
					this ["empuje_aliado"],
					this ["empuje_masa"]);
			}
			set
			{
				this ["empuje_enemigo"] = value.HaciaEnemigo;
				this ["empuje_aliado"] = value.HaciaAliado;
				this ["empuje_masa"] = value.Masa;
			}
		}

		#region Atributos

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

		public Condición Condición
		{
			get
			{
				var ret = Recs ["Condición"] as Condición;
				return ret;
			}
		}

		#endregion

		/// <summary>
		/// Lista de recursos
		/// </summary>
		/// <value>The recursos.</value>
		Dictionary<string, IRecurso> Recs { get; }

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

		public readonly HashSet<IModificador> Mods = new HashSet<IModificador> ();

		/// <summary>
		/// Devuelve el valor (ya modificados) de un atributo.
		/// No usar set con += o -= etc.
		/// </summary>
		/// <param name="key">Key.</param>
		public float this [string key]
		{
			get
			{
				float ret = Recs [key].Valor;
				foreach (var x in Mods)
				{
					if (x.Atributo == key)
						ret += x.Delta;
				}
				return ret;
			}
			private set
			{
				var rec = GetRecursoBase (key);
				rec.Valor = value;
			}
		}

		public bool TieneAtributo (string nombre)
		{
			return Recs.ContainsKey (nombre);
		}

		public IEnumerable<IRecurso> Enumerar
		{
			get
			{
				return Recs.Values;
			}
		}

		/// <summary>
		/// Devuelve el recurso base de un nombre dado
		/// </summary>
		public IRecurso GetRecursoBase (string nombre)
		{
			IRecurso ret;
			if (!Recs.TryGetValue (nombre, out ret))
			{
				ret = new AtributoGenérico (nombre, false);
			}

			return ret;
		}

		/// <summary>
		/// agrega un recurso o atributo.
		/// </summary>
		public void Add (IRecurso recurso)
		{
			if (!Recs.ContainsValue (recurso))
			{
				Recs.Add (recurso.Nombre, recurso);
				recurso.ConjAtrib = this;
			}
		}

		/// <summary>
		/// Agrega y devuelve un atributo genérico con un nombre dado.
		/// </summary>
		public AtributoGenérico Add (string nombre)
		{
			var ret = new AtributoGenérico (nombre, true);
			Add (ret);
			return ret;
		}

		#region IGuardable

		public void Guardar (System.IO.BinaryWriter writer)
		{
			IOComún.Guardar (new List<IRecurso> (Enumerar), writer);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				Add (Lector.Cargar (reader));
			}
		}

		#endregion
	}
}