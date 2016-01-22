﻿using KarTac.Skills;
using KarTac.Recursos;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public class Hacha : EquipArma
	{
		/// <summary>
		/// La habilidad otorgada por esta arma
		/// </summary>
		class Hacha_skill : Golpe
		{
			public Hacha_skill (Personaje pj)
				: base (pj)
			{
			}

			public override string Descripción
			{
				get
				{
					return "Da un golpe con el hacha";
				}
			}

			public override string Nombre
			{
				get
				{
					return "Hacha";
				}
			}

			public override string IconTextureName
			{
				get
				{
					//TODO
					return @"Icons/Equip/broadsword";
				}
			}

			protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
			{
				float daño = (float)DamageUtils.CalcularDaño (
					             UnidadUsuario.AtributosActuales.Ataque.Valor +
					             UnidadUsuario.PersonajeBase.Atributos.Recs ["Hacha"].Valor,
					             unid.AtributosActuales.Defensa.Valor,
					             2.2);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.1;
				UnidadUsuario.PersonajeBase.Atributos.Recs ["Hacha"].PeticiónExpAcumulada += 0.3;
				unid.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;

				LastReturn = new  SkillReturnType (
					-daño,
					unid.AtributosActuales.HP,
					unid.Pos);

				return LastReturn;
			}
		}

		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "arma";
				yield return "Hacha";
			}
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Hacha_skill (Portador);
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			// Agregar su atributo con la espada
			#if DEBUG
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Espada", true));
			#else
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Espada", false));
			#endif
			base.OnEquipar (anterior);
		}

		public override string IconContentString
		{
			get
			{
				//TODO
				return @"Icons/Equip/broadsword";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Hacha";
			}
		}
	}
}