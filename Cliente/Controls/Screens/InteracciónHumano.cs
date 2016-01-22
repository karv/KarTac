using KarTac.Batalla;
using KarTac.Batalla.Orden;
using OpenTK.Input;
using Microsoft.Xna.Framework;
using System;
using KarTac.Cliente.Controls.Primitivos;
using KarTac.Skills;

namespace KarTac.Cliente.Controls.Screens
{
	public class InteracciónHumano : ScreenDial, IInteractor
	{
		MenúTurno menú { get; }

		public ISelectorTarget Selector { get; }

		public Unidad UnidadActual
		{
			get
			{
				return menú.UnidadActual;
			}
		}

		public override bool DibujarBase
		{
			get
			{
				return true;
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
			Selector = new Selector (game, game.CurrentScreen);
			menú = new MenúTurno (this);
			menú.UnidadActual = unid;
			menú.Include ();
			sense = new SensorialExtremos (this);
			sense.AlHacerPresión += VP.Mover;
			sense.Include ();
		}

		void InputManager_AlSerPresionado (Key key)
		{
			IOrden orden;
			switch (key)
			{
			case Key.Down:
				menú.ÍndiceSkillSel++;
				return;
			case Key.Up:
				menú.ÍndiceSkillSel--;
				return;
			case Key.Enter:
			case Key.KeypadEnter:
				var skill = menú.SkillSeleccionado;
				if (skill.Usable)
				{
					var skillForma = skill as SkillTresPasosShaped; 
					Forma forma = null;
					if (skillForma != null)
					{
						forma = new Forma (ScreenBase, skillForma.GetÁrea ());
						forma.LoadContent ();
						forma.Color = Color.Yellow * 0.7f;

						Action iniciarDel = null;
						Action terminarDel = null;

						iniciarDel = delegate
						{
							forma.Include ();
							VP.CentrarEn (UnidadActual.Pos);
							CampoBatalla.RequiereInteracciónInmediata = true;
						};

						terminarDel = delegate
						{
							forma?.Exclude ();
							forma = null;
							skillForma.AlResponder -= terminarDel;
							skillForma.AlMostrarLista -= iniciarDel;
							skillForma.AlCancelar -= terminarDel;
						};

						skillForma.AlResponder += terminarDel;
						skillForma.AlMostrarLista += iniciarDel;
						skillForma.AlCancelar += terminarDel;
					}

					skill.Ejecutar ();
					Salir ();
				}
				return;
			case Key.Space:
				var sk = menú.SkillSeleccionado as IRangedSkill;
				var rng = (sk?.Rango ?? 40) * 0.9f;

				// Si usa ctrl, se carga; si no, rodea
				// Analysis disable ConvertIfStatementToConditionalTernaryExpression
				if (InputManager.EstáPresionado (Key.ShiftLeft))
					orden = new OrdenAtacar (UnidadActual, rng);
				else
					orden = new MantenerDistancia (UnidadActual, rng * 0.7, rng * 0.9);
				// Analysis restore ConvertIfStatementToConditionalTernaryExpression
				UnidadActual.OrdenActual = orden;
				Salir ();
				return;
			case Key.Tab:
				orden = new Huir (UnidadActual, TimeSpan.FromSeconds (5));
				UnidadActual.OrdenActual = orden;
				Salir ();
				return;
			case Key.Escape:
				(ScreenBase as BattleScreen)?.Pausar ();
				return;
			case Key.Z:
				var ordQuieto = new Sentinela (UnidadActual, TimeSpan.FromSeconds (3), 60);
				UnidadActual.OrdenActual = ordQuieto;
				Salir ();
				return;
			default:
				return;
			}
		}

		public override void Ejecutar ()
		{
			InputManager.AlSerPresionado += InputManager_AlSerPresionado;
			base.Ejecutar ();
		}

		public override void Salir ()
		{
			InputManager.AlSerPresionado -= InputManager_AlSerPresionado;
			base.Salir ();
		}

		SensorialExtremos sense;

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (MouseButton.Left))
			{
				var relClickPos = VP.PantallaACampo (new Point (InputManager.EstadoActualMouse.X,
				                                                InputManager.EstadoActualMouse.Y));
				var ordMov = new Movimiento (UnidadActual);
				ordMov.Destino = relClickPos;
				UnidadActual.OrdenActual = ordMov;
				Salir (); // Devuelve el control a la pantalla anterior
			}

			if (InputManager.FuePresionado (MouseButton.Right))
			{
				var relClickPos = VP.PantallaACampo (new Point (InputManager.EstadoActualMouse.X,
				                                                InputManager.EstadoActualMouse.Y));

				// Ver si una unidad está cerca
				foreach (var x in CampoBatalla.Unidades)
				{
					var selSkill = menú.SkillSeleccionado;
					double rang = (selSkill as IRangedSkill)?.Rango * 0.9 ?? 40;
					var vectorDist = x.PosPrecisa - relClickPos.ToVector2 ();
					if (vectorDist.Length () < rang && x != UnidadActual)
					{
						var ord = new Perseguir (UnidadActual);
						ord.UnidadDestino = x;
						UnidadActual.OrdenActual = ord;
						Salir ();
						return;
					}
				}
			}
		}

		ManejadorVP VP
		{
			get
			{
				return (ScreenBase as BattleScreen).ManejadorVista;
			}
		}

		public override void Inicializar ()
		{
			menú.Inicializar ();
			VP.CentrarEn (UnidadActual.Pos);
			sense.Inicializar ();
		}

		public override void UnloadContent ()
		{
			//((IDisposable)menú).Dispose ();
			menú.Unload ();

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