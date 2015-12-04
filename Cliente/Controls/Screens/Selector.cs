using KarTac.Batalla;
using System;
using System.Collections.Generic;

namespace KarTac.Cliente.Controls.Screens
{
	public class Selector: Screen, ISelectorTarget
	{
		public Selector (KarTacGame game)
			: base (game)
		{
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
		public void Selecciona ()
		{
			var diálogo = new ScreenPedirDeLista<Unidad> (this, Game);

			foreach (var x in PosiblesBlancos)
			{
				diálogo.Add (x);
			}

			diálogo.AlTerminar += delegate
			{
				var sel = diálogo.SelecciónActual;
				if (sel.Count == 0 && !PuedeSerVacío)
					sel.Add (diálogo.ObjetoEnCursor);
				AlResponder?.Invoke (new SelecciónRespuesta (sel));
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

		public event Action<SelecciónRespuesta> AlResponder;
	}
}