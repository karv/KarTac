using KarTac.Personajes;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using System;
using OpenTK.Input;
using KarTac.Cliente.Controls;
using KarTac.Equipamento;

namespace KarTac.Cliente.Controls.Screens
{
	public class OutsideScreen: Screen
	{
		public Clan MyClan { get; }

		public OutsideScreen (KarTacGame juego, Clan clan)
			: base (juego)
		{
			MyClan = clan;

			personajes = new Lista<Personaje> (this);
			personajes.Stringificación = x => x.Nombre + " HP: " + x.Atributos.HP.Max;
			personajes.Bounds = new Rectangle (10, 10, 400, 300);
			botónIniciar = new Botón (this, new Rectangle (500, 30, 30, 30));
			botónIniciar.Textura = @"Icons/sword";

			botónGuardar = new Botón (this, new Rectangle (535, 30, 30, 30));
			botónGuardar.Textura = @"Icons/guardar"; 
			botónGuardar.Color = Color.Yellow;

			botónRenombrar = new Botón (this, new Rectangle (570, 30, 30, 30));
			botónRenombrar.Color = Color.Yellow;
			botónRenombrar.Textura = @"Icons/rename";

			botónSalir = new Botón (this, new Rectangle (605, 30, 30, 30));
			botónSalir.Color = Color.Yellow;
			botónSalir.Textura = @"Icons/salir";

			botónEquip = new Botón (this, new Rectangle (640, 30, 30, 30));
			botónEquip.Color = Color.Yellow;
			botónEquip.Textura = @"Icons/equipar";

			botónTienda = new Botón (this, new Rectangle (675, 30, 30, 30));
			botónTienda.Color = Color.Yellow;
			botónTienda.Textura = @"Icons/tienda";

			recargar ();
			personajes.Include ();
			botónIniciar.Include ();
			botónGuardar.Include ();
			botónRenombrar.Include ();
			botónSalir.Include ();
			botónEquip.Include ();
			botónTienda.Include ();

			botónIniciar.AlClick += iniciarCombate;
			botónGuardar.AlClick += guardarClan;
			botónRenombrar.AlClick += delegate // Renombrar
			{
				var dial = new ScreenRenombrarDial (juego, this);
				dial.TextoPreg = "Renombrar a " + personajes.ObjetoEnCursor.Nombre;
				dial.AlTerminar += delegate
				{
					var ind = personajes.CursorIndex;
					personajes.Objetos [ind].Objeto.Nombre = dial.Texto;
				};

				dial.Ejecutar ();
			};
			botónSalir.AlClick += SalirJuego;
			botónEquip.AlClick += delegate // Ventana equipo
			{
				var scr = new EquipScreen (Game, MyClan, personajes.ObjetoEnCursor);
				scr.Ejecutar ();
			};

			botónTienda.AlClick += AbrirTienda;
		}

		void AbrirTienda ()
		{
			var t = new Tienda ();
			t.Artículos.Add (new Tienda.Entrada (() => new Arco (), 10, 110, "Arco corto"));
			t.Artículos.Add (new Tienda.Entrada (() => new EqEspada (), 10, 800, "Espada"));
			t.Artículos.Add (new Tienda.Entrada (() => new HpPoción (), 10, 50, "Poción"));
			var scr = new TiendaScreen (Game, t, MyClan.Inventario);
			scr.Ejecutar ();
		}

		void SalirJuego ()
		{
			Game.Exit ();
		}

		void guardarClan ()
		{
			MyClan.Guardar (KarTacGame.FileName);
			var msgGuardar = new VanishingString (this, "Guardado", TimeSpan.FromSeconds (2));
			msgGuardar.LoadContent ();
			msgGuardar.Inicializar ();
			msgGuardar.Centro = Ratón.Pos.ToVector2 ();
			msgGuardar.ColorInicial = Color.Red;
			msgGuardar.Include ();
		}

		public bool Autoguardado { get; set; }

		public override Color BgColor
		{
			get
			{
				return Color.BlueViolet;
			}
		}

		void iniciarCombate ()
		{
			var r = new Random ();
			var campoBatalla = new Campo (new Point (GetDisplayMode.Width, GetDisplayMode.Height));

			var btScr = new BattleScreen (Game, campoBatalla);
			Game.CurrentScreen = btScr;

			campoBatalla.AlTerminar += delegate
			{
				MyClan.Reestablecer ();
				recargar ();
				Game.CurrentScreen = this;
				btScr.UnloadContent ();
			};

			var ClanEnemigo = Clan.BuildStartingClan (8);
			var equipoRojo = new Equipo (0, Color.Red, MyClan.Inventario);
			var equipoAmarillo = new Equipo (1, Color.Yellow, ClanEnemigo.Inventario);

			// Asignar a todas las unidades del clan al equipo rojo
			foreach (var u in MyClan.Personajes)
			{
				var unid = u.ConstruirUnidad (campoBatalla);
				unid.Equipo = equipoRojo;
				campoBatalla.AñadirUnidad (unid);

				unid.Interactor = new InteracciónHumano (unid, Game);

			}

			// Asignar a todas las unidades del clan enemigo al equipo amarillo
			foreach (var u in ClanEnemigo.Personajes)
			{
				var unid = u.ConstruirUnidad (campoBatalla);
				unid.Equipo = equipoAmarillo;
				campoBatalla.AñadirUnidad (unid);

				unid.Interactor = new IA.AIMeléBásico (unid);
			}

			// Asignar posiciones
			foreach (var u in campoBatalla.Unidades)
			{
				u.PersonajeBase.Atributos.Inicializar ();
				u.PosPrecisa = randomPointInRectangle (campoBatalla.Área, r);
			}

			btScr.Inicializar ();
		}

		static Vector2 randomPointInRectangle (Rectangle rect, Random r)
		{
			return new Vector2 (
				rect.Left + (float)r.NextDouble () * rect.Width,
				rect.Top + (float)r.NextDouble () * rect.Height
			);
		}

		void recargar ()
		{
			personajes.Objetos.Clear ();
			foreach (var x in MyClan.Personajes)
			{
				personajes.Objetos.Add (new Lista<Personaje>.Entrada (x));
			}
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InputManager.FuePresionado (Key.Enter))
				iniciarCombate ();
		}

		Lista<Personaje> personajes { get; }

		Botón botónIniciar { get; }

		Botón botónGuardar { get; }

		Botón botónRenombrar { get; }

		Botón botónSalir { get; }

		Botón botónEquip { get; }

		Botón botónTienda { get; }
	}
}