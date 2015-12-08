using System;
using KarTac.Batalla;

namespace KarTac.Recursos
{
	public abstract class RecursoAcotado : IRecurso
	{
		protected RecursoAcotado ()
		{
		}

		float _max;
		float _actual;

		public double PeticiónExpAcumulada { get; protected set; }

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

		public abstract string Nombre { get; }

		public abstract void CommitExp (double exp);

		public override string ToString ()
		{
			return string.Format ("{0}:\t{1}/{2}", Nombre, Valor, Max);
		}

		void IRecurso.PedirExp (TimeSpan time, Campo campo)
		{
			PedirExp (time, campo);
		}

		protected abstract void PedirExp (TimeSpan time, Campo campo);

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