using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KarTac.Controls.Screens;
using MonoGame.Extended.BitmapFonts;
using System;
using KarTac.Skills;
using Moggle.Controles;
using Moggle.Screens;

namespace KarTac.Controls
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
			skillsList = new ListaSkills (screen);
			skillsList.TipoOrden = ContenedorBotón.TipoOrdenEnum.ColumnaPrimero;
			display = new MultiEtiqueta (screen);
			descripDisplay = new Etiqueta (screen);
			descripDisplay.UseFont = "UnitNameFont";
			descripDisplay.Texto = () => string.Format ("{0}\n\nExp: {1}",
			                                            SkillSeleccionado.Descripción,
			                                            SkillSeleccionado.TotalExp);
		}

		public TimeSpan TiempoCambioLado;

		public ISkill SkillSeleccionado
		{
			get
			{
				return skillsList.SkillSeleccionado;
			}
		}

		/// <summary>
		/// El control que se muestra bajo el nombre para mostrar atributos y recursos
		/// </summary>
		Etiqueta descripDisplay;

		bool inicializado;

		void actualizaDesc ()
		{
			descripDisplay.Texto = () => SkillSeleccionado.Descripción;
		}

		public int ÍndiceSkillSel
		{
			get
			{
				return skillsList.ÍndiceSkillSel;
			}
			set
			{
				skillsList.ÍndiceSkillSel = value;
			}
		}

		public override void Inicializar ()
		{
			LoadContent ();
			skillsList.Populate (UnidadActual.PersonajeBase);

			descripDisplay.Inicializar ();

			display.Inicializar ();
			ÍndiceSkillSel = 0;
			reposicionarControles ();

			display.NumEntradasMostrar = 4;
			display.TiempoEntreCambios = TimeSpan.FromMilliseconds (1500);
			var font = Screen.Content.Load<BitmapFont> ("fonts");
			foreach (var x in UnidadActual.AtributosActuales.GetModifiedAtribs())
			{
				if (x.Key.VisibleBatalla)
					display.Mostrables.Add (new MultiEtiqueta.IconTextEntry (
						font,
						Screen.Content.Load<Texture2D> (x.Key.Icono),
						string.Format ("{0}: {1}", x.Key.Nombre, x.Value),
						Color.Green * 0.8f,
						Color.White
					));
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

			descripDisplay.Posición = new Point (
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
					skillsList.Populate (UnidadActual.PersonajeBase);
			}
		}


		public int TamañoY = 200;

		Texture2D textura;
		BitmapFont InfoFont;
		public Color BgColor = Color.Blue * 0.4f;

		ListaSkills skillsList { get; }

		MultiEtiqueta display { get; }

		public override void LoadContent ()
		{
			textura = Screen.Content.Load<Texture2D> ("Rect");
			InfoFont = Screen.Content.Load<BitmapFont> ("fonts");
			skillsList.LoadContent ();
			display.LoadContent ();
			descripDisplay.LoadContent ();
		}

		public void Unload ()
		{
			skillsList.Clear ();
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