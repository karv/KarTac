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

		float deltaDef;
		float deltaAgil;

		public override System.Collections.Generic.IEnumerable<IEquipamento> AutoRemove (ConjuntoEquipamento conj)
		{
			return conj.Where (x => x.Tags.Contains ("cabeza"));
		}

		protected override void OnEquipar (ConjuntoEquipamento anterior)
		{
			if (!Portador.Atributos.Recs.ContainsKey ("armadura ligera"))
				Portador.Atributos.Recs.Add (new AtributoGenérico (
					"armadura ligera",
					false));
			deltaDef = 1 + Portador.Atributos.Recs ["armadura ligera"].Valor;
			deltaAgil = -1 / (Portador.Atributos.Recs ["armadura ligera"].Valor + 1);
			Portador.Atributos.Defensa.Inicial += deltaDef;
			Portador.Atributos.Agilidad.Inicial += deltaAgil;
		}

		protected override void OnDesequipar (ConjuntoEquipamento anterior)
		{
			if (Portador != null)
			{
				Portador.Atributos.Defensa.Inicial -= deltaDef;
				Portador.Atributos.Agilidad.Inicial -= deltaAgil;
			}
			deltaDef = 0;
			deltaAgil = 0;
		}

		public override void BattleUpdate (System.TimeSpan time)
		{
			Portador.Atributos.Recs ["armadura ligera"].PeticiónExpAcumulada += time.TotalSeconds / 10;
		}

		public override void Cargar (System.IO.BinaryReader reader)
		{
			base.Cargar (reader);
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