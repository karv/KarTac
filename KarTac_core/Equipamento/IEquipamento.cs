using System;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public interface IEquipamento : IItem
	{

		/// <summary>
		/// Personaje que porta este equipamento,
		/// </summary>
		Personaje Portador { get; set; }

		string IconContentString { get; }

		/// <summary>
		/// Se equipa en un personaje.
		/// Se desequipa de su personaje anterior
		/// </summary>
		void EquiparEn (Personaje personaje);

		/// <summary>
		/// Se desequipa
		/// </summary>
		void Desequipar ();

		/// <summary>
		/// Ocurre al equiparse.
		/// Primer argumento es el usuario anterior.
		/// </summary>
		/// 
		event Action<Personaje> AlEquipar;
		/// <summary>
		/// Ocurre al desequiparse.
		/// Argumento es sy usuario anterior.
		/// </summary>
		event Action<Personaje> AlDesequipar;
	}
}