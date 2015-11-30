using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Shape
{
	public class Rectángulo : IShape
	{
		Rectangle coords;

		public Rectángulo (Rectangle rect)
		{
			coords = rect;
		}

		public implicit operator Rectangle (Rectángulo rect)
		{
			return coords;
		}

		public implicit operator Rectángulo (Rectangle rect)
		{
			return new Rectángulo (rect);
		}

		public bool Contiene (Point punto)
		{
			return coords.Contains (punto);
		}
	}
}