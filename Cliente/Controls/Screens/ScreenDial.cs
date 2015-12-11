using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Es un IScreen que usa otro IScreen para dibujarse
	/// </summary>
	public abstract class ScreenDial : IScreen
	{
		protected IScreen ScreenBase { get; }

		protected KarTacGame Juego { get; }

		protected ScreenDial (KarTacGame juego)
			: this (juego, juego.CurrentScreen)
		{
		}

		protected ScreenDial (KarTacGame juego, IScreen baseScreen)
		{
			ScreenBase = baseScreen;
			Juego = juego;
			Controles = new ListaControl ();
		}

		public virtual Color BgColor
		{
			get
			{
				return ScreenBase.BgColor;
			}
		}

		public virtual void Dibujar (GameTime gameTime)
		{
			ScreenBase.Dibujar (gameTime);

			Batch = GetNewBatch ();
			Batch.Begin ();
			foreach (var x in Controles)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();

		}

		public virtual void LoadContent ()
		{
			foreach (var x in Controles)
			{
				x.LoadContent ();
			}
		}

		public virtual void Update (GameTime gameTime)
		{
			foreach (var x in Controles)
			{
				x.Update (gameTime);
			}
		}

		public virtual void UnloadContent ()
		{
		}

		public void Ejecutar ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + this);
			#endif
			Inicializar ();
			LoadContent ();
			Juego.CurrentScreen = this;
		}

		public void Salir ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + ScreenBase);
			#endif
			Juego.CurrentScreen = ScreenBase;
			AlTerminar?.Invoke ();
			UnloadContent ();
		}

		public SpriteBatch GetNewBatch ()
		{
			return ScreenBase.GetNewBatch ();
		}

		public abstract void Inicializar ();

		public ListaControl Controles { get; }

		public Microsoft.Xna.Framework.Content.ContentManager Content
		{
			get
			{
				return ScreenBase.Content;
			}
		}

		public SpriteBatch Batch { get; private set; }

		public DisplayMode GetDisplayMode
		{
			get
			{
				return ScreenBase.GetDisplayMode;
			}
		}

		public GraphicsDevice Device
		{
			get
			{
				return ScreenBase.Device;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[{0}]\nAnterior: {1}", GetType (), ScreenBase);
		}

		public event Action AlTerminar;
	}
}