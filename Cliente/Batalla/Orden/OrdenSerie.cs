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

		bool IOrden.EsCancelable
		{
			get
			{
				foreach (var z in Serie)
				{
					if (!z.EsCancelable)
						return false;
				}
				return true;
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

		public UpdateReturnType Update (TimeSpan time)
		{
			while (Actual != null && time >= TimeSpan.Zero)
			{
				var updateRet = Actual.Update (time);
				time = updateRet.Restante;
				if (updateRet.Terminó)
				{
					Serie.Dequeue ();
					if (Serie.Count == 0)
					{
						AlTerminar?.Invoke ();
						Unidad.OrdenActual = null;
						return new UpdateReturnType (time, time);
					}
				}
				else
				{
					return new UpdateReturnType (time);
				}
			}
			return new UpdateReturnType (time, TimeSpan.Zero);
		}

		public Unidad Unidad { get; }
	}
}