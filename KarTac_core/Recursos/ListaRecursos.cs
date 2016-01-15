using KarTac.Recursos;
using System.Collections.Generic;
using KarTac.IO;
using System;

namespace KarTac.Recursos
{
	public class ListaRecursos : Dictionary<string, IRecurso>, IGuardable
	{
		public new IRecurso this [string nombre]
		{
			get
			{
				IRecurso ret;
				if (!TryGetValue (nombre, out ret))
				{
					ret = new AtributoGenérico (nombre, false);
				}

				return ret;
			}
			set
			{
				if (nombre != value.Nombre)
					throw new Exception ();
				if (!ContainsKey (nombre))
					Add (value);
				else
					base [nombre] = value;
			}
		}

		public void Add (IRecurso rec)
		{
			if (!ContainsKey (rec.Nombre))
				Add (rec.Nombre, rec);
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			IOComún.Guardar (Values, writer);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				Add (Lector.Cargar (reader));
			}
		}
	}
}