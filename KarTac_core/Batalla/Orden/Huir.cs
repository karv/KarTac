using System;
using Microsoft.Xna.Framework;

namespace KarTac.Batalla.Orden
{
	public class Huir : OrdenMovCampoComún
	{
		public TimeSpan DuraciónRestante { get; set; }

		public Huir (Unidad unidad, TimeSpan duración)
			: base (unidad)
		{
			DuraciónRestante = duración;
		}

		public override Vector2 VectorDeMuro (KarTac.Batalla.Objetos.Pared pared)
		{
			var norm = pared.Normal (Unidad.PosPrecisa);
			return norm * pared.ImportanciaCoef / norm.LengthSquared ();
		}

		public override Vector2 VectorDeUnidad (Unidad unidad)
		{
			if (unidad != Unidad)
			{
				var ret = (Unidad.PosPrecisa - unidad.PosPrecisa);
				ret /= ret.LengthSquared ();
				if (Unidad.Equipo.EsAliado (unidad))
					ret *= -0.3f;
				return ret;
			}
			return Vector2.Zero;
		}

		public override UpdateReturnType Update (TimeSpan time)
		{
			DuraciónRestante -= time;
			if (DuraciónRestante >= TimeSpan.Zero)
			{
				EjecutarMov (time);
				return new UpdateReturnType (time);
			}
			OnTerminar ();
			return new UpdateReturnType (time, TimeSpan.Zero);
		}
	}
}