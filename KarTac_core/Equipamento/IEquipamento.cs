using System;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public interface IEquipamento : IItem
	{

		/// <summary>
		/// Personaje que porta este equipamento,
		/// </summary>
		ConjuntoEquipamento Conjunto { get; }

		string IconContentString { get; }

		/// <summary>
		/// Se equipa en un personaje.
		/// Se desequipa de su personaje anterior
		/// </summary>
		void EquiparEn (ConjuntoEquipamento equips);

		/// <summary>
		/// Se desequipa
		/// </summary>
		void Desequipar ();

		/// <summary>
		/// Ocurre al equiparse.
		/// Primer argumento es el usuario anterior.
		/// </summary>
		/// 
		event Action<ConjuntoEquipamento> AlEquipar;
		/// <summary>
		/// Ocurre al desequiparse.
		/// Argumento es sy usuario anterior.
		/// </summary>
		event Action<ConjuntoEquipamento> AlDesequipar;
	}
}