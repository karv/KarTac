using System.Collections.Generic;
using System.Linq;

namespace KarTac.Equipamento
{
	public abstract class EquipArma : Equipamento
	{
		public override IEnumerable<string> Tags
		{
			get
			{
				yield return "arma";
			}
		}

		public override void EquiparEn (KarTac.Personajes.Personaje personaje)
		{
			EquiparEn (personaje.Equipamento);
		}

		public override IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("arma"));
		}
	}
}