using KarTac.Batalla.Exp;
using System.Collections.Generic;
using KarTac.Personajes;
using System;
using Microsoft.Xna.Framework;
using KarTac.Recursos;

namespace KarTac.Skills
{
	public interface ISkillReturnType
	{
		float Delta { get; }

		Color? Color { get; }

		Point Loc { get; }
	}

	public struct SkillReturnType : ISkillReturnType
	{
		public SkillReturnType (float delta, IRecurso recurso, Point pos)
		{
			Delta = delta;
			Recurso = recurso;
			Loc = pos;
		}

		public Point Loc { get; }

		public float Delta { get; }

		public IRecurso Recurso { get; }

		public Color? Color
		{
			get
			{
				return Delta < 0 ? Recurso.ColorMostrarPerdido : Recurso.ColorMostrarGanado;
			}
		}
	}


	public interface ISkill: IExp
	{
		string Nombre { get; }

		string Descripción { get; }

		Personaje Usuario { get; }

		void Ejecutar ();

		/// <summary>
		/// Determina si es posible usar este skill.
		/// </summary>
		bool Usable { get; }

		string IconTextureName { get; }

		/// <summary>
		/// Se ejecuta después de asignar experiencia.
		/// Debe devolver los skills a la lista de 'podría aprender' de la unidad.
		/// </summary>
		IEnumerable<ISkill> DesbloquearSkills ();

		/// <summary>
		/// Revisa si una Unidad cumple los requicitos para aprender este Skill.
		/// </summary>
		bool PuedeAprender ();

		/// <summary>
		/// Se ejecuta al aprender
		/// </summary>
		void AlAprender ();

		/// <summary>
		/// Ocurre al terminar completamente el procedumiento de este ISkill
		/// </summary>
		event Action<ISkillReturnType> AlTerminarEjecución;
	}
}