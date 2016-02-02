using System;
using KarTac.Skills;
using KarTac.Recursos;
using KarTac.Batalla.Shape;

namespace KarTac.Equipamento
{
	public class Arco : EquipArma
	{
		/// <summary>
		/// La habilidad otorgada por esta arma
		/// </summary>
		class Flechazo : Golpe
		{
			public Flechazo (KarTac.Personajes.Personaje usuario)
				: base (usuario)
			{
			}


			public override string Descripción
			{
				get
				{
					return "Usa tu arco para lanzar una flecha";
				}
			}

			public override string Nombre
			{
				get
				{
					return "Flechazo";
				}
			}

			public override string IconTextureName
			{
				get
				{
					return @"Icons/Equip/arco";
				}
			}

			public override double Rango
			{
				get
				{
					return 120;
				}
			}

			protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
			{
				float daño = (float)DamageUtils.CalcularDaño (
					             UnidadUsuario.AtributosActuales.Ataque.Valor +
					             UnidadUsuario.PersonajeBase.Atributos ["Arco"],
					             unid.AtributosActuales.Defensa.Valor,
					             1.5);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				PeticiónExpAcumulada += 1;
				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.1;
				UnidadUsuario.PersonajeBase.Atributos.GetRecursoBase ("Arco").AcumularExp (0.3);
				unid.PersonajeBase.Atributos.Defensa.PeticiónExpAcumulada += 0.3;

				LastReturn = new  SkillReturnType (
					-daño,
					unid.AtributosActuales.HP,
					unid.Pos);

				return LastReturn;
			}

			public override IShape GetÁrea ()
			{
				return new Círculo (UnidadUsuario.Pos, (float)Rango);
			}
		}

		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "arma";
				yield return "arco";
			}
		}

		public override string IconContentString
		{
			get
			{
				return @"Icons/Equip/arco";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Arco corto";
			}
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Flechazo (Portador);
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			#if DEBUG
			Portador.Atributos.Add (new AtributoGenérico ("Arco", true));
			#else
			Portador.Atributos.Add (new AtributoGenérico ("Arco", false));
			#endif
			base.OnEquipar (anterior);
		}

	}
}

