using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla.Exp;
using KarTac.Batalla;

namespace KarTac.Buff
{
	/// <summary>
	/// Buff que se destruye después de un tiempo determinado.
	/// </summary>
	public abstract class BuffCronometrado : IBuff
	{
		protected BuffCronometrado (IObjetivo portador)
		{
			Portador = portador;
			Restante = TiempoInicial;
		}

		Campo campo
		{
			get
			{
				return Portador.GetCampo;
			}
		}

		Campo IObjetivo.GetCampo
		{
			get
			{
				return campo;
			}
		}

		/// <summary>
		/// Tags de experiencia, típicamente un DictionaryTag
		/// </summary>
		protected abstract ITagging ExpTags { get; }

		public virtual void Insertar ()
		{
			campo.AddObj (this);
		}

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
		public abstract TimeSpan TiempoInicial { get; set; }

		protected virtual void OnTiempo ()
		{
			Terminar ();
		}

		void IBuff.Terminar ()
		{
			Terminar ();
		}

		protected virtual void Terminar ()
		{
			campo.RemObj (this);
		}

		public void Tick (TimeSpan gameTime)
		{
			if (Restante <= gameTime)
				OnTiempo ();
			else
				Restante -= gameTime;
		}

		public void Tick (GameTime gameTime)
		{
			Tick (gameTime.ElapsedGameTime);
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