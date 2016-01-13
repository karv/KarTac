using KarTac.Equipamento;
using KarTac.Personajes;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla para equiparse
	/// </summary>
	public class ScreenEquip : Screen
	{
		/// <summary>
		/// Devuelve el clan al que pertenece todo esto
		/// </summary>
		public Clan ClanActual { get; }

		/// <summary>
		/// Devuelve el personaje seleccionado
		/// </summary>
		public Personaje PersonajeSeleccionado { get; private set; }

		Lista<IEquipamento> InvEquips { get; }

		Lista<IEquipamento> Equiped { get; }

		public ScreenEquip (KarTacGame game, Clan clan)
			: base (game)
		{
			ClanActual = clan;

			InvEquips = new Lista<IEquipamento> (this);
			Equiped = new Lista<IEquipamento> (this);

			// TODO cálculo independiente de resolución
			InvEquips.Bounds = new Rectangle (300, 0, 300, 500);
			InvEquips.Stringificación = x => x.ToString ();
			Equiped.Bounds = new Rectangle (0, 0, 290, 500);
			Equiped.Stringificación = x => x.ToString ();

			buildEquips ();
		}

		void buildEquips ()
		{
			foreach (var inv in ClanActual.Inventario)
			{
				var eq = inv as IEquipamento;
				if (eq != null)
					InvEquips.Add (eq);
			}
		}

		void buildEquiped (Personaje pj)
		{
			foreach (var eq in pj.Equipamento)
				InvEquips.Add (eq);
		}

		void buildEquiped ()
		{
			buildEquiped (PersonajeSeleccionado);
		}
	}
}