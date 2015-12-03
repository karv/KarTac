using System;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Control cronometrado
	/// </summary>
	public abstract class SBCC : SBC
	{
		protected SBCC (IScreen screen)
			: base (screen)
		{
			TiempoEntreCambios = TimeSpan.FromSeconds (2);
		}

		public TimeSpan TiempoEntreCambios { get; set; }

		/// <summary>
		/// Tiempo acumulado desde el último cambio
		/// </summary>
		/// <value>The tiempo acumulado.</value>
		protected TimeSpan TiempoAcumulado { get; private set; }

		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update (gameTime);

			TiempoAcumulado += gameTime.ElapsedGameTime;
			if (TiempoAcumulado > TiempoEntreCambios)
			{
				TiempoAcumulado = TimeSpan.Zero;
				OnChrono ();
			}
		}

		/// <summary>
		/// Se llama cuando ocurre el tick cronometrizado
		/// </summary>
		protected void OnChrono ()
		{
			AlTick?.Invoke ();
		}

		public event Action AlTick;
	}
}