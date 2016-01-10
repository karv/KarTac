using System;
using KarTac.Personajes;
using System.Collections.Generic;
using KarTac.Skills;
using System.IO;

namespace KarTac.Equipamento
{
	public static class Lector
	{
		public static IEquipamento Cargar (BinaryReader reader)
		{
			IEquipamento ret;
			var recNombre = reader.ReadString ();
			switch (recNombre)
			{
				case "Espada":
					ret = new EqEspada ();
					break;
				default:
					throw new Exception ("No existe equipment " + recNombre);
			}
			ret.Cargar (reader);
			return ret;
		}
	}

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
			OnEquipar (anterior);
		}

		public void Desequipar ()
		{
			var anterior = ConjEquipment;
			conjEquipment?.Remove (this);
			OnDesequipar (anterior);
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

		public abstract string Nombre { get; }

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

		public virtual void Cargar (BinaryReader reader)
		{
		}

		IEnumerable<ISkill> IEquipamento.Skills
		{
			get
			{
				return Skills;
			}
		}

		/// <summary>
		/// Se ejecuta al equipar un arma
		/// </summary>
		protected virtual void OnEquipar (ConjuntoEquipamento anterior)
		{
			AlEquipar?.Invoke (anterior);
		}

		/// <summary>
		/// Se ejecuta al desequipar un arma
		/// </summary>
		protected virtual void OnDesequipar (ConjuntoEquipamento anterior)
		{
			AlDesequipar?.Invoke (anterior);
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