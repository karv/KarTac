using KarTac.Recursos;
using System.Collections.Generic;

namespace KarTac.Recursos
{
	public class ListaRecursos : List<IRecurso>
	{
		public IRecurso this [string nombre]
		{
			get
			{
				return Find (x => x.Nombre == nombre);
			}
		}
	}
}