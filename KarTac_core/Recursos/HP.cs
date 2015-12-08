using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using KarTac.Batalla.Exp;

namespace KarTac.Recursos
{
	public class HP : IRecurso
	{
		public void Tick (GameTime delta)
		{
			Valor += (float)delta.ElapsedGameTime.TotalMinutes * Regen; // Regeneración natural
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

		/// <summary>
		/// HP regenerada por minuto
		/// </summary>
		public float Regen { get; set; }

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

		void  IExp.CommitExp (double exp)
		{
			Max += (float)exp; //TODO Aquí no creo que termine siendo así de simple.
			PeticiónExpAcumulada = 0;
		}

		public double PeticiónExpAcumulada { get; private set; }

		void IRecurso.PedirExp (TimeSpan time, Campo campo)
		{
			var pct = Valor / Max;

			PeticiónExpAcumulada += (1 - pct) * time.Minutes;
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