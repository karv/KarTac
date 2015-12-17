using KarTac.Batalla;
using System;
using Microsoft.Xna.Framework;

namespace KarTac.Recursos
{
	public class AtributoGenérico : IRecurso
	{
		public AtributoGenérico (string nombre)
		{
			_nombre = nombre;
		}

		float _valor;

		public float Valor
		{
			get
			{
				return _valor;
			}
			set
			{
				_valor = value;
				AlCambiarValor?.Invoke ();
			}
		}

		public float Inicial;

		public void CommitExp (double exp)
		{
			Valor += (float)exp;
		}

		string _nombre;

		public string Nombre
		{
			get
			{
				return _nombre;
			}
		}

		public void PedirExp (TimeSpan time, Campo campo)
		{
		}

		public event Action AlCambiarValor;

		public void Tick (GameTime delta)
		{
		}

		public void Reestablecer ()
		{
			Valor = Inicial;
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (Nombre);
			writer.Write (Inicial);
			// No es necesario escribir valor, no queremos guardar el estado de una batalla.
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Inicial = reader.ReadSingle ();
		}

		public Color? ColorMostrarGanado
		{
			get
			{
				return null;
			}
		}

		public Color? ColorMostrarPerdido
		{
			get
			{
				return null;
			}
		}

		public double PeticiónExpAcumulada { get; set; }

		public override string ToString ()
		{
			return string.Format ("{0}: {1}", Nombre, Valor);
		}

	}
}