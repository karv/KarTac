using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla;

namespace KarTac.Recursos
{
	public interface IRecurso : IExp
	{
		/// <summary>
		/// Nombre del recurso
		/// </summary>
		string Nombre { get; }

		/// <summary>
		/// Valor actual del recurso
		/// </summary>
		float Valor { get; }

		/// <summary>
		/// Ejecuta un tick de longitud dada
		/// </summary>
		void Tick (DateTime delta);

		/// <summary>
		/// Se ejecuta junto con Update,
		/// Debe usarse para actualizar la experiencia pedida
		/// </summary>
		void PedirExp (TimeSpan time, Campo campo);

		/// <summary>
		/// Ocurre cuando cambia el valor actual
		/// </summary>
		event Action AlCambiarValor;
	}
}