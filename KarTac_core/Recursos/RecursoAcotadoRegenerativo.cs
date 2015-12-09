using System;

namespace KarTac.Recursos
{
	public abstract class RecursoAcotadoRegenerativo : RecursoAcotado
	{
		/// <summary>
		/// Regeneración por minuto
		/// </summary>
		protected abstract float Regen { get; }

		public override void Tick (Microsoft.Xna.Framework.GameTime delta)
		{
			DoRegen (delta.ElapsedGameTime);
		}

		void DoRegen (TimeSpan delta)
		{
			Valor += (float)delta.TotalMinutes * Regen; // Regeneración natural
		}
	}
}