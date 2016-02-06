using System;

namespace KarTac.Recursos
{
	public abstract class RecursoAcotadoRegenerativo : RecursoAcotado
	{
		/// <summary>
		/// Regeneración por minuto
		/// </summary>
		protected abstract float Regen { get; }

		public override void Tick (TimeSpan delta)
		{
			DoRegen (delta);
		}

		void DoRegen (TimeSpan delta)
		{
			Valor += (float)delta.TotalMinutes * Regen; // Regeneración natural
		}
	}
}