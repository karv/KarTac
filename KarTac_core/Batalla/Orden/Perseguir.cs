using Microsoft.Xna.Framework;
using System;

namespace KarTac.Batalla.Orden
{
	public class Perseguir: Movimiento
	{
		const float _distanciaCercano = 30;

		public Unidad UnidadDestino;

		public Perseguir (Unidad yo)
			: base (yo)
		{
		}

		public Perseguir (Unidad yo, Unidad destino)
			: this (yo)
		{
			UnidadDestino = destino;
		}

		public override TimeSpan Update (TimeSpan time)
		{
			var dist = UnidadDestino.PosPrecisa - Unidad.PosPrecisa;
			if (dist.Length () < _distanciaCercano)
			{
				Unidad.OrdenActual = null;
				return TimeSpan.Zero;
			}
			Destino = UnidadDestino.Pos;
			return base.Update (time);
		}
	}
}