using System.Collections.Generic;
using KarTac.Personajes;
using KarTac.Skills;

namespace KarTac.Equipamento
{
	/// <summary>
	/// Representa el conjunto de objetos equipados de una unidad
	/// </summary>
	public class ConjuntoEquipamento : List<IEquipamento>
	{
		public ConjuntoEquipamento (Personaje portador)
		{
			Portador = portador;
		}

		public Personaje Portador { get; }

		public ICollection<ISkill> GetSkills ()
		{
			var ret = new HashSet<ISkill> ();
			foreach (var x in this)
			{
				foreach (var y in x.Skills)
				{
					ret.Add (y);
				}
			}
			return ret;
		}

		public void Equiparse (IEquipamento eq, InventarioClan salida)
		{
			var rem = new List<IEquipamento> (eq.AutoRemove (this));
			foreach (var x in rem)
			{
				x.Desequipar ();
				salida.Add (x);
			}
			eq.EquiparEn (this);
			salida.Remove (eq);
		}
	}
}