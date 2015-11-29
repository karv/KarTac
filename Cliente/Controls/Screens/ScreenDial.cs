using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Es un IScreen que usa otro IScreen para dibujarse
	/// </summary>
	public abstract class ScreenDial : IScreen
	{
		protected IScreen ScreenBase { get; }

		protected ScreenDial (IScreen anterior)
		{
			ScreenBase = anterior;
		}

		public void Dibujar (Microsoft.Xna.Framework.GameTime gameTime)
		{
			ScreenBase.Dibujar (gameTime);
		}

		public virtual void LoadContent ()
		{
			foreach (var x in Controles)
			{
				x.LoadContent ();
			}
		}

		public void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			LastKeyboardState = Keyboard.GetState ();
			LastMouseState = Mouse.GetState ();
		}

		public virtual void UnloadContent ()
		{
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

		public SpriteBatch Batch
		{
			get
			{
				return ScreenBase.Batch;
			}
		}

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
	}
}

