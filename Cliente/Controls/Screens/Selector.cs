using KarTac.Batalla;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Screens
{
	public class Selector : ScreenDial, ISelectorTarget
	{
		public Selector (KarTacGame game, IScreen screenBase)
			: base (game, screenBase)
		{
		}

		public override bool DibujarBase
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Revisa si es posible dar una salida con la función Selecciona
		/// </summary>
		public bool Validar ()
		{
			return PosiblesBlancos.Count > 0;
		}

		/// <summary>
		/// Ejecuta el selector y devuelve los seleccionados
		/// </summary>
		public void Selecciona (Unidad unid)
		{
			var diálogo = new ScreenPedirDeLista<Unidad> (Juego);

			var lista = diálogo.ListaComponente;

			lista.Stringificación = new Func<Unidad, string> (unidad => string.Format ("{0}: {1}",
			                                                                           unidad.PersonajeBase.Nombre,
			                                                                           unidad.AtributosActuales.HP));

			foreach (var x in PosiblesBlancos)
			{
				lista.Add (x, unid.Equipo.EsAliado (x) ? Color.White : Color.Red);
			}

			diálogo.AlTerminar += delegate
			{
				var sel = diálogo.Salida;
				if (sel.Tipo == ScreenPedirDeLista<Unidad>.TipoSalida.EnumTipoSalida.Aceptación)
				{
					var selección = new List<Unidad> (sel.Selección);
					if (selección.Count == 0 && !PuedeSerVacío)
						selección.Add (diálogo.ObjetoEnCursor);
					AlResponder?.Invoke (new SelecciónRespuesta (selección));
				}
				else
				{
					AlCancelar?.Invoke ();
				}
				ClearStatus ();
				Salir ();
			};

			diálogo.Ejecutar ();
		}


		/// <summary>
		/// Espablece el máximo número
		/// </summary>
		/// <value>The max select.</value>
		public int MaxSelect { get; set; }

		/// <summary>
		/// Establece si el número máximo de blancos es el realidad el número exacto de blancos.
		/// </summary>
		public bool IgualdadEstricta { get; set; }

		public bool PuedeSerVacío { set; private get; }

		/// <summary>
		/// Establece los posibles blancos
		/// </summary>
		public IList<Unidad> PosiblesBlancos { get; set; }

		#region IScreen

		#endregion

		public override void Inicializar ()
		{
			foreach (var x in Controles)
			{
				x.Inicializar ();
			}
		}

		public event Action<SelecciónRespuesta> AlResponder;

		public event Action AlCancelar;

		public void ClearStatus ()
		{
			AlResponder = null;
		}
	}
}