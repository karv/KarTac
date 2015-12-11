using KarTac.Personajes;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using System;

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
			personajes.Stringificación = x => x.Nombre + " " + x.Atributos.HP;
			personajes.Bounds = new Rectangle (10, 10, 400, 300);
			iniciar = new Botón (this, new Rectangle (500, 30, 30, 30));
			iniciar.Textura = "Rect";

			recargar ();
			personajes.Include ();
			iniciar.Include ();

			iniciar.AlClick += iniciarCombate;
		}

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
			campoBatalla.SelectorTarget = new Selector (Game, btScr);
			Game.CurrentScreen = btScr;

			btScr.AlTerminarBatalla += delegate
			{
				MyClan.Reestablecer ();
				recargar ();
				Game.CurrentScreen = this;
			};

			var ClanEnemigo = Clan.BuildStartingClan ();
			var equipoRojo = new Equipo (0, Color.Red);
			var equipoAmarillo = new Equipo (1, Color.Yellow);

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

				unid.Interactor = new InteracciónHumano (unid, Game);
			}

			// Asignar posiciones
			foreach (var u in campoBatalla.Unidades)
			{
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

		Lista<Personaje> personajes { get; }

		Botón iniciar { get; }
	}
}