using System;
using KarTac.Batalla;
using Microsoft.Xna.Framework;

namespace KarTac.Recursos
{
	public class HP : RecursoAcotadoRegenerativo
	{
		public static IMultiRecurso BuildMulti ()
		{
			var ret = new MultiRecurso ();
			var hp = new HP ();
			hp.RecBase = ret;
			ret.Recursos.Add (hp);
			ret.Recursos.Add (new AtributoGenérico ("regen", false));

			return ret;
		}

		public MultiRecurso RecBase;

		public override string Nombre
		{
			get
			{
				return "HP";
			}
		}

		/// <summary>
		/// Regeneración constante por minuto
		/// </summary>
		public float Regeneración
		{
			get
			{
				return RecBase.Recursos [1].Valor;
			}
			set
			{
				RecBase.Recursos [1].Valor = value;
			}
		}

		protected override float Regen
		{
			get
			{
				return Regeneración;
			}
		}

		public override string Icono
		{
			get
			{
				return @"Icons/Recursos/HP"; 
			}
		}

		
		public override bool VisibleBatalla
		{
			get
			{
				return true;
			}
		}

		public override void CommitExp (double exp)
		{
			Max += (float)exp * 100f; 
			PeticiónExpAcumulada = 0;
		}

		protected override void PedirExp (TimeSpan time, Campo campo)
		{
			var pct = Valor / Max;

			PeticiónExpAcumulada += (1 - pct) * time.TotalMinutes;
			if (pct < 0.4)
				RecBase.Recursos [1].AcumularExp ((1 - pct) * time.TotalMinutes);
		}

		public override Color? ColorMostrarGanado
		{
			get
			{
				return Color.White;
			}
		}

		public override Color? ColorMostrarPerdido
		{
			get
			{
				return Color.Red;
			}
		}
	}
}