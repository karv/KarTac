using System;
using System.Collections.Generic;
using KarTac.Skills;
using KarTac.Batalla;
using KarTac.IO;
using System.IO;

namespace KarTac.Personajes
{
	public class Personaje : IGuardable
	{
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
			Skills = new List<ISkill> ();

			// Agregar defaults
			Skills.Add (new Golpe (this));
			Desbloqueables = new HashSet<ISkill> ();

		}

		public IList<ISkill> Skills { get; }

		public HashSet<ISkill> Desbloqueables { get; }

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
			IOComún.Guardar (Skills, writer);
			IOComún.Guardar (Desbloqueables, writer);
		}

		public void Cargar (BinaryReader reader)
		{
			Nombre = reader.ReadString ();
			Atributos.Cargar (reader);

			Skills.Clear ();
			Desbloqueables.Clear ();
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				Skills.Add (SkillComún.Cargar (reader, this));
			}
			count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				Desbloqueables.Add (SkillComún.Cargar (reader, this));
			}
		}

		#endregion
	}
}