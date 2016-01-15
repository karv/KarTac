using System;
using Microsoft.Xna.Framework;

namespace KarTac
{
	public static class Utils
	{
		public static Random Rnd = new Random ();

		public static float Randomize (this Random rnd, float orig, double varn)
		{
			var Coef = (float)(1 + (rnd.NextDouble () * (2 * varn) - varn));
			return orig * Coef;
		}

		public static float Randomize (float orig, double varn)
		{
			return Rnd.Randomize (orig, varn);
		}

		public static Vector2 Normal (this Vector2 vect)
		{
			return new Vector2 (vect.Y, -vect.X);
		}

		public static float CoefProy (this Vector2 proyectando, Vector2 espacio)
		{
			return Vector2.Dot (proyectando, espacio);
		}

		/// <summary>
		/// Devuelve la proyección de un vector sobre otro
		/// </summary>
		public static Vector2 Proy (this Vector2 proyectando, Vector2 espacio)
		{
			espacio.Normalize ();
			return proyectando.CoefProy (espacio) * espacio;
		}

		/// <summary>
		/// Devuelve la proyección de un espacio sobre el ortogonal de otro
		/// </summary>
		public static Vector2 ProyOrto (this Vector2 proyectando, Vector2 espacio)
		{
			return proyectando - Proy (proyectando, espacio);
		}
	}
}