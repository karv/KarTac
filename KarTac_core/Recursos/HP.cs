using System;
using KarTac.Batalla;
using Microsoft.Xna.Framework;

namespace KarTac.Recursos
{
	public class HP : RecursoAcotadoRegenerativo
	{

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
		public float Regeneración { get; set; }

		protected override float Regen
		{
			get
			{
				return Regeneración;
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

		#region Guardable

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
	}
}