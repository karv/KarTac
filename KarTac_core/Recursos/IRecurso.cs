using System;
using Microsoft.Xna.Framework;
using KarTac.Batalla;
using KarTac.Batalla.Exp;
using KarTac.IO;
using System.IO;
using KarTac.Personajes;
using OpenTK.Graphics.OpenGL;

namespace KarTac.Recursos
{
	public static class Lector
	{
		public static IRecurso Cargar (BinaryReader reader)
		{
			IRecurso ret;
			var recNombre = reader.ReadString ();
			switch (recNombre)
			{
				case "HP":
					ret = new HP ();
					break;
				case "Maná":
					ret = new Maná ();
					break;
				case "Condición":
					ret = new Condición ();
					break;
				case "Multi":
					ret = new MultiRecurso ();
					break;
				default:
					ret = new AtributoGenérico (recNombre, false);
					break;
			}
			ret.Cargar (reader);
			return ret;
		}
	}

	public interface IRecurso : IExp, IGuardable
	{
		/// <summary>
		/// Nombre del recurso
		/// </summary>
		string Nombre { get; }

		/// <summary>
		/// Valor actual del recurso
		/// </summary>
		float Valor { get; set; }

		/// <summary>
		/// Si el atributo es visible durante la batalla
		/// </summary>
		bool VisibleBatalla { get; }

		/// <summary>
		/// El icono que se usará para mostrarse
		/// </summary>
		string Icono { get; }

		/// <summary>
		/// Conjunto de atributos al que pertenece.
		/// </summary>
		AtributosPersonaje ConjAtrib { get; set; }

		/// <summary>
		/// Ejecuta un tick de longitud dada
		/// </summary>
		void Tick (TimeSpan delta);

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