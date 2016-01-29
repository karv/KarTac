using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
			if (DibujarBase)
				ScreenBase.Dibujar (gameTime);

			Batch = GetNewBatch ();
			Batch.Begin ();
			Dibujar (gameTime, Batch);
			foreach (var x in Controles)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();

		}

		public virtual void Dibujar (GameTime gametime, SpriteBatch bat)
		{
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
			foreach (var x in new List<IControl>(Controles))
			{
				x.Exclude ();
				x.Dispose ();
			}
		}

		public virtual void Ejecutar ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + this);
			#endif
			Juego.CurrentScreen.Escuchando = false;
			Inicializar ();
			LoadContent ();
			Juego.CurrentScreen = this;
			Escuchando = true;
		}

		public bool Escuchando
		{
			set
			{
				if (value)
					InputManager.AlSerActivado += EscuchadorTeclado;
				else
					InputManager.AlSerActivado -= EscuchadorTeclado;
			}
		}

		public virtual void Salir ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + ScreenBase);
			#endif
			Escuchando = false;
			Juego.CurrentScreen = ScreenBase;
			Juego.CurrentScreen.Escuchando = true;
			AlTerminar?.Invoke ();

			UnloadContent ();
		}

		public SpriteBatch GetNewBatch ()
		{
			return ScreenBase.GetNewBatch ();
		}

		public virtual void Inicializar ()
		{
			foreach (var x in Controles)
			{
				x.Inicializar ();
			}
		}

		public virtual void EscuchadorTeclado (OpenTK.Input.Key key)
		{
			foreach (var x in Controles)
			{
				x.CatchKey (key);
			}
		}

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

		public abstract bool DibujarBase { get; }

		public event Action AlTerminar;
	}
}