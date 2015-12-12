using System.Collections.Generic;
using System.IO;

namespace KarTac.IO
{
	public static class IOComún
	{
		public static void Guardar (this ICollection<IGuardable> list,
		                            BinaryWriter writer)
		{
			writer.Write (list.Count);
			foreach (var x in list)
			{
				x.Guardar (writer);
			}
		}
	}
}