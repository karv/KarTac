using System;
using KarTac.Personajes;
using System.Collections.Generic;
using KarTac.Skills;

namespace KarTac.Equipamento
{
	public abstract class Equipamento : IEquipamento
	{
		public event Action<ConjuntoEquipamento> AlEquipar;

		public event Action<ConjuntoEquipamento> AlDesequipar;

		public virtual void EquiparEn (Personaje personaje)
		{
			EquiparEn (personaje.Equipamento);
		}

		public virtual void EquiparEn (ConjuntoEquipamento conjEquip)
		{
			var anterior = ConjEquipment;
			Desequipar ();
			conjEquipment = conjEquip;
			conjEquipment.Add (this);
			AlEquipar?.Invoke (anterior);
		}

		public void Desequipar ()
		{
			var anterior = ConjEquipment;
			conjEquipment?.Remove (this);
			AlDesequipar?.Invoke (anterior); //TODO: ¿Debe invocarse cuando no tenía dueño?
		}

		public Personaje Portador
		{
			get
			{
				return ConjEquipment.Portador;
			}
		}

		ConjuntoEquipamento IEquipamento.Conjunto
		{
			get
			{
				return ConjEquipment;
			}
		}

		public abstract string Nombre { get; protected set; }

		ConjuntoEquipamento conjEquipment;

		public ConjuntoEquipamento ConjEquipment
		{
			get
			{
				return conjEquipment;
			}
			set
			{
				if (value == null)
					Desequipar ();
				else
					EquiparEn (value);
			}
		}

		/// <summary>
		/// Devuelve los equipamentos de la misma unidad.
		/// </summary>
		/// <value>The equip set.</value>
		[Obsolete ("Es mejor usar ConjEquipment")]
		public ICollection<IEquipamento> EquipSet
		{
			get
			{
				return ConjEquipment;
			}
		}

		public abstract IEnumerable<string> Tags { get; }

		ISet<string> IItem.Tags
		{
			get
			{
				return new HashSet<string> (Tags);
			}
		}

		public abstract string IconContentString { get; }

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (GetType ().Name);
			writer.Write (Nombre);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Nombre = reader.ReadString ();
		}

		IEnumerable<ISkill> IEquipamento.Skills
		{
			get
			{
				return Skills;
			}
		}

		protected virtual IEnumerable<ISkill> Skills
		{
			get
			{
				yield break;
			}
		}
	}
}