using System.Collections.Generic;

namespace KarTac.Cliente.Controls
{
	public class ListaControl : SortedSet<IControl>
	{
		class Comparador :Comparer<IControl>
		{
			public override int Compare (IControl x, IControl y)
			{
				return x.Prioridad < y.Prioridad ? -1 : 1;
			}
		}

		public ListaControl ()
			: base (new Comparador ())
		{
		}
	}
}

