using KarTac.Recursos;
using System.Collections.Generic;
using KarTac.IO;

namespace KarTac.Recursos
{
	public class ListaRecursos : List<IRecurso>, IGuardable
	{
		public IRecurso this [string nombre]
		{
			get
			{
				return Find (x => x.Nombre == nombre);
			}
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			IOComún.Guardar (this, writer);
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