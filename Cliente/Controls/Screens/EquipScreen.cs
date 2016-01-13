using KarTac.Equipamento;
using KarTac.Personajes;
using Microsoft.Xna.Framework;
using OpenTK.Input;
using System;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla para equiparse
	/// </summary>
	public class EquipScreen : Screen
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

		public IListaControl ListaSeleccionada { get; private set; }

		public EquipScreen (KarTacGame game, Clan clan)
			: base (game)
		{
			ClanActual = clan;
			PersonajeSeleccionado = ClanActual.Personajes [0];

			InvEquips = new Lista<IEquipamento> (this);
			Equiped = new Lista<IEquipamento> (this);
			ListaSeleccionada = InvEquips;

			// TODO cálculo independiente de resolución
			InvEquips.Bounds = new Rectangle (300, 0, 300, 500);
			InvEquips.Stringificación = x => x.Nombre;
			InvEquips.InterceptarTeclado = false;
			Equiped.Bounds = new Rectangle (0, 0, 290, 500);
			Equiped.Stringificación = x => x.Nombre;
			Equiped.InterceptarTeclado = false;

			buildEquips ();
			buildEquiped ();

			InvEquips.Include ();
			Equiped.Include ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (Key.Right))
			{
				ListaSeleccionada = InvEquips;
			}
			if (InputManager.FuePresionado (Key.Left))
			{
				ListaSeleccionada = Equiped;
			}
			if (InputManager.FuePresionado (Key.Up))
			{
				ListaSeleccionada.SeleccionaAnterior ();
			}
			if (InputManager.FuePresionado (Key.Down))
			{
				ListaSeleccionada.SeleccionaSiguiente ();
			}
			if (InputManager.FuePresionado (Key.Enter))
			{
				if (ListaSeleccionada == InvEquips)
				{
					InvEquips.ObjetoEnCursor.EquiparEn (PersonajeSeleccionado.Equipamento);
				}
				else
				if (ListaSeleccionada == Equiped)
				{
					Equiped.ObjetoEnCursor.Desequipar ();
				}
				else
				{
					throw new Exception ();
				}
				buildEquips ();
				buildEquiped ();
			}
		}

		public override Color BgColor
		{
			get
			{
				return Color.Blue;
			}
		}

		void buildEquips ()
		{
			InvEquips.Clear ();
			foreach (var inv in ClanActual.Inventario)
			{
				var eq = inv as IEquipamento;
				if (eq != null)
					InvEquips.Add (eq);
			}
		}

		void buildEquiped (Personaje pj)
		{
			Equiped.Clear ();
			foreach (var eq in pj.Equipamento)
				Equiped.Add (eq);
		}

		void buildEquiped ()
		{
			buildEquiped (PersonajeSeleccionado);
		}
	}
}