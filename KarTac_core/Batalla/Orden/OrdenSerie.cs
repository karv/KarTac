using System;
using System.Collections.Generic;

namespace KarTac.Batalla.Orden
{
	/// <summary>
	/// Permite la concatenación en serie de órdenes
	/// </summary>
	public class OrdenSerie : IOrden
	{
		public Queue<IOrden> Serie { get; }

		public IOrden Actual
		{
			get
			{
				return Serie.Count > 0 ? Serie.Peek () : null;
			}
		}

		public OrdenSerie (Unidad unidad)
			: this (unidad, new IOrden[0])
		{
			
		}

		public OrdenSerie (Unidad unidad, IEnumerable<IOrden> órdenes)
		{
			Serie = new Queue<IOrden> (órdenes);
			Unidad = unidad;
		}

		public event Action AlTerminar;

		public TimeSpan Update (TimeSpan time)
		{
			while (Actual != null && time > TimeSpan.Zero)
			{
				var tiempo = Actual.Update (time);
				time -= tiempo;
				if (time > TimeSpan.Zero)
				{
					Serie.Dequeue ();
					if (Serie.Count == 0)
					{
						AlTerminar?.Invoke ();
						Unidad.OrdenActual = null;
					}
				}
			}
			return time;
		}

		public Unidad Unidad { get; }
	}
}

