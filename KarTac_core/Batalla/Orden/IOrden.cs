using System;

namespace KarTac.Batalla.Orden
{
	/// <summary>
	/// Da la orden de qué hacer en cada Update de Unidad,
	/// en términos de primitivos
	/// </summary>
	public interface IOrden
	{
		Unidad Unidad { get; }

		/// <summary>
		/// Realiza la orden a la unidad,
		/// devuelve el tiempo que requirió en hacerlo, así como el tiempo de petición y si terminó la orden.
		/// </summary>
		UpdateReturnType Update (TimeSpan time);

		event Action AlTerminar;
	}

	public struct UpdateReturnType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KarTac.Batalla.Orden.UpdateReturnType"/> struct.
		/// Inicializado sin término
		/// </summary>
		/// <param name="petición">Petición.</param>
		public UpdateReturnType (TimeSpan petición)
		{
			TiempoPetición = petición;
			TiempoUsado = petición;
			Terminó = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KarTac.Batalla.Orden.UpdateReturnType"/> struct.
		/// Inicializado con término.
		/// </summary>
		/// <param name="petición">Petición.</param>
		public UpdateReturnType (TimeSpan petición, TimeSpan usado)
		{
			TiempoPetición = petición;
			TiempoUsado = usado;
			Terminó = true;
		}

		/// <summary>
		/// Tiempo restante
		/// </summary>
		/// <value>The restante.</value>
		public TimeSpan Restante
		{
			get
			{
				return TiempoPetición - TiempoUsado;
			}
		}

		/// <summary>
		/// Devuelve si terminó de realizar la orden
		/// </summary>
		public bool Terminó { get; }

		/// <summary>
		/// Tiempo que se le dio en Update
		/// </summary>
		public TimeSpan TiempoPetición { get; }

		/// <summary>
		/// Tiempo que usó durante Update
		/// </summary>
		public TimeSpan TiempoUsado { get; }


	}
}