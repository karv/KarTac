using System;
using System.Collections.Generic;
using KarTac.Skills;
using KarTac.Batalla;
using KarTac.IO;
using System.IO;
using KarTac.Equipamento;
using System.Linq;

namespace KarTac.Personajes
{
	public class Personaje : IGuardable
	{
		internal class SkillComparer : IEqualityComparer<ISkill>
		{
			public bool Equals (ISkill x, ISkill y)
			{
				return x.Nombre == y.Nombre;
			}

			public int GetHashCode (ISkill obj)
			{
				return obj.ToString ().GetHashCode ();
			}
		}

		/// <summary>
		/// Nombre del personaje
		/// </summary>
		/// <value>The nombre.</value>
		public string Nombre { get; set; }

		/// <summary>
		/// Atributos del personaje
		/// </summary>
		/// <value>The atributos.</value>
		public AtributosPersonaje Atributos { get; }

		public Unidad Unidad { get; private set; }

		public Unidad ConstruirUnidad (Campo campoBatalla)
		{
			if (Unidad != null)
				throw new Exception ("Este personaje ya tiene una unidad asignada.");
			var ret = new Unidad (this, campoBatalla);
			Unidad = ret;
			return ret;
		}

		public void LimpiarUnidad ()
		{
			Unidad = null;
		}

		public Personaje ()
		{
			Atributos = new AtributosPersonaje ();
			Atributos.Empuje = new Empuje (300, 100, 30);
			InnerSkill = new List<ISkill> ();
			Equipamento = new ConjuntoEquipamento (this);

			// Agregar defaults
			Desbloqueables = new HashSet<ISkill> (new SkillComparer ());
		}

		public float TotalExp { get; set; }

		/// <summary>
		/// Habilidades inhatas del personaje
		/// </summary>
		/// <value>The inner skill.</value>
		public IList<ISkill> InnerSkill { get; }

		/// <summary>
		/// Todas las habilidades del personaje, incluyendo de armas
		/// </summary>
		/// <value>The skills.</value>
		public IReadOnlyList<ISkill> Skills
		{
			get
			{
				var ret = new List<ISkill> ();
				foreach (var x in Equipamento.GetSkills ())
				{
					if (InnerSkill.All (y => y.Nombre != x.Nombre)) // Evitar duplicados
						ret.Add (x);
				}
				foreach (var x in InnerSkill)
				{
					ret.Add (x);
				}
				return ret;
			}
		}

		public HashSet<ISkill> Desbloqueables { get; }

		public ConjuntoEquipamento Equipamento { get; }

		public event Action AlMorir
		{
			add
			{
				Atributos.HP.AlValorCero += value;
			}
			remove
			{
				Atributos.HP.AlValorCero -= value;
			}
		}

		#region IGuardable

		public void Guardar (BinaryWriter writer)
		{
			writer.Write (Nombre);
			Atributos.Guardar (writer);
			writer.Write (TotalExp);
			IOComún.Guardar (InnerSkill, writer);
			IOComún.Guardar (Desbloqueables, writer);
			IOComún.Guardar (Equipamento, writer);
		}

		public void Cargar (BinaryReader reader)
		{
			Nombre = reader.ReadString ();
			Atributos.Cargar (reader);
			TotalExp = reader.ReadSingle ();
			InnerSkill.Clear ();
			Desbloqueables.Clear ();
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				InnerSkill.Add (SkillComún.Cargar (reader, this));
			}
			count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				Desbloqueables.Add (SkillComún.Cargar (reader, this));
			}
			count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				var eq = Lector.Cargar (reader) as IEquipamento;
				eq.EquiparEn (Equipamento);
				//Equipamento.Add ();
			}
		}

		public static Personaje CargarReader (BinaryReader reader)
		{
			var ret = new Personaje ();
			ret.Cargar (reader);
			return ret;
		}

		#endregion
	}
}