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

		float deltaDef = 0;
		float deltaAgil = 0;

		public override System.Collections.Generic.IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("cuerpo"));
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			Portador.Atributos.Add (new AtributoGenérico (
				"armadura ligera",
				false));
			//deltaDef = 2 + Portador.Atributos.Recs ["armadura ligera"].Valor;
			//deltaAgil = -2 / (Portador.Atributos.Recs ["armadura ligera"].Valor + 1);
			Portador.Atributos.Defensa.Inicial += deltaDef;
			Portador.Atributos.Agilidad.Inicial += deltaAgil;
		}

		protected override void OnDesequipar (ConjuntoEquipamento anterior)
		{
			if (Portador != null)
			{
				Portador.Atributos.Defensa.Inicial -= deltaDef;
				Portador.Atributos.Agilidad.Inicial += deltaAgil;
			}
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			Portador.Atributos.GetRecursoBase ("armadura ligera").PeticiónExpAcumulada += time.TotalSeconds / 10;
		}

		public override void Cargar (System.IO.BinaryReader reader)
		{
			deltaDef = reader.ReadSingle ();
			deltaAgil = reader.ReadSingle ();
		}

		public override void Guardar (System.IO.BinaryWriter writer)
		{
			base.Guardar (writer);
			writer.Write (deltaDef);
			writer.Write (deltaAgil);
		}

	}
}