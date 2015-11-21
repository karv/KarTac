using KarTac.Batalla;
using System.Linq;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;

namespace KarTac.Skills
{
	public class Golpe:ISkill
	{
		public string Nombre
		{
			get
			{
				return "Golpe";
			}
		}


		public void Ejecutar (Unidad usuario, Campo campo)
		{
			var selector = campo.SelectorTarget;
			var área = new Círculo (usuario.Pos, 100);

			selector.MaxSelect = 1;
			selector.PosiblesBlancos = new List<Unidad> (campo.Unidades.Where (x => área.Contiene (x.Pos)));
			selector.IgualdadEstricta = true;
			var selección = selector.Selecciona () [0];

			// usuario ataca a selección

			var dañoBloqueado = Math.Max (
				                    usuario.PersonajeBase.Atributos.Ataque - selección.PersonajeBase.Atributos.Defensa,
				                    0);
			var daño = dañoBloqueado * 2;

			selección.PersonajeBase.Atributos.HP.Valor -= daño;
		}

		public bool Usable (Unidad usuario, Campo campo)
		{
			return true; //Siempre me puedo golpear solo :3
		}
	}
}