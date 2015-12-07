using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class Huir : IOrden
	{
		public Unidad Unidad { get; }

		public static Rectangle Tamaño { get; set; }

		public TimeSpan DuraciónRestante { get; set; }

		public Huir (Unidad unidad, TimeSpan duración)
		{
			DuraciónRestante = duración;
			Unidad = unidad;

		}

		public bool Update (GameTime time)
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

			Unidad.Mover (vectorMov, time.ElapsedGameTime);
			DuraciónRestante -= time.ElapsedGameTime;
			if (DuraciónRestante <= TimeSpan.Zero)
			{
				Unidad.OrdenActual = null;
				return true;
			}
			return false;
		}
	}
}