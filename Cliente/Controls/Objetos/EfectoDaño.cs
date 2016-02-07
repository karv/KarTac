using KarTac.Controls.Screens;
using System;
using KarTac.Batalla;
using Moggle;
using Microsoft.Xna.Framework;
using KarTac.Batalla.Shape;

namespace KarTac.Controls.Objetos
{
	public class EfectoDaño : ÁreaEfecto
	{
		public EfectoDaño (BattleScreen scr) // TODO ¿No debe ser esto un objeto de campo y no un control?
			: base (scr)
		{
			Forma = new Círculo (Point.Zero, 0);
		}

		Círculo formaCirc
		{
			get
			{
				return Forma as Círculo;
			}
		}

		public double PoderDaño;
		public Func<Unidad, double> PoderDefensivo;
		public double Coef;

		public Point Centro
		{
			set
			{
				formaCirc.Centro = value;
			}
			get
			{
				return formaCirc.Centro;
			}
		}

		public float Radio
		{
			get
			{
				return formaCirc.Radio;
			}
			set
			{
				formaCirc.Radio = value;
			}
		}

		public TimeSpan DuraciónRestante { get; set; }

		public override void EfectoEn (Unidad u, TimeSpan time)
		{
			var daño = KarTac.Skills.DamageUtils.CalcularDaño (PoderDaño, PoderDefensivo (u), Coef * time.TotalSeconds);
			u.AtributosActuales.HP.Valor -= (float)daño;
			u.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.5 * time.TotalSeconds;
		}

		public override void Update (TimeSpan time)
		{
			base.Update (time);
			DuraciónRestante -= time;
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
			var centro = VP.CampoAPantalla (Centro);
			Screen.Batch.DrawCircle (centro.ToVector2 (), Radio, 20, Color.White);
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