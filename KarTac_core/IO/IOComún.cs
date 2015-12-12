using System.Collections.Generic;
using System.IO;

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
	}
}