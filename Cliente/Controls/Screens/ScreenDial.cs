using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Es un IScreen que usa otro IScreen para dibujarse
	/// </summary>
	public abstract class ScreenDial : IScreen
	{
		protected IScreen ScreenBase { get; }

		protected KarTacGame Juego { get; }

		protected ScreenDial (IScreen anterior, KarTacGame juego)
		{
			ScreenBase = anterior;
			Juego = juego;
		}

		public void Dibujar (Microsoft.Xna.Framework.GameTime gameTime)
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

		public virtual void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			foreach (var x in Controles)
			{
				x.Update (gameTime);
			}
			LastKeyboardState = Keyboard.GetState ();
			LastMouseState = Mouse.GetState ();
		}

		public virtual void UnloadContent ()
		{
		}

		public void Ejecutar ()
		{
			Inicializar ();
			LoadContent ();
			Juego.CurrentScreen = this;
		}

		public void Salir ()
		{
			Juego.CurrentScreen = ScreenBase;
			AlTerminar?.Invoke ();
			UnloadContent ();
		}


		public SpriteBatch GetNewBatch ()
		{
			return ScreenBase.GetNewBatch ();
		}

		public abstract void Inicializar ();

		public abstract ListaControl Controles { get; }

		public Microsoft.Xna.Framework.Content.ContentManager Content
		{
			get
			{
				return ScreenBase.Content;
			}
		}

		public MouseState LastMouseState { get; private set; }

		public KeyboardState LastKeyboardState { get; private set; }

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

		public event Action AlTerminar;
	}
}