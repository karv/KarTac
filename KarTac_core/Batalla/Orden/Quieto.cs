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

		public virtual TimeSpan Update (TimeSpan time)
		{
			Duración -= time;
			if (Duración.TotalMilliseconds < 0)
			{
				OnTerminar ();
				return time + Duración;
			}
			return time;
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