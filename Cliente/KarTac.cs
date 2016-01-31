using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Screens;
using System.IO;
using Moggle.IO;

#if DEBUG
// Para usar Ctrl + Esc = salida rápida en cualquier pantalla
using OpenTK.Input;
#endif

namespace KarTac.Cliente
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class KarTacGame : Moggle.Game
	{
		public const string FileName = "game.sav";

		public KarTacGame ()
		{
			Mouse.ArchivoTextura = @"Icons/arrow-cursor";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			Clan unClan;
			if (File.Exists (FileName))
			{
				unClan = new Clan ();
				unClan.Cargar (FileName);
			}
			else
			{
				unClan = Clan.BuildStartingClan ();
				unClan.Dinero = 100000;
			}

			var scr = new OutsideScreen (this, unClan);
			scr.Inicializar ();
			scr.Ejecutar ();
			base.Initialize ();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			#if DEBUG
			if (InputManager.EstáPresionado (Key.Escape) && InputManager.EstáPresionado (Key.ControlLeft))
			{
				Exit ();
			}
			#endif

			base.Update (gameTime);
		}

	}
}