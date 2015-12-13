using System.Collections.Generic;
using System.IO;
using System;

namespace KarTac.IO
{
	public static class IOComún
	{
		public static void Guardar<TObjeto> (ICollection<TObjeto> list,
		                                     BinaryWriter writer)
			where TObjeto : IGuardable
		{
			writer.Write (list.Count);
			foreach (var x in list)
			{
				x.Guardar (writer);
			}
		}

		public static void Cargar<TObj> (ICollection<TObj> coll,
		                                 Func<TObj> ctor,
		                                 BinaryReader reader)
			where TObj : IGuardable
		{
			coll.Clear ();
			var count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				var ob = ctor.Invoke ();
				ob.Cargar (reader);
				coll.Add (ob);
			}
		}
	}
}