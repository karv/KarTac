using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IControl
	{
		
		protected SBC (KarTacGame juego)
		{
			GameBase = juego;
		}

		public int Prioridad { get; set; }

		public virtual void Include ()
		{
			GameBase.Controles.Add (this);
		}

		public virtual void Exclude ()
		{
			GameBase.Controles.Remove (this);
		}

		public KarTacGame Game
		{
			get
			{
				return GameBase;
			}
		}

		public virtual void Initialize ()
		{
		}

		protected KarTacGame GameBase { get; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		public abstract void Dibujar (GameTime gameTime);

		/// <summary>
		/// Loads the content.
		/// </summary>
		public abstract void LoadContent ();

		public virtual void Update (GameTime gameTime)
		{
			CheckMouseState ();
		}

		public virtual Rectangle Bounds { get; protected set; }

		/// <summary>
		/// Se ejecuta cada llamada a game.Update 
		/// </summary>
		public virtual void CheckMouseState ()
		{
			if (MouseOver)
			{
				var state = Mouse.GetState ();
				if (state.LeftButton == ButtonState.Pressed)
					AlPresionalMouse?.Invoke (state);

				if (GameBase.LastMouseState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
					AlClick?.Invoke ();
			}
		}

		public bool MouseOver
		{
			get
			{
				var state = Mouse.GetState ();
				return Bounds.Contains (state.Position);
			}
		}


		public event Action<MouseState> AlPresionalMouse;
		public event Action AlClick;
	}
}

