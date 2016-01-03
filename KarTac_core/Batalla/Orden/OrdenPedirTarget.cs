using System;

namespace KarTac.Batalla.Orden
{
	public class OrdenPedirTarget : IOrden
	{
		public OrdenPedirTarget (Unidad unidad)
		{
			Unidad = unidad;
		}

		public event Action AlMostrarLista;

		#region IOrden implementation

		public event Action AlTerminar;

		public TimeSpan Update (TimeSpan time)
		{
			var selector = Unidad.Interactor.Selector;

			//selector.PosiblesBlancos = new List<Unidad> (Unidad.CampoBatalla.Unidades.Where (SeleccionaTarget).OrderBy (x => Unidad.Equipo.EsAliado (x)));

			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += delegate
			{
				selector.ClearStatus ();
				AlTerminar?.Invoke ();
			};

			selector.AlCancelar += delegate
			{
				Unidad.OrdenActual = null;
				AlTerminar?.Invoke ();
				//AlCancelar?.Invoke ();
			};

			selector.Selecciona (Unidad);
			return TimeSpan.Zero;
		}

		public Unidad Unidad { get; }

		#endregion
	}
}