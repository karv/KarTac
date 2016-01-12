using KarTac.Skills;
using KarTac.Recursos;
using System;
using KarTac.Personajes;

namespace KarTac.Equipamento
{
	public class EqEspada : EquipArma
	{
		/// <summary>
		/// La habilidad otorgada por esta arma
		/// </summary>
		class Espadazo : Golpe
		{
			public Espadazo (Personaje pj)
				: base (pj)
			{
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
				var dañoBloqueado = Math.Max (
					                    unid.AtributosActuales.Defensa.Valor - UnidadUsuario.AtributosActuales.Ataque.Valor - UnidadUsuario.AtributosActuales.Recs ["Espada"].Valor,
					                    0);
				var daño = Math.Max (30 - dañoBloqueado, 1);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				PeticiónExpAcumulada += 1;
				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.3;
				UnidadUsuario.PersonajeBase.Atributos.Recs ["Espada"].PeticiónExpAcumulada += 0.3;
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
				yield return "Arma";
				yield return "Espada";
			}
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Espadazo (Portador);
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			// Agregar su atributo con la espada
			if (!Portador.Atributos.Recs.ContainsKey ("Espada"))
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