using KarTac.Batalla;

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
			return true;
		}

		/// <summary>
		/// Ejecuta el selector y devuelve los seleccionados
		/// </summary>
		public System.Collections.Generic.IList<KarTac.Batalla.Unidad> Selecciona ()
		{
			return PosiblesBlancos;
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

		/// <summary>
		/// Establece los posibles blancos
		/// </summary>
		public System.Collections.Generic.IList<KarTac.Batalla.Unidad> PosiblesBlancos { get; set; }

		#region IScreen

		#endregion
	}
}