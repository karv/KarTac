using System;
using KarTac.Personajes;
using System.Collections.Generic;

namespace KarTac.Equipamento
{
	public abstract class Equipamento : IEquipamento
	{
		public event Action<Personaje> AlEquipar;

		public event Action<Personaje> AlDesequipar;

		public virtual void EquiparEn (Personaje personaje)
		{
			var anterior = Portador;
			Desequipar ();
			portador = personaje;
			portador.Equipamento.Add (this);
			AlEquipar.Invoke (anterior);
		}

		public void Desequipar ()
		{
			var anterior = Portador;
			portador?.Equipamento.Remove (this);
			AlDesequipar?.Invoke (anterior); //TODO: ¿Debe invocarse cuando no tenía dueño?
		}

		public abstract string Nombre { get; }

		Personaje portador;

		public Personaje Portador
		{
			get
			{
				return portador;
			}
			set
			{
				EquiparEn (value);
			}
		}

		/// <summary>
		/// Devuelve los equipamentos de la misma unidad.
		/// </summary>
		/// <value>The equip set.</value>
		public ICollection<IEquipamento> EquipSet
		{
			get
			{
				return Portador?.Equipamento;
			}
		}

		public abstract IEnumerable<string> Tags { get; }

		ISet<string> IEquipamento.Tags
		{
			get
			{
				return new HashSet<string> (Tags);
			}
		}

		public abstract string IconContentString { get; }
	}
}