using System;

namespace KarTac.Batalla.Orden
{
	public class OrdenPedirTarget : IOrden
	{
		public OrdenPedirTarget (Unidad unidad)
		{
			Unidad = unidad;
		}

		#region IOrden implementation

		public event Action AlTerminar
		{
			add
			{
				throw new Exception ();
			}
			remove
			{
				throw new Exception ();
			}
		}

		public bool Update (TimeSpan time)
		{
			var selector = Unidad.Interactor.Selector;

			//selector.PosiblesBlancos = new List<Unidad> (Unidad.CampoBatalla.Unidades.Where (SeleccionaTarget).OrderBy (x => Unidad.Equipo.EsAliado (x)));

			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += (obj) => selector.ClearStatus ();

			selector.AlCancelar += delegate
			{
				Unidad.OrdenActual = null;
				//AlCancelar?.Invoke ();
			};

			selector.Selecciona (Unidad);
			return true;
		}

		public Unidad Unidad { get; }

		#endregion
	}
}