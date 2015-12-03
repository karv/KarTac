using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla
{
	/// <summary>
	/// Representa un campo de batalla
	/// </summary>
	public class Campo
	{
		/// <summary>
		/// Experiencia por minuto
		/// </summary>
		public float ExpPorMinuto = 1;

		public ISelectorTarget SelectorTarget { get; set; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

		public Campo ()
		{
			Unidades = new List<Unidad> ();
		}


		public void Tick (GameTime delta)
		{
			foreach (var x in Unidades)
			{
				var ord = x.OrdenActual;
				if (ord != null)
					ord.Update (delta);
				else
				{
					// Pedir orden al usuario o a la IA
					AlRequerirOrdenAntes?.Invoke (x);
					x.Interactor.Ejecutar ();
					x.Interactor.AlTerminar += () => AlRequerirOrdenDespués?.Invoke (x);
				}
			}


			RecibirExp (delta.ElapsedGameTime);

			foreach (var x in Unidades)
			{
				x.AcumularPetición (delta.ElapsedGameTime);
				// Sus recursos
				foreach (var y in x.AtributosActuales.Recs)
				{
					y.Tick (delta);
				}
			}
		}

		/// <summary>
		/// Unidades vivas reciben exp
		/// </summary>
		void RecibirExp (TimeSpan delta)
		{
			var mins = delta.TotalMinutes;
			foreach (var uni in Unidades.Where (x => x.PuedeRecibirExp))
			{
				uni.RecibirExp (mins * ExpPorMinuto);
			}
		}

		public event Action<Unidad> AlRequerirOrdenAntes;
		public event Action<Unidad> AlRequerirOrdenDespués;
	}
}