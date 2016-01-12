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
			TiempoCambioLado = TimeSpan.FromMilliseconds (700);
			skillsList = new ContenedorBotón (screen);
			skillsList.TipoOrden = ContenedorBotón.TipoOrdenEnum.ColumnaPrimero;
			display = new RandomStringDisplay (screen);
			descripDisplay = new RandomStringDisplay (screen, "UnitNameFont");
		}

		int índiceSkillSel;

		public TimeSpan TiempoCambioLado;

		public int ÍndiceSkillSel
		{
			get{ return índiceSkillSel; }
			set
			{
				var nuevoInd = Math.Min (Math.Max (value, 0), skillsList.Count - 1);
				skillsList.BotónEnÍndice (ÍndiceSkillSel).Color = 
					getSkillColor (false, skillsList.BotónEnÍndice (ÍndiceSkillSel).Habilidato);
				índiceSkillSel = nuevoInd;
				skillsList.BotónEnÍndice (ÍndiceSkillSel).Color = 
					getSkillColor (true, skillsList.BotónEnÍndice (ÍndiceSkillSel).Habilidato);
				actualizaDesc ();
			}
		}

		public void Unload ()
		{
			skillsList.Clear ();
		}

		static Color getSkillColor (bool selected, bool habil)
		{
			var ret = selected ? skillSelColor : skillNoSelColor;
			if (!habil)
				ret = new Color (ret.R / 2 + 128, ret.G / 2 + 128, ret.B / 2 + 128, ret.A);
			//ret += Color.Gray * 0.4f;
			return ret;
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

		/// <summary>
		/// El control que se muestra bajo el nombre para mostrar atributos y recursos
		/// </summary>
		RandomStringDisplay descripDisplay;

		bool inicializado;

		void actualizaDesc ()
		{
			descripDisplay.Mostrables [0] = SkillSeleccionado.Descripción;
		}

		public override void Inicializar ()
		{
			LoadContent ();
			rehacerSkills ();

			descripDisplay.Inicializar ();
			descripDisplay.Mostrables.Add ("");

			display.Inicializar ();
			ÍndiceSkillSel = 0;
			reposicionarControles ();

			display.Color = Color.Green * 0.8f;
			display.NumEntradasMostrar = 4;
			display.TiempoEntreCambios = TimeSpan.FromMilliseconds (1500);
			foreach (var x in UnidadActual.AtributosActuales.Recs.Values)
			{
				if (x.VisibleBatalla)
					display.Mostrables.Add (x.ToString ());
			}

			base.Inicializar ();

			inicializado = true;
		}

		void reposicionarControles ()
		{
			skillsList.Posición = new Point (
				GetBounds ().Right - skillsList.GetBounds ().Width - 50,
				GetBounds ().Bottom - skillsList.GetBounds ().Height - 50
			);
			display.Pos = new Vector2 (20, GetBounds ().Top + 50);

			descripDisplay.Pos = new Vector2 (
				skillsList.GetBounds ().Left - 300,
				skillsList.GetBounds ().Top
			);
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
				if (inicializado)
					rehacerSkills ();
			}
		}

		void rehacerSkills ()
		{
			// Actualizar skills
			skillsList.Clear ();
			var numUsables = 0;

			foreach (var sk in UnidadActual.PersonajeBase.Skills)
			{
				Botón bt;
				if (sk.Usable)
				{
					bt = skillsList.Add (numUsables++);
					bt.Color = Color.Red;
				}
				else
				{
					bt = skillsList.Add ();
					bt.Habilidato = false;
					bt.Color = Color.Gray;
				}
				bt.Textura = sk.IconTextureName;
			}
		}

		public int TamañoY = 200;

		Texture2D textura;
		BitmapFont InfoFont;
		public Color BgColor = Color.Blue * 0.4f;

		ContenedorBotón skillsList { get; }

		RandomStringDisplay display { get; }

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
			InfoFont = Screen.Content.Load<BitmapFont> ("fonts");
			skillsList.LoadContent ();
			display.LoadContent ();
			descripDisplay.LoadContent ();
		}

		protected override void Dispose ()
		{
			textura = null;
			InfoFont = null;
			((IDisposable)skillsList).Dispose ();
			((IDisposable)display).Dispose ();
			((IDisposable)descripDisplay).Dispose ();

			base.Dispose ();
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
				descripDisplay.Dibujar (gameTime);
			}
			bat.End ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			display.Update (gameTime);
			if (TiempoMouseOver > TiempoCambioLado)
			{
				Switched = !Switched;
				reposicionarControles ();
			}
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
				return UnidadActual.Pos.Y < Screen.GetDisplayMode.Height - TamañoY - 50 ^ Switched;
			}
		}

		bool Switched { get; set; }
	}
}