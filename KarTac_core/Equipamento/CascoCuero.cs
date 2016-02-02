using System.Linq;
using KarTac.Recursos;

namespace KarTac.Equipamento
{
	public class CascoCuero : Equipamento
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
				return "Casco de cuero";
			}
		}

		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "casco";
				yield return "cabeza";
			}
		}

		public override System.Collections.Generic.IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("cabeza"));
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			if (!Portador.Atributos.TieneAtributo ("armadura ligera"))
				Portador.Atributos.Add (new AtributoGenérico (
					"armadura ligera",
					false));
		}

		public override System.Collections.Generic.IEnumerable<IModificador> Modificadores
		{
			get
			{
				yield return new KarTac.Personajes.ModificadorAtributo (
					"Defensa",
					1 + Portador.Atributos.GetRecursoBase ("armadura ligera").Valor);

				yield return new KarTac.Personajes.ModificadorAtributo (
					"Agilidad",
					-1 / Portador.Atributos.GetRecursoBase ("armadura ligera").Valor);
			}
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			Portador.Atributos.GetRecursoBase ("armadura ligera").PeticiónExpAcumulada += time.TotalSeconds / 10;
		}
	}
}