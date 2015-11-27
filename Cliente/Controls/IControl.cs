﻿using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls
{
	public interface IControl
	{
		IScreen Screen { get; }

		/// <summary>
		/// Incluir este control en su pantalla
		/// </summary>
		void Include ();

		/// <summary>
		/// Excluir este control de su pantalla
		/// </summary>
		void Exclude ();

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		int Prioridad { get; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		void Dibujar (GameTime gameTime);

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime);

		/// <summary>
		/// Cargar contenido
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
		void Inicializar ();

	}
}