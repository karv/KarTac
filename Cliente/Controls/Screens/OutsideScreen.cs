using KarTac.Personajes;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using System;
using OpenTK.Input;
using KarTac.Cliente.Controls;
using KarTac.Equipamento;
using KarTac.Batalla.Generador;
using KarTac.Batalla.Objetos;
using Moggle.Screens;
using Moggle.Controles.Listas;
using Moggle.Controles;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Pantalla entre turnos.
	/// Note que esta pantalla nunca se libera.
	/// </summary>
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
				var dial = new RenombrarDialScreen (juego, this);
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
				var scr = new EquipScreen (Juego, MyClan, personajes.ObjetoEnCursor);
				scr.Ejecutar ();
			};

			botónTienda.AlClick += AbrirTienda;
		}

		void AbrirTienda ()
		{
			var t = new Tienda ();
			t.Artículos.Add (new Tienda.Entrada (() => new Arco (), 10, 110, "Arco corto"));
			t.Artículos.Add (new Tienda.Entrada (() => new EqEspada (), 10, 150, "Espada"));
			t.Artículos.Add (new Tienda.Entrada (() => new HpPoción (), 10, 50, "Poción"));
			t.Artículos.Add (new Tienda.Entrada (() => new ArmaduraCuero (), 10, 120, "Armadura"));
			t.Artículos.Add (new Tienda.Entrada (() => new CascoCuero (), 10, 120, "Casco"));
			t.Artículos.Add (new Tienda.Entrada (() => new Bastón (), 10, 125, "Bastón"));
			t.Artículos.Add (new Tienda.Entrada (() => new Hacha (), 10, 125, "Hacha"));
			t.Artículos.Add (new Tienda.Entrada (() => new Lanza (), 10, 125, "Lanza"));
			var scr = new TiendaScreen (Juego, t, MyClan.Inventario);
			scr.Ejecutar ();
		}

		void SalirJuego ()
		{
			Juego.Exit ();
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
			personajes.InterceptarTeclado = false;
			var r = Utils.Rnd;
			//var campoBatalla = new Campo (new Point (GetDisplayMode.Width, GetDisplayMode.Height));
			var campoBatalla = new Campo (new Point (Utils.Rnd.Next (100, 1000), Utils.Rnd.Next (100, 1000)));
			Clan enemClan;
			for (int i = 0; i < 2; i++)
			{
				var p = new Pared (campoBatalla.Área.GetRandomPoint ().ToVector2 (),
				                   campoBatalla.Área.GetRandomPoint ().ToVector2 ());
				p.ImportanciaCoef = 0.2f;
				campoBatalla.Paredes.Add (p);
			}

			enemClan = Clan.BuildStartingClan ();

			MyClan.Reestablecer ();

			var btScr = new BattleScreen (Juego, campoBatalla);
			btScr.Ejecutar ();

			campoBatalla.AlTerminar += delegate
			{
				MyClan.Reestablecer ();
				recargar ();
				Ejecutar ();
				btScr.UnloadContent ();
				personajes.InterceptarTeclado = true;
			};

			//var ClanEnemigo = Clan.BuildStartingClan ();
			var equipoRojo = new Equipo (0, Color.Red, MyClan.Inventario);
			var equipoAmarillo = new Equipo (1, Color.Yellow, enemClan.Inventario);
			var enemigo = GeneradorCombates.GenerarEquipoAleatorio (
				              MyClan.TotalExp, 
				              campoBatalla,
				              GeneradorUnidad.Melee,
				              GeneradorUnidad.Melee,
				              GeneradorUnidad.Melee
			              );

			// Asignar a todas las unidades del clan al equipo rojo
			foreach (var u in MyClan.Personajes)
			{
				var unid = u.ConstruirUnidad (campoBatalla);
				unid.Equipo = equipoRojo;
				campoBatalla.AñadirUnidad (unid);

				unid.Interactor = new InteracciónHumano (unid, Juego);

			}

			// Asignar a todas las unidades del clan enemigo al equipo amarillo
			foreach (var u in enemigo)
			{
				u.Equipo = equipoAmarillo;
				u.AtributosActuales.Inicializar ();
			}
			// Asignar posiciones
			foreach (var u in campoBatalla.Unidades)
			{
				u.PosPrecisa = randomPointInRectangle (campoBatalla.Área, r);
			}

			foreach (var u in campoBatalla.Unidades)
			{
				if (u != u.PersonajeBase.Unidad)
					Console.WriteLine ();
			}


			btScr.Inicializar ();

		}

		public static void MostrarAtrs (string atr, Campo c)
		{
			foreach (var x in c.Unidades)
			{
				Console.WriteLine (string.Format ("{0} - {1}", x.PersonajeBase.Nombre, x.AtributosActuales.Recs [atr].ToString ()));
			}
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

		protected override void TeclaPresionada (Key key)
		{
			base.TeclaPresionada (key);
			if (key == Key.Enter)
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