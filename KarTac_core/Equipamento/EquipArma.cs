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
			foreach (var x in new List<IEquipamento> (ConjEquipment))
			{
				if (x.Tags.Contains ("arma"))
					x.Desequipar ();
			}
			base.EquiparEn (personaje);
		}
	}
}