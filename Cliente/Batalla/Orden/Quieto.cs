using System;

namespace KarTac.Batalla.Orden
{
	public class Quieto : IOrden
	{
		public Unidad Unidad { get; }

		public TimeSpan Duración { get; set; }

		public Quieto (Unidad unidad, TimeSpan time)
		{
			Unidad = unidad;
			Duración = time;
		}

		protected Quieto (Unidad unidad)
		{
			Unidad = unidad;
		}

		bool IOrden.EsCancelable
		{
			get
			{
				return false;
			}
		}

		public virtual UpdateReturnType Update (TimeSpan time)
		{
			Duración -= time;
			UpdateReturnType ret;
			if (Duración.TotalMilliseconds <= 0)
			{
				OnTerminar ();
				ret = new UpdateReturnType (time, TimeSpan.Zero);
			}
			else
			{
				ret = new UpdateReturnType (time);
			}
			return ret;
		}

		public virtual void OnTerminar ()
		{
			if (Unidad.OrdenActual == this)
				Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public event Action AlTerminar;
	}
}