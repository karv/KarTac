using System;

namespace KarTac.Cliente.Controls.Utils
{
	public class ContadorFps
	{
		readonly double[] muestreo;
		int currInd;

		public ContadorFps (int samples = 50)
		{
			muestreo = new double[samples];
		}

		void agregaMuestra (double t)
		{
			muestreo [currInd] = t;
			currInd = (currInd + 1) % muestreo.Length;
		}

		double suma
		{
			get
			{
				var ret = 0.0;
				foreach (var x in muestreo)
				{
					ret += x;
				}
				return ret;
			}
		}

		public void Update (TimeSpan time)
		{
			agregaMuestra (time.TotalSeconds);
		}

		public double Fps
		{
			get
			{
				return suma / muestreo.Length;
			}
		}
	}
}