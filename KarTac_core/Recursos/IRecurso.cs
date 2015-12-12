using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using KarTac.Batalla.Exp;
using KarTac.IO;

namespace KarTac.Recursos
{
	public interface IRecurso : IExp, IGuardable
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
		void Tick (GameTime delta);

		/// <summary>
		/// Se ejecuta junto con Update,
		/// Debe usarse para actualizar la experiencia pedida
		/// </summary>
		void PedirExp (TimeSpan time, Campo campo);

		void Reestablecer ();

		/// <summary>
		/// Ocurre cuando cambia el valor actual
		/// </summary>
		event Action AlCambiarValor;

		/// <summary>
		/// Color a mostrar si se aumenta este recurso
		/// Null para nunca mostrar.
		/// </summary>
		Color? ColorMostrarGanado { get; }

		/// <summary>
		/// Color a mostrar si se reduce este recurso
		/// Null para nunca mostrar.
		/// </summary>
		Color? ColorMostrarPerdido { get; }
	}
}