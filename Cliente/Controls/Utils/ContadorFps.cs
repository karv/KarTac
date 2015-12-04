using System;

namespace KarTac.Cliente.Controls.Utils
{
	/// <summary>
	/// Permite obtener FPS de un proceso cíclico
	/// Se debe invocar a Update en cada ciclo
	/// </summary>
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

		/// <summary>
		/// Agrega una muestra para aproximar a Fps
		/// </summary>
		public void Update (TimeSpan time)
		{
			agregaMuestra (time.TotalSeconds);
		}

		/// <summary>
		/// Devuelve los cuadros por segundo según la muestra
		/// </summary>
		public double Fps
		{
			get
			{
				return suma / muestreo.Length;
			}
		}
	}
}