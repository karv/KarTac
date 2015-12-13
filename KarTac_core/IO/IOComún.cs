using System.Collections.Generic;
using System.IO;
using System;

namespace KarTac.IO
{
	public static class IOComún
	{
		/// <summary>
		/// Guarda una collección de IGuardables por un BinaryWriter
		/// </summary>
		/// <param name="coll">Colección a guardar</param>
		/// <param name="writer">Writer.</param>
		/// <typeparam name="TObjeto">Tipo de objetos de la colección</typeparam>
		public static void Guardar<TObjeto> (ICollection<TObjeto> coll,
		                                     BinaryWriter writer)
			where TObjeto : IGuardable
		{
			writer.Write (coll.Count);
			foreach (var x in coll)
			{
				x.Guardar (writer);
			}
		}

		/// <summary>
		/// Carga una colección de IGuardables por un BinaryReader
		/// </summary>
		/// <param name="coll">La colección donde se agregarán los objetos</param>
		/// <param name="ctor">Constructor del tipo de objetos</param>
		/// <param name="reader">Lector binario</param>
		/// <typeparam name="TObj">Tipo de objetos de la lista</typeparam>
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