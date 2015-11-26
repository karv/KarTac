﻿using System;

namespace KarTac.Recursos
{
	public class HP : IRecurso
	{
		public void Tick (DateTime delta)
		{
			throw new NotImplementedException ();
		}

		public string Nombre
		{
			get
			{
				return "HP";
			}
		}

		float _max;
		float _actual;

		public float Max
		{
			get
			{
				return _max;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException (
						"value",
						Max,
						"Max no puede ser negativo.");
				_max = value;
				Valor = Math.Min (_actual, value);
			}
		}

		public float Valor
		{
			get
			{
				return _actual;
			}
			set
			{
				_actual = Math.Max (Math.Min (value, Max), 0);

				if (_actual <= 0)
					AlValorCero?.Invoke ();
				AlCambiarValor?.Invoke ();
			}
		}

		/// <summary>
		/// Ocurre cuando HP no es positivo
		/// </summary>
		public event Action AlValorCero;

		/// <summary>
		/// Ocurre cuando cambia el valor actual
		/// </summary>
		public event Action AlCambiarValor;

		public override string ToString ()
		{
			return string.Format ("HP:\t{0}/{1}", Valor, Max);
		}
	}
}