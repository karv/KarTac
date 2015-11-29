using System;
using Microsoft.Xna.Framework.Input;

namespace KarTac.Cliente.Controls.Screens
{
	/// <summary>
	/// Es un IScreen que usa otro IScreen para dibujarse
	/// </summary>
	public abstract class ScreenDial : IScreen
	{
		IScreen screenBase { get; }

		protected ScreenDial (IScreen anterior)
		{
			screenBase = anterior;
		}

		public void Dibujar (Microsoft.Xna.Framework.GameTime gameTime)
		{
			screenBase.Dibujar (gameTime);
		}

		public abstract void LoadContent ();

		public void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			LastKeyboardState = Keyboard.GetState ();
			LastMouseState = Mouse.GetState ();
		}

		public abstract void UnloadContent ();

		public Microsoft.Xna.Framework.Graphics.SpriteBatch GetNewBatch ()
		{
			return screenBase.GetNewBatch ();
		}

		public abstract void Inicializar ();

		public abstract ListaControl Controles { get; }

		public Microsoft.Xna.Framework.Content.ContentManager Content
		{
			get
			{
				return screenBase.Content;
			}
		}

		public MouseState LastMouseState { get; private set; }

		public KeyboardState LastKeyboardState { get; private set; }

		public Microsoft.Xna.Framework.Graphics.SpriteBatch Batch
		{
			get
			{
				return screenBase.Batch;
			}
		}

		public Microsoft.Xna.Framework.Graphics.DisplayMode GetDisplayMode
		{
			get
			{
				return screenBase.GetDisplayMode;
			}
		}
	}
}

