using System;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public abstract class Equipamento : IEquipamento
	{
		protected Equipamento ()
		{
		}

		public event Action<Personaje> AlEquipar;

		public event Action<Personaje> AlDesequipar;

		public void EquiparEn (Personaje personaje)
		{
			var anterior = Portador;
			Desequipar ();
			portador = personaje;
			portador.Equipamento.Add (this);
			AlEquipar.Invoke (anterior);
		}

		public void Desequipar ()
		{
			var anterior = Portador;
			portador?.Equipamento.Remove (this);
			AlDesequipar?.Invoke (anterior); //TODO: ¿Debe invocarse cuando no tenía dueño?
		}

		public abstract string Nombre { get; }

		Personaje portador;

		public Personaje Portador
		{
			get
			{
				return portador;
			}
			set
			{
				EquiparEn (value);
			}
		}

		public abstract System.Collections.Generic.IEnumerable<string> Tags { get; }
	}
}