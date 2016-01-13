using KarTac.Batalla;
using Microsoft.Xna.Framework;
using OpenTK.Input;
using System;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla de pausa en BattleScreen
	/// </summary>
	public class PauseScreen : ScreenDial
	{
		public PauseScreen (KarTacGame game, Campo campo)
			: base (game)
		{
			Campo = campo;
			Unidades = new Lista<Unidad> (this);
		}

		public PauseScreen (KarTacGame game, IScreen scr, Campo campo)
			: base (game, scr)
		{
			Campo = campo;
			Unidades = new Lista<Unidad> (this);
		}

		public override bool DibujarBase
		{
			get
			{
				return true;
			}
		}

		public Campo Campo { get; }

		public Lista<Unidad> Unidades;

		public override void Inicializar ()
		{
			foreach (var u in Campo.Unidades)
				Unidades.Add (u, u.Equipo.FlagColor);

			Unidades.Bounds = new Rectangle (0, 0, 500, 500);
			Unidades.ColorBG = Color.Pink * 0.3f;
			Unidades.Stringificación = x => (x.OrdenActual == null ? "*" : "") + x.PersonajeBase.Nombre;

			Unidades.Include ();
			Unidades.Inicializar ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (Key.Enter))
			{
				var uni = Unidades.ObjetoEnCursor;
				uni.OrdenActual = null;
				AlCancelarOrden?.Invoke (uni);
			}
			if (InputManager.FuePresionado (Key.Escape))
				Salir ();
		}

		/// <summary>
		/// Ocurre cuando se ordena a una unidad a cancelar su orden actual
		/// </summary>
		public event Action<Unidad> AlCancelarOrden;
	}
}