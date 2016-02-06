using System;
using KarTac.Batalla.Shape;
using Moggle.Controles;
using KarTac.Batalla;
using KarTac.Batalla.Objetos;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls.Objetos
{
	public abstract class ÁreaEfecto : SBC, IObjeto
	{
		protected ÁreaEfecto (BattleScreen scr)
			: base (scr)
		{
			Campo = scr.CampoBatalla;
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