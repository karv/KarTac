using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KarTac.Cliente.Controls.Screens;
using OpenTK.Input;

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
				if (InputManager.FuePresionado (MouseButton.Left))
					AlClick?.Invoke ();

				if (InputManager.FuePresionado (MouseButton.Right))
					AlClickDerecho?.Invoke ();

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

		void IDisposable.Dispose ()
		{
			Dispose ();
		}

		/// <summary>
		/// Releases all resource used by the <see cref="KarTac.Cliente.Controls.SBC"/> object.
		/// Liberar cada textura.
		/// </summary>
		protected virtual void Dispose ()
		{
			Exclude ();
		}

		public event Action AlClick;
		public event Action AlClickDerecho;
	}
}