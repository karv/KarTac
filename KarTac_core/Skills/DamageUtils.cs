using System;

namespace KarTac.Skills
{
	public static class DamageUtils
	{
		public const double MinDmg = 0.01;

		public static double CalcularDaño (double poderAta,
		                                   double poderDef,
		                                   double coef)
		{
			return Math.Max (MinDmg, (poderAta - poderDef / 2) * coef);
		}
	}
}

