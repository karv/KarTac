using System;

namespace KarTac
{
	public static class Utils
	{
		public static float Randomize (this Random rnd, float orig, double varn)
		{
			var Coef = (float)(1 + (rnd.NextDouble () * (2 * varn) - varn));
			return orig * Coef;
		}
	}
}

