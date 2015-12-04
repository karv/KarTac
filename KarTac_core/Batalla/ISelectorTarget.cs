using System.Collections.Generic;
using System;

namespace KarTac.Batalla
{
	/// <summary>
	/// Representa un método para seleccionar blancos
	/// Una clase que herede esto se supone que debe ser una interfás(gráfica) de interacción con el usuario
	/// O una IA
	/// </summary>
	public interface ISelectorTarget
	{
		/// <summary>
		/// Espablece el máximo número 
		/// </summary>
		int MaxSelect { set; }

		/// <summary>
		/// Establece si el número máximo de blancos es el realidad el número exacto de blancos.
		/// </summary>
		bool IgualdadEstricta { set; }

		/// <summary>
		/// Establece los posibles blancos
		/// </summary>
		IList<Unidad> PosiblesBlancos { set; }

		/// <summary>
		/// Revisa si es posible dar una salida con la función Selecciona
		/// </summary>
		bool Validar ();

		/// <summary>
		/// Ejecuta el selector y devuelve los seleccionados
		/// </summary>
		void Selecciona ();

		event Action<SelecciónRespuesta> AlResponder;
	}

	public interface IResponseSelector
	{
	}

	public struct SelecciónRespuesta : IResponseSelector
	{
		public SelecciónRespuesta (IList<Unidad> unidades)
		{
			Selección = unidades;
		}

		public readonly IList<Unidad> Selección;
	}

}