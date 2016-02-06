using System;
using KarTac.Batalla.Shape;

namespace KarTac.Batalla.Objetos
{
	public abstract class ÁreaEfecto :IObjeto
	{
		protected ÁreaEfecto (Campo campo)
		{
			Campo = campo;
		}

		public Campo Campo { get; }

		public abstract void Dibujar ();

		public virtual void Update (TimeSpan time)
		{
			foreach (var x in Campo.Unidades)
			{
				if (Forma.Contiene (x.Pos) && Selector (x))
				{
					EfectoEn (x);
				}
			}
		}

		public abstract void EfectoEn (Unidad u);

		/// <summary>
		/// Selecciona qué unidades pueden (además de estar en Forma).
		/// </summary>
		protected abstract bool Selector (Unidad unidad);

		public IShape Forma { get; set; }
	}
}