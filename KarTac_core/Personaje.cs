﻿using System;

namespace KarTac
{
	public class Personaje
	{
		/// <summary>
		/// Nombre del personaje
		/// </summary>
		/// <value>The nombre.</value>
		public string Nombre { get; set; }

		/// <summary>
		/// Atributos del personaje
		/// </summary>
		/// <value>The atributos.</value>
		public AtributosPersonaje Atributos { get; }

		public Personaje ()
		{
			Atributos = new AtributosPersonaje ();
		}
	}
}