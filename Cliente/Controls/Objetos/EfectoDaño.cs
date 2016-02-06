using KarTac.Controls.Screens;
using System;
using KarTac.Batalla;
using Moggle;
using Microsoft.Xna.Framework;

namespace KarTac.Controls.Objetos
{
	public class EfectoDaño : ÁreaEfecto
	{
		public EfectoDaño (BattleScreen scr)
			: base (scr)
		{
		}

		public double PoderDaño;
		public Func<Unidad, double> PoderDefensivo;
		public double Coef;

		public Point Centro;
		public float Radio;

		public TimeSpan DuraciónRestante { get; set; }

		public override void EfectoEn (Unidad u, TimeSpan time)
		{
			var daño = KarTac.Skills.DamageUtils.CalcularDaño (PoderDaño, PoderDefensivo (u), Coef * time.TotalSeconds);
			u.AtributosActuales.HP.Valor -= (float)daño;
			u.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;
		}

		public override void Update (TimeSpan time)
		{
			base.Update (time);
			DuraciónRestante = -time;
			if (DuraciónRestante <= TimeSpan.Zero)
			{
				Dispose ();
			}
		}

		protected override bool Selector (Unidad unidad)
		{
			return unidad.EstáVivo;
		}

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.DrawCircle (Centro.ToVector2 (), Radio, 20, Color.White);
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (Centro.X - (int)Radio, Centro.Y - (int)Radio, 2 * (int)Radio, 2 * (int)Radio);
		}

		public override void LoadContent ()
		{
		}
	}
}