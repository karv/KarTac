using System.Collections.Generic;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;
using Microsoft.Xna.Framework;
using KarTac.Skills;
using OpenTK.Input;
using KarTac.Batalla.Objetos;
using Moggle.Screens;
using Moggle.Controles;

namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen : Screen
	{
		public List<UnidadSprite> Unidades { get; private set; }

		public Campo CampoBatalla { get; }

		public BattleScreen (Moggle.Game juego, Campo campo)
			: base (juego)
		{
			CampoBatalla = campo;
			contadorTiempo = new Etiqueta (this);
			contadorTiempo.Texto = () => CampoBatalla.DuraciónBatalla.ToString ("ss\\.fff");
			contadorTiempo.UseFont = @"UnitNameFont";
			contadorTiempo.Color = Color.White;
			contadorTiempo.Posición = new Point (0, 20);
			contadorTiempo.Include ();
			PendingPause = false;

			ManejadorVista = new ManejadorVP ();
			ManejadorVista.ÁreaVisible = new Rectangle (Point.Zero, new Point (GetDisplayMode.Width, GetDisplayMode.Height));
			ManejadorVista.BuenCentroRelTamaño = 0.8f;

			sense = new SensorialExtremos (this);
			sense.AlHacerPresión += ManejadorVista.Mover;
			sense.Include ();
		}

		Etiqueta contadorTiempo;

		SensorialExtremos sense;

		public ManejadorVP ManejadorVista;

		/// <summary>
		/// Devuelve si hay pendiente una pausa del juego.
		/// Cambiado por Pausar()
		/// </summary>
		public bool PendingPause { get; private set; }

		public override Color BgColor
		{
			get
			{
				return Color.Green;
			}
		}

		/// <summary>
		/// Agrega una unidad
		/// </summary>
		/// <returns>Devuelve el control (recién creado) asociado a la unidad</returns>
		[Obsolete ("Usar Campo.Unidades")]
		public UnidadSprite AgregaUnidad (Unidad unit)
		{
			var ret = new UnidadSprite (this, unit);
			ret.Inicializar ();
			ret.Include ();
			return ret;
		}

		protected override void TeclaPresionada (Key key)
		{
			base.TeclaPresionada (key);
			if (key == Key.Escape)
				Pausar ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			// Ejecutar órdenes
			CampoBatalla.Tick (gameTime.ElapsedGameTime);

			if (CampoBatalla.EquipoGanador != null)
			{
				CampoBatalla.Terminar ();
			}

			if (PendingPause)
				doPause ();
		}

		/// <summary>
		/// Pide permiso de pausar
		/// </summary>
		public void Pausar ()
		{
			PendingPause = true;
		}

		/// <summary>
		/// Pausea ahora.
		/// </summary>
		void doPause ()
		{
			var scr = new PauseScreen (Juego, CampoBatalla);
			scr.Ejecutar ();
			PendingPause = false;
		}

		public override void Inicializar ()
		{
			// Crear sprites de unidades
			Unidades = new List<UnidadSprite> (CampoBatalla.Unidades.Count);
			foreach (var pared in CampoBatalla.Paredes)
			{
				var controlPared = new ParedControl (pared, this);
				controlPared.Include ();
			}

			LoadContent ();
			foreach (var u in CampoBatalla.Unidades)
			{
				var sprite = new UnidadSprite (this, u);
				sprite.LoadContent ();
				Unidades.Add (sprite);
				sprite.Include ();
				u.AlSerBlanco += x => HacerDamageInfoString (x);
			}


			base.Inicializar ();
		}

		public VanishingString HacerDamageInfoString (ISkillReturnType sklRet)
		{
			if (sklRet.Color.HasValue)
			{
				var texto = string.Format ("{0:f2}", Math.Abs (sklRet.Delta));

				var mostrarDaño = new VanishingString (Juego, texto, TimeSpan.FromSeconds (1));
				mostrarDaño.LoadContent ();
				mostrarDaño.ColorInicial = sklRet.Color.Value;
				mostrarDaño.Centro = this.ManejadorVista.CampoAPantalla (sklRet.Loc.ToVector2 ());
				mostrarDaño.Include ();
				return mostrarDaño;
			}
			return null;
		}
	}

	/// <summary>
	/// Controla la parte visible del campo
	/// </summary>
	public class ManejadorVP
	{
		/// <summary>
		/// Tamaño
		/// </summary>
		public Point Tamaño
		{
			get
			{
				return ÁreaVisible.Size;
			}
		}

		/// <summary>
		/// Área que se considera un buen lugar, un evento fuera de esta área requiere cenrtrar.
		/// </summary>
		public Rectangle BuenLugarCentro { get; set; }

		/// <summary>
		/// Establece el tamaño del buen centro\n
		/// 1 => BuenLugarCentro == ÁreaVisible\n
		/// 0 => BuenLugarCentro tiene área cero.
		/// </summary>
		public float BuenCentroRelTamaño
		{
			set
			{
				// 1 => 0 
				// 0 => AreaVis / 2
				var tl = ÁreaVisible.Center - new Point ((int)(ÁreaVisible.Size.X * value / 2),
				                                         (int)(ÁreaVisible.Size.Y * value / 2));
				// 0 => 0
				// 1 => AreaVis
				var tamaño = new Point ((int)(ÁreaVisible.Size.X * value), (int)(ÁreaVisible.Size.Y * value));
				BuenLugarCentro = new Rectangle (tl, tamaño);
			}
		}

		/// <summary>
		/// Área del campo que se muestra
		/// </summary>
		public Rectangle ÁreaVisible { get; set; }

		/// <summary>
		/// Centra el área visible en un punto específico
		/// </summary>
		/// <param name="p">Punto del campo doónde centrar</param>
		/// <param name="forzar">Si debe centrar aunque ya esté en el buen lugar</param>
		public void CentrarEn (Point p, bool forzar = false)
		{
			bool centrar = forzar || !BuenLugarCentroRelativo.Contains (p);
			if (centrar)
			{
				var topLeft = new Point (p.X - Tamaño.X / 2, p.Y - Tamaño.Y / 2);
				ÁreaVisible = new Rectangle (topLeft, Tamaño);
			}
		}

		/// <summary>
		/// Devuelve el BuenLugarCentro relativo al campo, NO a pantala
		/// </summary>
		public Rectangle BuenLugarCentroRelativo
		{
			get
			{
				var ret = BuenLugarCentro;
				ret.Location += ÁreaVisible.Location;
				return ret;
			}
		}

		/// <summary>
		/// Convierte un punto relativo de un campo a uno de pantalla
		/// </summary>
		public Point CampoAPantalla (Point p)
		{
			return p - ÁreaVisible.Location;
		}

		/// <summary>
		/// Convierte un punto relativo de pantalla a uno de campo
		/// </summary>
		public Point PantallaACampo (Point p)
		{
			return p + ÁreaVisible.Location;
		}

		/// <summary>
		/// Convierte un punto relativo de un campo a uno de pantalla
		/// </summary>
		public Vector2 CampoAPantalla (Vector2 p)
		{
			return p - ÁreaVisible.Location.ToVector2 ();
		}

		/// <summary>
		/// Convierte un punto relativo de pantalla a uno de campo
		/// </summary>
		public Vector2 PantallaACampo (Vector2 p)
		{
			return p + ÁreaVisible.Location.ToVector2 ();
		}

		/// <summary>
		/// Convierte un rectángulo relativo de un campo a uno de pantalla
		/// </summary>
		public Rectangle CampoAPantalla (Rectangle rect)
		{
			return new Rectangle (CampoAPantalla (rect.Location), rect.Size);
		}

		/// <summary>
		/// Convierte un rectángulo relativo de pantalla a uno de campo
		/// </summary>
		public Rectangle PantallaACampo (Rectangle rect)
		{
			return new Rectangle (PantallaACampo (rect.Location), rect.Size);
		}

		public void Mover (Vector2 dir)
		{
			Point mv = dir.ToPoint ();
			ÁreaVisible = new Rectangle (ÁreaVisible.Left + mv.X,
			                             ÁreaVisible.Top + mv.Y,
			                             ÁreaVisible.Width,
			                             ÁreaVisible.Height);
		}
	}
}