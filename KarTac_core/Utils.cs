using System;

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
	}
}