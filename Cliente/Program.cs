#region Using Statements
using System;

#endregion

namespace KarTac.Cliente
{
	static class Program
	{
		static KarTacGame game;

		internal static void RunGame ()
		{
			game = new KarTacGame ();
			game.Run ();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main (string[] args)
		{
			RunGame ();
		}
	}
}