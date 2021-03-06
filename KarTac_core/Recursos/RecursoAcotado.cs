﻿using System;
using KarTac.Batalla;
using KarTac.Personajes;

namespace KarTac.Recursos
{
	public abstract class RecursoAcotado : IRecurso
	{
		protected RecursoAcotado ()
		{
		}

		float _max;
		float _actual;

		AtributosPersonaje _conjAtrib;

		public AtributosPersonaje ConjAtrib
		{
			get
			{
				return _conjAtrib;
			}
			set
			{
				_conjAtrib = value;
				OnAsignarConjAtrib ();
			}
		}

		protected virtual void OnAsignarConjAtrib ()
		{
		}

		public void AcumularExp (double exp)
		{
			PeticiónExpAcumulada += exp;
		}

		public void ResetExp ()
		{
			PeticiónExpAcumulada = 0;
		}

		protected virtual float Inicial
		{
			get
			{
				return Max;
			}
		}

		public abstract string Icono { get; }

		public abstract bool VisibleBatalla { get; }

		public void Reestablecer ()
		{
			Valor = Inicial;
		}

		public double PeticiónExpAcumulada { get; set; }

		public virtual float Max
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
				Valor = Math.Min (_actual, Valor);
			}
		}

		public virtual float Valor
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
				if (_actual == _max)
					AlValorMáximo?.Invoke ();
				AlCambiarValor?.Invoke ();
			}
		}

		public abstract string Nombre { get; }

		public abstract void CommitExp (double exp);

		public virtual void Tick (TimeSpan delta)
		{
		}

		public override string ToString ()
		{
			return string.Format ("{0}:\t{1}/{2}", Nombre, Valor, Max);
		}

		void IRecurso.PedirExp (TimeSpan time, Campo campo)
		{
			PedirExp (time, campo);
		}

		protected abstract void PedirExp (TimeSpan time, Campo campo);

		public virtual Microsoft.Xna.Framework.Color? ColorMostrarGanado
		{
			get
			{
				return null;
			}
		}

		public virtual Microsoft.Xna.Framework.Color? ColorMostrarPerdido
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Ocurre cuando se alcanza el vamor máximo
		/// </summary>
		public event Action AlValorMáximo;

		/// <summary>
		/// Ocurre cuando HP no es positivo
		/// </summary>
		public event Action AlValorCero;

		/// <summary>
		/// Ocurre cuando cambia el valor actual
		/// </summary>
		public event Action AlCambiarValor;

		#region Guardable

		public virtual void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (Nombre);
			writer.Write (Max);
		}

		public virtual void Cargar (System.IO.BinaryReader reader)
		{
			Max = reader.ReadSingle ();
		}

		#endregion
	}
}