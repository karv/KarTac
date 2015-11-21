using System;

namespace KarTac.Batalla
{
	public struct Equipo : IEquipoSelector
	{
		public int EquipoRef { get; }

		public bool EsAliado (Unidad unidad)
		{
			return unidad.Equipo.EquipoRef == EquipoRef;
		}

		public bool EsEnemigo (Unidad unidad)
		{
			return unidad.Equipo.EquipoRef != EquipoRef;
		}
	}
}

