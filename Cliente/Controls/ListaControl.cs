using System.Collections.Generic;

namespace KarTac.Cliente.Controls
{
	public class ListaControl : SortedSet<IControl>
	{
		class Comparador :Comparer<IControl>
		{
			public override int Compare (IControl x, IControl y)
			{
				if (x.Equals (y))
					return 0;
				return x.Prioridad < y.Prioridad ? -1 : 1;
			}
		}

		public ListaControl ()
			: base (new Comparador ())
		{
		}

		public new bool Add (IControl control)
		{
			foreach (var x in this)
			{
				if (ReferenceEquals (x, control))
				{
					System.Console.WriteLine ();
				}
			}
			var ad = base.Add (control);
			return ad;
		}
	}
}

