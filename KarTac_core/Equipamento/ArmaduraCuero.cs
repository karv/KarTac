using System.Linq;
using KarTac.Recursos;

namespace KarTac.Equipamento
{
	public class ArmaduraCuero : Equipamento
	{

		public override string IconContentString
		{
			get
			{
				return "Rect";//TODO
			}
		}

		public override string Nombre
		{
			get
			{
				return "Armadura de cuero";
			}
		}

		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "armadura";
				yield return "cuerpo";
			}
		}

		public override System.Collections.Generic.IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("cuerpo"));
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			Portador.Atributos.Add (new AtributoGenérico (
				"armadura ligera",
				false));
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			Portador.Atributos.GetRecursoBase ("armadura ligera").AcumularExp (time.TotalSeconds / 10);
		}

		public override System.Collections.Generic.IEnumerable<IModificador> Modificadores
		{
			get
			{
				yield return new KarTac.Personajes.ModificadorAtributo (
					"Defensa",
					2 + Portador.Atributos.GetRecursoBase ("armadura ligera").Valor);

				yield return new KarTac.Personajes.ModificadorAtributo (
					"Agilidad",
					-2 / (Portador.Atributos.GetRecursoBase ("armadura ligera").Valor + 1));
			}
		}
	}
}