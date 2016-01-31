using KarTac.Batalla.Objetos;
using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controles;
using Moggle.Screens;
using Moggle;

namespace KarTac.Cliente.Controls
{
	public class ParedControl : SBC
	{
		public ParedControl (Pared pared, IScreen scr)
			: base (scr)
		{
			Pared = pared;
		}

		public Pared Pared { get; }

		Texture2D textura { get; set; }

		ManejadorVP VP
		{
			get
			{
				return (Screen as BattleScreen).ManejadorVista;
			}
		}

		public override void Dibujar (GameTime gameTime)
		{
			Moggle.Primitivos.DrawLine (Screen.Batch,
			                            VP.CampoAPantalla (Pared.P0),
			                            VP.CampoAPantalla (Pared.P1),
			                            Color.Black,
			                            textura);
		}

		protected override void Dispose ()
		{
			textura = null;
		}

		public override Rectangle GetBounds ()
		{
			return Rectangle.Empty;
		}

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
		}
	}
}

