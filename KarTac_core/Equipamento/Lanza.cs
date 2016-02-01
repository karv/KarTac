using KarTac.Skills;
using KarTac.Recursos;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public class Lanza : EquipArma
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
					return 65;
				}
			}

			public override string Descripción
			{
				get
				{
					return "Da un golpe con su lanza";
				}
			}

			public override string Nombre
			{
				get
				{
					return "Lanza";
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
					             UnidadUsuario.PersonajeBase.Atributos ["Lanza"],
					             unid.AtributosActuales.Defensa.Valor / 2,
					             1);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.1;
				UnidadUsuario.PersonajeBase.Atributos.GetRecursoBase ("Lanza").PeticiónExpAcumulada += 0.3;
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
				yield return "Lanza";
			}
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Skill (Portador);
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			#if DEBUG
			const bool _visible = true;
			#else
			const bool _visible = false;
			#endif

			Portador.Atributos.Add (new AtributoGenérico ("Lanza", _visible));
			base.OnEquipar (anterior);
		}

		public override string IconContentString
		{
			get
			{
				// TODO
				return @"Icons/Equip/broadsword";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Lanza";
			}
		}
	}
}