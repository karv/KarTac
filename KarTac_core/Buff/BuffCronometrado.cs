using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla.Exp;

namespace KarTac.Buff
{
	public abstract class BuffCronometrado : IBuff
	{
		protected BuffCronometrado (IObjetivo portador)
		{
			Portador = portador;
			Restante = TiempoInicial;
		}

		/// <summary>
		/// Tags de experiencia, típicamente un DictionaryTag
		/// </summary>
		protected abstract ITagging ExpTags { get; }

		ITagging IBuff.ExpTags
		{
			get
			{
				return ExpTags;
			}
		}

		public abstract string Nombre { get; }

		public IObjetivo Portador { get; }

		/// <summary>
		/// Duración del buff
		/// Obs. Se invoca durante el ctor.
		/// </summary>
		public abstract TimeSpan TiempoInicial { get; }

		protected virtual void OnTiempo ()
		{
			Terminar ();
		}

		void IBuff.Terminar ()
		{
			Terminar ();
		}

		void Terminar ()
		{
			throw new NotImplementedException ();
		}

		public void Update (GameTime gameTime)
		{
			if (Restante <= gameTime.ElapsedGameTime)
				OnTiempo ();
			else
				Restante -= gameTime.ElapsedGameTime;
		}

		public TimeSpan Restante { get; private set; }

		public bool Visible
		{
			get
			{
				return true;
			}
		}
	}
}