using System;

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
				_actual = Math.Min (value, Max);

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
	}
}