﻿using KarTac.Skills;
using KarTac.Recursos;
using KarTac.Personajes;
using System.Collections.Generic;

namespace KarTac.Equipamento
{
	public class EqEspada : EquipArma
	{
		/// <summary>
		/// La habilidad otorgada por esta arma
		/// </summary>
		class Skill : Golpe
		{
			public Skill (Personaje pj)
				: base (pj)
			{
			}

			public override double Rango
			{
				get
				{
					return 50;
				}
			}

			public override string Descripción
			{
				get
				{
					return "Da un golpe con la espada";
				}
			}

			public override string Nombre
			{
				get
				{
					return "Espadazo";
				}
			}

			public override string IconTextureName
			{
				get
				{
					return @"Icons/Equip/broadsword";
				}
			}

			protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
			{
				float daño = (float)DamageUtils.CalcularDaño (
					             UnidadUsuario.AtributosActuales.Ataque.Valor +
					             UnidadUsuario.PersonajeBase.Atributos ["Espada"],
					             unid.AtributosActuales.Defensa.Valor,
					             2);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				PeticiónExpAcumulada += 1;
				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.1;
				UnidadUsuario.PersonajeBase.Atributos.GetRecursoBase ("Espada").AcumularExp (0.3);
				unid.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;

				LastReturn = new  SkillReturnType (
					-daño,
					unid.AtributosActuales.HP,
					unid.Pos);

				return LastReturn;
			}
		}

		public override IEnumerable<string> Tags
		{
			get
			{
				yield return "arma";
				yield return "Espada";
			}
		}

		protected override IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Skill (Portador);
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			#if DEBUG
			Portador.Atributos.Add (new AtributoGenérico ("Espada", true));
			#else
			Portador.Atributos.Add (new AtributoGenérico ("Espada", false));
			#endif
			base.OnEquipar (anterior);
		}

		public override string IconContentString
		{
			get
			{
				return @"Icons/Equip/broadsword";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Espada";
			}
		}
	}
}