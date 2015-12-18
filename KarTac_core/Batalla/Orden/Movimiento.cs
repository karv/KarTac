using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	/// <summary>
	/// Orden de movimiento con destino fijo
	/// </summary>
	public class Movimiento : IOrden
	{
		/// <summary>
		/// Distancia al destino para considerar que llegó
		/// </summary>
		const float _distanciaCercano = 3;

		public Point Destino { get; set; }

		public Unidad Unidad { get; }

		public Movimiento (Unidad unidad)
		{
			Unidad = unidad;
		}

		public virtual bool Update (GameTime time)
		{
			var movDir = (Destino - Unidad.Pos).ToVector2 ();
			if (movDir.Length () < _distanciaCercano)
			{
				Unidad.PosPrecisa = Destino.ToVector2 ();
				OnTerminar ();
				return true;
			}
			Unidad.Mover (movDir, time.ElapsedGameTime);
			return false;
		}

		public virtual void OnTerminar ()
		{
			if (Unidad.OrdenActual == this)
				Unidad.OrdenActual = null;
			AlTerminar?.Invoke ();
		}

		public event System.Action AlTerminar;
	}
}