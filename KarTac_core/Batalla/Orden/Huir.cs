using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class Huir : IOrden
	{
		public Unidad Unidad { get; }

		public Rectangle Tamaño
		{
			get
			{
				return Unidad.CampoBatalla.Área;
			}
		}

		public TimeSpan DuraciónRestante { get; set; }

		public Huir (Unidad unidad, TimeSpan duración)
		{
			DuraciónRestante = duración;
			Unidad = unidad;

		}

		public bool Update (TimeSpan time)
		{
			var vectorMov = new Vector2 ();
			foreach (var u in Unidad.CampoBatalla.UnidadesVivas)
			{
				if (u != Unidad)
				{
					var sumando = (Unidad.PosPrecisa - u.PosPrecisa);
					sumando /= sumando.LengthSquared ();
					if (Unidad.Equipo.EsAliado (u))
						sumando *= -1;
					vectorMov += sumando;
				}
			}

			// Sumar las paredes
			vectorMov += new Vector2 (1 / (Unidad.PosPrecisa.X - Tamaño.Left), 0);
			vectorMov += new Vector2 (1 / (Unidad.PosPrecisa.X - Tamaño.Right), 0);
			vectorMov += new Vector2 (0, 1 / (Unidad.PosPrecisa.Y - Tamaño.Top));
			vectorMov += new Vector2 (0, 1 / (Unidad.PosPrecisa.Y - Tamaño.Bottom));

			Unidad.Mover (vectorMov, time);
			DuraciónRestante -= time;
			if (DuraciónRestante <= TimeSpan.Zero)
			{
				Unidad.OrdenActual = null;
				AlTerminar?.Invoke ();
				return true;
			}
			return false;
		}

		public event Action AlTerminar;
	}
}