using Microsoft.Xna.Framework;
using KarTac;

namespace KarTac.Batalla
{
	public struct Equipo : IEquipoSelector
	{
		public Equipo (int equipoRef, Color flagColor, Clan clan)
		{
			EquipoRef = equipoRef;
			FlagColor = flagColor;
			ClanEquipo = clan;
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

		public Clan ClanEquipo { get; }
	}
}