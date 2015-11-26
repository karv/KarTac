using System.Collections.Generic;

namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		public List<Unidad> Unidades { get; }

		public BottomMenu Menú { get; }

		public BattleScreen (KarTacGame juego)
			: base (juego)
		{
			Menú = new BottomMenu (this);
			Menú.Include ();
		}

		/// <summary>
		/// Agrega una unidad
		/// </summary>
		/// <returns>Devuelve el control (recién creado) asociado a la unidad</returns>
		public Unidad AgregaUnidad (KarTac.Batalla.Unidad unit)
		{
			var ret = new Unidad (this, unit);
			ret.Initialize ();
			ret.Include ();
			return ret;
		}
	}
}

