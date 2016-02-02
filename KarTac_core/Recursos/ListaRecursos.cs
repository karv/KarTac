using KarTac.Recursos;
using System.Collections.Generic;
using KarTac.IO;
using System;

namespace KarTac.Recursos
{
	public class ListaRecursos : Dictionary<string, IRecurso>, IGuardable
	{

		public void Add (IRecurso rec)
		{
			if (!ContainsKey (rec.Nombre))
				Add (rec.Nombre, rec);
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
		}
	}
}