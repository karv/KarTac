using System;

namespace KarTac.Batalla.Exp
{
	public struct TotalExp
	{
		/// <summary>
		/// Experiencia por estar vivo
		/// </summary>
		public float ExpVivo;

		/// <summary>
		/// La experiencia total
		/// </summary>
		public float Total
		{
			get
			{
				return ExpVivo;
			}
		}
	}
}