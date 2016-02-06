using System;
using KarTac.Batalla.Shape;
using Moggle.Controles;
using KarTac.Batalla;
using Moggle.Screens;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Objetos
{
	public abstract class ÁreaEfecto : SBC, IObjeto
	{
		protected ÁreaEfecto (Campo campo, IScreen scr)
			: base (scr)
		{
			Campo = campo;
		}

		void IObjeto.Dibujar ()
		{
			Dibujar (null);
		}

		public Campo Campo { get; }

		public virtual void Update (TimeSpan time)
		{
			foreach (var x in Campo.Unidades)
			{
				if ((Forma?.Contiene (x.Pos) ?? true) && Selector (x))
				{
					EfectoEn (x);
				}
			}
		}

		public override void Update (GameTime gameTime)
		{
			Update (gameTime.ElapsedGameTime);
		}

		public abstract void EfectoEn (Unidad u);

		/// <summary>
		/// Selecciona qué unidades pueden (además de estar en Forma).
		/// </summary>
		protected abstract bool Selector (Unidad unidad);

		public IShape Forma { get; set; }
	}
}