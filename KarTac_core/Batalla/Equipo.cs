using Microsoft.Xna.Framework;

namespace KarTac.Batalla
{
	public struct Equipo : IEquipoSelector
	{
		public Equipo (int equipoRef, Color flagColor)
		{
			EquipoRef = equipoRef;
			FlagColor = flagColor;
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
	}
}