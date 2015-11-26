using KarTac.Batalla;
using System.Linq;
using KarTac.Batalla.Shape;
using System.Collections.Generic;
using System;

namespace KarTac.Skills
{
	public class Golpe : ISkill
	{
		public Golpe ()
		{
			ExpTags = new DictionaryTag ();
		}

		public float TotalExp { get; private set; }

		public string Nombre
		{
			get
			{
				return "Golpe";
			}
		}

		public ITagging ExpTags { get; }

		public string IconTextureName
		{
			get
			{
				return @"Icons/Skills/Golpe";
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

		public void RecibirExp (float exp)
		{
			TotalExp += exp;
		}
	}
}