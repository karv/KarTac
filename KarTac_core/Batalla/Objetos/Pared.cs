using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Objetos
{
	public class Pared : IObjetoCampo
	{
		public readonly Vector2 P0;
		public readonly Vector2 P1;

		public Campo Campo { get; set; }

		public Pared (Vector2 p0, Vector2 p1)
		{
			P0 = p0;
			P1 = p1;
		}

		public Vector2 GetVector ()
		{
			return P1 - P0;
		}

		public Pared (Campo campo, Vector2 p0, Vector2 p1)
			: this (p0, p1)
		{
			Campo = campo;
		}

		public Vector2 Normal (Vector2 p)
		{
			var rel_p = p - P0; // Punto relativo
			var coef = rel_p.CoefProy (GetVector ());
			if (coef > GetVector ().Length ()) // Usar a P1
				return p - P1;
			else if (coef < 0)
			{
				return p - P0;
			}
			return rel_p.ProyOrto (GetVector ());
		}
	}
}