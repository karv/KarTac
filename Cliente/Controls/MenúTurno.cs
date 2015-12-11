﻿using Microsoft.Xna.Framework.Graphics;
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
			descripDisplay = new RandomStringDisplay (screen, "Arial small");
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
				actualizaDesc ();
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

		RandomStringDisplay descripDisplay;

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
			reposicionarControles ();

			display.Color = Color.Green * 0.8f;
			foreach (var x in UnidadActual.AtributosActuales.Recs)
			{
				display.Mostrables.Add (x.ToString ());
			}

			base.Inicializar ();
			actualizaDesc ();
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
		public Color BgColor = Color.Blue;

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
			if (TiempoMouseOver > TimeSpan.FromMilliseconds (1500))
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