using KarTac.Batalla;
using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace KarTac.Recursos
{
	public class AtributoGenérico : IRecurso
	{
		public AtributoGenérico (string nombre, bool visibleBatalla)
		{
			_nombre = nombre;
			VisibleBatalla = visibleBatalla;
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
				#if DEBUG
				if (_valor < 0)
					Debug.WriteLine (string.Format ("{0} con valor {1}", Nombre, _valor));
				#endif
				AlCambiarValor?.Invoke ();
			}
		}

		public bool VisibleBatalla { get; set; }

		public float Inicial;

		public void CommitExp (double exp)
		{
			Inicial += (float)exp * CommitExpCoef;
		}

		public float CommitExpCoef = 1;

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

		public void Tick (TimeSpan delta)
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
			writer.Write (VisibleBatalla);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Inicial = reader.ReadSingle ();
			VisibleBatalla = reader.ReadBoolean ();
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