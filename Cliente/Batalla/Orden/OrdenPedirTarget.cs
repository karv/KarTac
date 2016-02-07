using System;

namespace KarTac.Batalla.Orden
{
	public class OrdenPedirTarget : IOrden
	{
		public OrdenPedirTarget (Unidad unidad)
		{
			Unidad = unidad;
		}

		bool IOrden.EsCancelable
		{
			get
			{
				return false;
			}
		}

		public event Action AlMostrarLista;

		public event Action AlTerminar;

		public bool FueRespondido { get; private set; }

		#region IOrden implementation

		public UpdateReturnType Update (TimeSpan time)
		{
			var selector = Unidad.Interactor.Selector;

			//selector.PosiblesBlancos = new List<Unidad> (Unidad.CampoBatalla.Unidades.Where (SeleccionaTarget).OrderBy (x => Unidad.Equipo.EsAliado (x)));

			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += delegate
			{
				selector.ClearStatus ();
				AlTerminar?.Invoke ();
				FueRespondido = true;
			};

			selector.AlCancelar += delegate
			{
				FueRespondido = true;
			};

			selector.Selecciona (Unidad);

			AlMostrarLista?.Invoke ();
			return new UpdateReturnType (time, TimeSpan.Zero);
		}

		public Unidad Unidad { get; }

		#endregion
	}
}