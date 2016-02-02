using KarTac.Recursos;
using System.Collections.Generic;

namespace KarTac.Recursos
{
	public interface IMultiRecurso : IRecurso
	{
		ICollection<IRecurso> SubRecursos { get; }
	}

	public class MultiRecurso : IMultiRecurso
	{
		public List<IRecurso> Recursos = new List<IRecurso> ();

		event System.Action IRecurso.AlCambiarValor
		{
			add{}
			remove{}
		}

		public void Tick (System.TimeSpan delta)
		{
			foreach (var x in Recursos)
			{
				x.Tick (delta);
			}
		}

		public void PedirExp (System.TimeSpan time, KarTac.Batalla.Campo campo)
		{
			foreach (var x in Recursos)
			{
				x.PedirExp (time, campo);
			}
		}

		public void Reestablecer ()
		{
			foreach (var x in Recursos)
			{
				x.Reestablecer ();
			}
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write ("Multi");
			IO.IOComún.Guardar (Recursos, writer);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			Recursos.Clear ();
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; i++)
			{
				var ad = Lector.Cargar (reader);
				var hp = ad as HP;
				if (hp != null)
					hp.RecBase = this;
				Recursos.Add (ad);
			}
		}

		public void CommitExp (double exp)
		{
			foreach (var x in Recursos)
			{
				x.CommitExp (exp);
			}
		}

		public void AcumularExp (double exp)
		{
			throw new System.Exception ("Éste no se usa así");
		}

		public void ResetExp ()
		{
			foreach (var x in Recursos)
			{
				x.ResetExp ();
			}
		}

		ICollection<IRecurso> IMultiRecurso.SubRecursos
		{
			get
			{
				return Recursos;
			}
		}

		public string Nombre
		{
			get
			{
				return Recursos [0].Nombre;
			}
		}

		public float Valor
		{
			get
			{
				return Recursos [0].Valor;
			}
			set
			{
				Recursos [0].Valor = value;
			}
		}

		public bool VisibleBatalla
		{
			get
			{
				return Recursos [0].VisibleBatalla;
			}
		}

		public string Icono
		{
			get
			{
				return Recursos [0].Icono;
			}
		}

		public KarTac.Personajes.AtributosPersonaje ConjAtrib
		{
			get
			{
				return Recursos [0].ConjAtrib;
			}
			set
			{
				foreach (var x in Recursos)
				{
					x.ConjAtrib = value;
				}
			}
		}

		public Microsoft.Xna.Framework.Color? ColorMostrarGanado
		{
			get
			{
				return Recursos [0].ColorMostrarGanado;
			}
		}

		public Microsoft.Xna.Framework.Color? ColorMostrarPerdido
		{
			get
			{
				return Recursos [0].ColorMostrarPerdido;
			}
		}

		public double PeticiónExpAcumulada
		{
			get
			{
				double ret = 0;
				foreach (var x in Recursos)
				{
					ret += x.PeticiónExpAcumulada;
				}
				return ret;
			}
		}
	}
}