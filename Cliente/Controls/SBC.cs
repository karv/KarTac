﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KarTac.Cliente.Controls.Screens;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IControl
	{
		public IScreen Screen { get; }

		protected SBC (IScreen screen)
		{
			Screen = screen;
		}

		public int Prioridad { get; set; }

		public virtual void Include ()
		{
			Screen.Controles.Add (this);
		}

		public virtual void Exclude ()
		{
			Screen.Controles.Remove (this);
		}

		public virtual void Inicializar ()
		{
		}

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
			CheckMouseState (gameTime.ElapsedGameTime);
		}

		public abstract Rectangle GetBounds ();

		/// <summary>
		/// Se ejecuta cada llamada a game.Update 
		/// </summary>
		public virtual void CheckMouseState (TimeSpan time)
		{
			if (MouseOver)
			{
				var state = Mouse.GetState ();
				if (state.LeftButton == ButtonState.Pressed)
					AlPresionalMouse?.Invoke (state);

				if (InputManager.FuePresionado (OpenTK.Input.MouseButton.Left))
					AlClick?.Invoke ();

				TiempoMouseOver += time;
			}
			else
			{
				TiempoMouseOver = TimeSpan.Zero;
			}
		}

		public bool MouseOver
		{
			get
			{
				var state = Mouse.GetState ();
				return GetBounds ().Contains (state.Position);
			}
		}

		public TimeSpan TiempoMouseOver { get; private set; }

		public event Action<MouseState> AlPresionalMouse;
		public event Action AlClick;
	}
}