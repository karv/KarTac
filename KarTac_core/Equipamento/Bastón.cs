using KarTac.Recursos;
using KarTac.Skills;

namespace KarTac.Equipamento
{
	public class Bastón : EquipArma
	{
		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "arma";
				yield return "Bastón";
			}
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			#if DEBUG
			const bool visible = true;
			#else
			const bool visible = false;
			#endif
			Portador.Atributos.Add (new AtributoGenérico ("Bastón", visible));
			Portador.Atributos.Add (new AtributoGenérico ("Poder mágico", visible));
			Portador.Atributos.Add (new Maná ()); // Dárselos si no los tiene
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new RayoManá (Portador);
			}
		}

		public override System.Collections.Generic.IEnumerable<IModificador> Modificadores
		{
			get
			{
				yield return new KarTac.Personajes.ModificadorAtributo (
					"Poder mágico",
					1 + Portador.Atributos.GetRecursoBase ("Bastón").Valor);
			}
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			base.BattleUpdate (time);
			Portador.Atributos.GetRecursoBase ("Bastón").AcumularExp (time.TotalSeconds / 6);
		}

		public override string IconContentString
		{
			get
			{
				// TODO
				return "Rect";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Bastón";
			}
		}

	}
}