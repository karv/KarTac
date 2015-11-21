﻿using System;

namespace KarTac.Recursos
{
	public interface IRecurso
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
	}
}

