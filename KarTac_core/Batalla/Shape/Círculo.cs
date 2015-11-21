using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Shape
{
	public class Círculo:IShape
	{
		public Point Centro { get; set; }

		public float Radio { get; set; }

		public Círculo (Point centro, float radio)
		{
			Centro = centro;
			Radio = radio;
		}

		public bool Contiene (Point punto)
		{
			var r = Centro - punto;
			var vect = new Vector2 (r.X, r.Y);
			return vect.LengthSquared () < Radio * Radio;
		}
	}
}