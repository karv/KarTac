using System.Collections.Generic;

namespace KarTac.Batalla
{
	/// <summary>
	/// Representa un campo de batalla
	/// </summary>
	public class Campo
	{
		public ISelectorTarget SelectorTarget { get; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

		public Campo ()
		{
			Unidades = new List<Unidad> ();
			ExpDict = new Dictionary<Unidad, KarTac.Batalla.Exp.TotalExp> ();
		}

		public Dictionary<Unidad, Exp.TotalExp> ExpDict { get; }
	}
}