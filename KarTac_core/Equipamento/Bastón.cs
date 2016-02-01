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

		float deltaPM = 0;

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			#if DEBUG
			const bool visible = true;
			#else
			const bool visible = false;
			#endif
			Portador.Atributos.Add (new AtributoGenérico ("Bastón", visible));
			Portador.Atributos.Add (new AtributoGenérico ("Poder mágico", visible));

			//deltaPM = 1 + Portador.Atributos.Recs ["Bastón"].Valor;
			//Portador.Atributos.Recs ["Poder mágico"].Valor += deltaPM;
			Portador.Atributos.Add (new Maná ()); // Dárselo si no lo tiene
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new RayoManá (Portador);
			}
		}

		protected override void OnDesequipar (ConjuntoEquipamento anterior)
		{
//			if (Portador != null)
//				Portador.Atributos.Recs ["Poder mágico"].Valor += deltaPM;
			deltaPM = 0;
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			base.BattleUpdate (time);
			Portador.Atributos.GetRecursoBase ("Bastón").PeticiónExpAcumulada += time.TotalSeconds / 6;
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