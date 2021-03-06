﻿using System;

namespace KarTac.Skills
{
	public static class DamageUtils
	{
		public const double MinDmg = 0.01;

		static Random r
		{
			get
			{
				return Utils.Rnd;
			}
		}

		public static double CalcularDaño (double poderAta,
		                                   double poderDef,
		                                   double coef)
		{
			var orig = Math.Max (MinDmg, (poderAta - poderDef / 2) * coef);
			return r.Randomize ((float)orig, 0.1);
		}

	}
}

