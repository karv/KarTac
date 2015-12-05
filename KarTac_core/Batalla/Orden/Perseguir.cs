using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class Perseguir: Movimiento
	{
		const float _distanciaCercano = 3;

		public Unidad UnidadDestino;

		public Perseguir (Unidad yo)
			: base (yo)
		{
		}

		public override bool Update (GameTime time)
		{
			Destino = UnidadDestino.Pos;
			return base.Update (time);
		}

	}
}