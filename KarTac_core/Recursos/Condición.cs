using System;

namespace KarTac.Recursos
{
	public class Condición : RecursoAcotadoRegenerativo
	{
		/// <summary>
		/// Regeneración por minuto.
		/// Se reduce a la mitad si condición es menor que 50% de su máximo.
		/// </summary>
		public float Regeneración = 30;

		public double CoefVelocidad
		{
			get
			{
				return (1 + Valor) / (2 * Max);
			}
		}

		public override string Icono
		{
			get
			{
				return @"Icons/Recursos/Condición"; 
			}
		}

		public override void CommitExp (double exp)
		{
			Max += (float)exp;
			Regeneración += (float)(exp * 0.1);
		}

		public override string Nombre
		{
			get
			{
				return "Condición";
			}
		}

		public override bool VisibleBatalla
		{
			get
			{
				return true;
			}
		}

		protected override void PedirExp (TimeSpan time, KarTac.Batalla.Campo campo)
		{
			PeticiónExpAcumulada += time.TotalSeconds * 2 * (1 - CoefVelocidad);
		}

		protected override float Regen
		{
			get
			{
				return CoefVelocidad < 0.5 ? Regeneración / 2 : Regeneración;
			}
		}

		#region IGuardable


		public override void Guardar (System.IO.BinaryWriter writer)
		{
			base.Guardar (writer);
			writer.Write (Regeneración);
		}

		public override void Cargar (System.IO.BinaryReader reader)
		{
			base.Cargar (reader);
			Regeneración = reader.ReadSingle ();
		}


		#endregion

		#if !DEBUG
		public override string ToString ()
		{
			return string.Format ("Condición: {0:P2}", CoefVelocidad);
		}
		#endif
	}
}

