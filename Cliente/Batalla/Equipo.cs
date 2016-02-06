using Microsoft.Xna.Framework;
using KarTac.Equipamento;
using System;

namespace KarTac.Batalla
{
	public struct Equipo : IEquipoSelector, IEquatable<Equipo>
	{
		public Equipo (int equipoRef, Color flagColor, InventarioClan drops)
		{
			EquipoRef = equipoRef;
			FlagColor = flagColor;
			Drops = drops;
		}

		public int EquipoRef { get; }

		public bool EsAliado (Unidad unidad)
		{
			return unidad.Equipo.EquipoRef == EquipoRef;
		}

		public bool EsEnemigo (Unidad unidad)
		{
			return unidad.Equipo.EquipoRef != EquipoRef;
		}

		public Color FlagColor { get; set; }

		public InventarioClan Drops { get; }

		#region IEquatable

		public bool Equals (Equipo other)
		{
			return EquipoRef == other.EquipoRef;
		}

		#endregion
	}
}