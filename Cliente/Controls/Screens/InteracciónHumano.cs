﻿using KarTac.Batalla;
using KarTac.Batalla.Orden;
using OpenTK.Input;
using Microsoft.Xna.Framework;
using System;

namespace KarTac.Cliente.Controls.Screens
{
	public class InteracciónHumano : ScreenDial, IInteractor
	{
		const double _distCercaUnidadClickCuadrada = 400;
		// dist = 20

		MenúTurno menú { get; }

		public Unidad UnidadActual
		{
			get
			{
				return menú.UnidadActual;
			}
		}

		public Campo CampoBatalla
		{
			get
			{
				return UnidadActual.CampoBatalla;
			}
		}

		public InteracciónHumano (Unidad unid, KarTacGame game)
			: base (game)
		{
			menú = new MenúTurno (this);
			menú.UnidadActual = unid;
			menú.Include ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

			if (InputManager.FuePresionado (Key.Down))
			{
				menú.ÍndiceSkillSel++;
			}
			if (InputManager.FuePresionado (Key.Up))
			{
				menú.ÍndiceSkillSel--;
			}
			if (InputManager.FuePresionado (Key.Enter))
			{
				var skill = menú.SkillSeleccionado;
				if (skill.Usable)
				{
					skill.Ejecutar ();
					Salir ();
				}
			}

			if (InputManager.FuePresionado (Key.Tab)) // Huir
			{
				var orden = new Huir (UnidadActual, TimeSpan.FromSeconds (2));
				UnidadActual.OrdenActual = orden;
				Salir ();
			}


			if (InputManager.FuePresionado (MouseButton.Left))
			{
				var clickLoc = new Vector2 (InputManager.EstadoActualMouse.X,
				                            InputManager.EstadoActualMouse.Y);

				// Ver si una unidad está cerca
				foreach (var x in CampoBatalla.Unidades)
				{
					var vectorDist = x.PosPrecisa - clickLoc;
					if (vectorDist.LengthSquared () < _distCercaUnidadClickCuadrada && x != UnidadActual)
					{
						var ord = new Perseguir (UnidadActual);
						ord.UnidadDestino = x;
						UnidadActual.OrdenActual = ord;
						Salir ();
						return;
					}
				}

				var ordMov = new Movimiento (UnidadActual);
				ordMov.Destino = new Point (InputManager.EstadoActualMouse.X,
				                            InputManager.EstadoActualMouse.Y);
				UnidadActual.OrdenActual = ordMov;
				Salir (); // Devuelve el control a la pantalla anterior
			}
		}

		public override void Inicializar ()
		{
			menú.Inicializar ();
		}

		#region IInteractor

		Unidad IInteractor.Unidad
		{
			get
			{
				return UnidadActual;
			}
		}

		#endregion
	}
}