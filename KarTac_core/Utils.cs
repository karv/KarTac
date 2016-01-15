using System;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

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

		public static float Deter (Vector2 a, Vector2 b)
		{
			return a.X * b.Y - a.Y * b.X;
		}

		public static float CoefProy (this Vector2 proyectando, Vector2 espacio)
		{
			return Vector2.Dot (proyectando, espacio) / espacio.Length ();
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

	public struct Segmento
	{
		/// <summary>
		/// Punto inicial
		/// </summary>
		public readonly Vector2 Inicial;

		/// <summary>
		/// Delta
		/// </summary>
		public readonly Vector2 Delta;

		/// <summary>
		/// Punto final
		/// </summary>
		/// <value>The final.</value>
		public Vector2 Final
		{
			get
			{
				return Inicial + Delta;
			}
		}

		public Segmento (Vector2 ini, Vector2 delta)
		{
			Inicial = ini;
			Delta = delta;
		}

		/// <summary>
		/// Revisa si dos segmentos intersectan
		/// </summary>
		public static bool Intersecta (Segmento s0, Segmento s1)
		{
			if (Utils.Deter (s0.Delta, s1.Delta) == 0)
				return false;
			var deterDelta = Utils.Deter (s0.Delta, s1.Delta);
			float t0 = Utils.Deter ((s1.Inicial - s0.Inicial), s1.Delta) / deterDelta;
			float t1 = Utils.Deter ((s1.Inicial - s0.Inicial), s0.Delta) / deterDelta;
			return t0 >= 0 && t1 >= 0 && t0 <= 1 && t1 <= 1;
		}
	}
}