using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;
using MonoGame.Extended.BitmapFonts;
using System;
using KarTac.Skills;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// El menú "en pausa"
	/// </summary>
	public class MenúTurno : SBC
	{
		public MenúTurno (IScreen screen)
			: base (screen)
		{
			Prioridad = -20;
			skillsList = new ContenedorBotón (screen);
			skillsList.TipoOrden = ContenedorBotón.TipoOrdenEnum.ColumnaPrimero;
			display = new RandomStringDisplay (screen);
		}

		int índiceSkillSel;

		public int ÍndiceSkillSel
		{
			get{ return índiceSkillSel; }
			set
			{
				var nuevoInd = Math.Min (Math.Max (value, 0), skillsList.Count - 1);
				skillsList.BotónEnÍndice (ÍndiceSkillSel).Color = skillNoSelColor;
				índiceSkillSel = nuevoInd;
				skillsList.BotónEnÍndice (ÍndiceSkillSel).Color = skillSelColor;
			}
		}

		public ISkill SkillSeleccionado
		{
			get
			{
				return UnidadActual.PersonajeBase.Skills [ÍndiceSkillSel];
			}
		}

		static Color skillNoSelColor
		{
			get{ return Color.Red; }
		}

		static Color skillSelColor
		{
			get{ return Color.Green; }
		}

		public override void Inicializar ()
		{
			skillsList.Posición = new Point (
				GetBounds ().Right - skillsList.GetBounds ().Width - 50,
				GetBounds ().Bottom - skillsList.GetBounds ().Height - 50
			);
			display.Pos = new Vector2 (20, GetBounds ().Top + 50);
			display.Color = Color.Green * 0.8f;

			display.Inicializar ();

			foreach (var x in UnidadActual.AtributosActuales.Recs)
			{
				display.Mostrables.Add (x.ToString ());
			}

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
				skillsList.Clear ();

				foreach (var sk in UnidadActual.PersonajeBase.Skills)
				{
					
					var bt = skillsList.Add ();
					bt.Textura = sk.IconTextureName;
				}

			}
		}

		public int TamañoY = 200;

		Texture2D textura;
		BitmapFont InfoFont;
		public Color BgColor = Color.Blue;

		ContenedorBotón skillsList { get; }

		RandomStringDisplay display { get; }

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
			InfoFont = Screen.Content.Load<BitmapFont> ("fonts");
			skillsList.LoadContent ();
			display.LoadContent ();
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

				skillsList.Dibujar (gameTime);

				display.Dibujar (gameTime);
			}
			bat.End ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			display.Update (gameTime);
		}

		public override Rectangle GetBounds ()
		{
			return new Rectangle (
				0, 
				DibujarAbajo ? Screen.GetDisplayMode.Height - TamañoY : 0,
				Screen.GetDisplayMode.Width,
				TamañoY);
		}

		/// <summary>
		/// Devuelve si el control debe dibujarse en la parte baja de la pantalla
		/// </summary>
		/// <value><c>true</c> if dibujar abajo; otherwise, <c>false</c>.</value>
		public bool DibujarAbajo
		{
			get
			{
				return UnidadActual.Pos.Y < Screen.GetDisplayMode.Height - TamañoY - 50;
			}
		}
	}
}