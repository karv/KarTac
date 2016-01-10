using System.Collections.Generic;

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
			if (ConjEquipment != null)
			{
				foreach (var x in new List<IEquipamento> (ConjEquipment))
				{
					if (x.Tags.Contains ("arma"))
						x.Desequipar ();
				}
			}
			base.EquiparEn (personaje);
		}

		public override void EquiparEn (ConjuntoEquipamento conjEquip)
		{
			base.EquiparEn (conjEquip);
		}
	}
}