using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public abstract class OrdenMovCampoComún : IOrden
	{
		public OrdenMovCampoComún (Unidad unidad)
		{
			Unidad = unidad;
		}

		public Unidad Unidad { get; }

		public Rectangle Tamaño
		{
			get
			{
				return Unidad.CampoBatalla.Área;
			}
		}


		public abstract Vector2 VectorDeUnidad (Unidad unidad);

		public abstract Vector2 VectorDeMuro ();

		public virtual Vector2 VectorMovimiento ()
		{
			var vectorMov = new Vector2 ();
			foreach (var u in Unidad.CampoBatalla.UnidadesVivas)
			{
				vectorMov += VectorDeUnidad (u);
			}

			// Sumar las paredes
			vectorMov += VectorDeMuro ();

			return vectorMov;
		}

		protected virtual void EjecutarMov (TimeSpan time)
		{
			var vectorMov = VectorMovimiento ();
			Unidad.Mover (vectorMov, time);
		}

		protected virtual void OnTerminar ()
		{
			Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public abstract UpdateReturnType Update (TimeSpan time);

		public event Action AlTerminar;
	}
}

