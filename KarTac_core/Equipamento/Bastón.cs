using System;
using KarTac.Recursos;

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
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Bastón", true));
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Poder mágico", true));
			#else
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Bastón", false));
			Portador.Atributos.Recs.Add (new AtributoGenérico ("Poder mágico", false));
			#endif

			deltaPM = 1 + Portador.Atributos.Recs ["Bastón"].Valor;
			Portador.Atributos.Recs ["Poder mágico"].Valor += deltaPM;
		}

		protected override void OnDesequipar (ConjuntoEquipamento anterior)
		{
			if (Portador != null)
				Portador.Atributos.Recs ["Poder mágico"].Valor += deltaPM;
			deltaPM = 0;
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