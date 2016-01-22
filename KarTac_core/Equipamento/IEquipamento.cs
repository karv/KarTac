using System;
using System.Collections.Generic;
using KarTac.Skills;

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

		IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj);

		/// <summary>
		/// Se desequipa
		/// </summary>
		void Desequipar ();

		/// <summary>
		/// Skills que ofrece este objeto
		/// </summary>
		/// <value>The skills.</value>
		IEnumerable<ISkill> Skills { get; }

		/// <summary>
		/// Se ejecuta continuamente para todas las unidades durante una batalla.
		/// </summary>
		void BattleUpdate (TimeSpan time);

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