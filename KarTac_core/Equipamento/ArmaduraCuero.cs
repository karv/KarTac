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

		float deltaDef;

		public override System.Collections.Generic.IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("cuerpo"));
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			if (!Portador.Atributos.Recs.ContainsKey ("armadura ligera"))
				Portador.Atributos.Recs.Add (new AtributoGenérico (
					"armadura ligera",
					false));
			deltaDef = 3 + Portador.Atributos.Recs ["armadura ligera"].Valor;
			Portador.Atributos.Defensa.Inicial += deltaDef;
		}

		protected override void OnDesequipar (ConjuntoEquipamento anterior)
		{
			if (Portador != null)
				Portador.Atributos.Defensa.Inicial -= deltaDef;
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			Portador.Atributos.Recs ["armadura ligera"].PeticiónExpAcumulada += time.TotalSeconds / 10;
		}
	
	}
}