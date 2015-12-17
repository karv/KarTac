using KarTac.Batalla;
using System;

namespace KarTac.Recursos
{
	public class AtributoGenérico : RecursoAcotado
	{
		public AtributoGenérico (string nombre)
		{
			_nombre = nombre;
		}

		public override void CommitExp (double exp)
		{
			Max += (float)exp;
		}

		string _nombre;

		public override string Nombre
		{
			get
			{
				return _nombre;
			}
		}

		protected override void PedirExp (TimeSpan time, Campo campo)
		{
		}
	}
}