using Microsoft.Xna.Framework;
using System;

namespace KarTac.Batalla.Orden
{
	public class Perseguir: OrdenMovCampoComún
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

		public override Vector2 VectorDeMuro ()
		{
			return Vector2.Zero;
		}

		public override Vector2 VectorDeUnidad (Unidad unidad)
		{
			if (unidad != Unidad)
				return Vector2.Zero;
			return UnidadDestino.PosPrecisa - Unidad.PosPrecisa;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			var dist = UnidadDestino.PosPrecisa - Unidad.PosPrecisa;
			if (dist.Length () < _distanciaCercano)
			{
				OnTerminar ();
				return new UpdateReturnType (time, TimeSpan.Zero);
			}
			EjecutarMov (time);
			return new UpdateReturnType (time);
		}
	}
}