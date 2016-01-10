using KarTac.Skills;
using KarTac.Recursos;
using System;
using System.Diagnostics;
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

			public override string Nombre
			{
				get
				{
					return "Espadazo";
				}
			}

			protected override ISkillReturnType EffectOnTarget (KarTac.Batalla.Unidad unid)
			{
				var dañoBloqueado = Math.Max (
					                    UnidadUsuario.AtributosActuales.Recs ["Ataque"].Valor + UnidadUsuario.AtributosActuales.Recs ["espada"].Valor - unid.AtributosActuales.Recs ["Defensa"].Valor,
					                    0);
				var daño = Math.Max (20 - dañoBloqueado, 1);

				unid.AtributosActuales.HP.Valor -= daño;
				System.Diagnostics.Debug.WriteLine (string.Format (
					"{0} causa {1} daño HP a {2}",
					UnidadUsuario,
					daño,
					unid));

				PeticiónExpAcumulada += 1;
				UnidadUsuario.PersonajeBase.Atributos.Ataque.PeticiónExpAcumulada += 0.3;
				UnidadUsuario.PersonajeBase.Atributos.Recs ["espada"].PeticiónExpAcumulada += 0.3;
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
				yield return "espada";
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
			if (!Portador.Atributos.Recs.ContainsKey ("espada"))
				Portador.Atributos.Recs.Add (new AtributoGenérico ("espada"));
			base.OnEquipar (anterior);
		}

		public override string IconContentString
		{
			get
			{
				return "Rect"; // TODO: buscarle icono
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