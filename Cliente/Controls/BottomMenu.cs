using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;
using MonoGame.Extended.BitmapFonts;
using System.Text;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// El menú "en pausa"
	/// </summary>
	public class BottomMenu : SBC
	{
		public BottomMenu (IScreen screen)
			: base (screen)
		{
			Prioridad = -20;
			SkillsList = new ContenedorBotón (screen);
		}

		public override void Inicializar ()
		{
			SkillsList.Posición = new Point (
				GetBounds ().Right - SkillsList.GetBounds ().Right - 50,
				GetBounds ().Bottom - SkillsList.GetBounds ().Bottom - 50
			);

			base.Inicializar ();
		}

		KarTac.Batalla.Unidad unidadActual;

		public KarTac.Batalla.Unidad UnidadActual
		{
			get
			{
				return unidadActual;
			}
			set
			{
				unidadActual = value;

				// Actualizar skills
				SkillsList.Clear ();

				foreach (var sk in UnidadActual.PersonajeBase.Skills)
				{
					
					var bt = SkillsList.Add ();
					bt.Textura = sk.IconTextureName;
				}

			}
		}

		public int TamañoY = 200;

		Texture2D textura;
		BitmapFont InfoFont;
		public Color BgColor = Color.Blue;

		ContenedorBotón SkillsList { get; }

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
			InfoFont = Screen.Content.Load<BitmapFont> ("fonts");
			SkillsList.LoadContent ();
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.GetNewBatch ();
			bat.Begin ();
			bat.Draw (textura, GetBounds (), BgColor);

			if (UnidadActual != null)
			{
				string infoStr = string.Format ("Nombre: {0}",
				                                UnidadActual.PersonajeBase.Nombre);
				bat.DrawString (InfoFont,
				                infoStr,
				                new Vector2 (20, GetBounds ().Top + 20),
				                Color.Red * 0.8f);

				SkillsList.Dibujar (gameTime);
			}
			bat.End ();
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (0, Screen.GetDisplayMode.Height - TamañoY,
			                      Screen.GetDisplayMode.Width, TamañoY);
		}
	}
}