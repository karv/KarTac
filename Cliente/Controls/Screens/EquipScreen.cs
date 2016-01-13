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
	public class EquipScreen : ScreenDial
	{
		/// <summary>
		/// Devuelve el clan al que pertenece todo esto
		/// </summary>
		public Clan ClanActual { get; }

		public override bool DibujarBase
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Devuelve el personaje seleccionado
		/// </summary>
		public Personaje PersonajeSeleccionado { get; private set; }

		Lista<IEquipamento> InvEquips { get; }

		Lista<IEquipamento> Equiped { get; }

		Botón botónOk { get; }

		public IListaControl ListaSeleccionada { get; private set; }

		public EquipScreen (KarTacGame game, Clan clan, Personaje pj)
			: base (game)
		{
			ClanActual = clan;
			PersonajeSeleccionado = pj;

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
			Equiped.ColorSel = Color.Green * 0.5f;

			botónOk = new Botón (this, new Rectangle (30, 550, 30, 30));
			botónOk.Color = Color.Yellow;
			botónOk.Textura = "Rect";
			botónOk.AlClick += delegate
			{
				Salir ();
			};

			buildEquips ();
			buildEquiped ();

			InvEquips.Include ();
			Equiped.Include ();
			botónOk.Include ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (Key.Right))
			{
				ListaSeleccionada = InvEquips;
				InvEquips.ColorSel = Color.Green * 0.5f;
				Equiped.ColorSel = Color.White * 0.5f;
			}
			if (InputManager.FuePresionado (Key.Left))
			{
				ListaSeleccionada = Equiped;
				InvEquips.ColorSel = Color.White * 0.5f;
				Equiped.ColorSel = Color.Green * 0.5f;
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
					if (InvEquips.Count > 0)
					{
						var equip = InvEquips.ObjetoEnCursor;
						PersonajeSeleccionado.Equipamento.Equiparse (equip, ClanActual.Inventario);
					}
				}
				else
				if (ListaSeleccionada == Equiped)
				{
					if (Equiped.Count > 0)
					{
						var equip = Equiped.ObjetoEnCursor;
						equip.Desequipar ();
						ClanActual.Inventario.Add (equip);
					}
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
			InvEquips.CursorIndex = 0;
		}

		void buildEquiped (Personaje pj)
		{
			Equiped.Clear ();
			foreach (var eq in pj.Equipamento)
				Equiped.Add (eq);
			InvEquips.CursorIndex = 0;
		}

		void buildEquiped ()
		{
			buildEquiped (PersonajeSeleccionado);
		}
	}
}