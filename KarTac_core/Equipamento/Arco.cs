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
				var dañoBloqueado = Math.Max (
					                    unid.AtributosActuales.Defensa.Valor - UnidadUsuario.AtributosActuales.Ataque.Valor - UnidadUsuario.AtributosActuales.Recs ["Espada"].Valor,
					                    0);
				var daño = Math.Max (8 - dañoBloqueado, 1);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				PeticiónExpAcumulada += 1;
				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.3;
				UnidadUsuario.PersonajeBase.Atributos.Recs ["Arco"].PeticiónExpAcumulada += 0.3;
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
			// Agregar su atributo con la espada
			if (!Portador.Atributos.Recs.ContainsKey ("Arco"))
				#if DEBUG
				Portador.Atributos.Recs.Add (new AtributoGenérico ("Arco", true));
			#else
				Portador.Atributos.Recs.Add (new AtributoGenérico ("Arco", false));
			#endif
			base.OnEquipar (anterior);
		}

	}
}

