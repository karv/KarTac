﻿using KarTac.Batalla;
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

		public KarTac.Personajes.AtributosPersonaje ConjAtrib { get; set; }

		public float Valor
		{
			get
			{
				return Inicial;
			}
			set
			{
				Inicial = value;
				#if DEBUG
				if (Valor < 0)
					Debug.WriteLine (string.Format ("{0} con valor {1}", Nombre, Valor));
				#endif
				AlCambiarValor?.Invoke ();
			}
		}

		public void AcumularExp (double exp)
		{
			PeticiónExpAcumulada += exp;
		}

		public void ResetExp ()
		{
			PeticiónExpAcumulada = 0;
		}

		public string Icono
		{
			get
			{
				return @"Icons/Recursos/etc"; 
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