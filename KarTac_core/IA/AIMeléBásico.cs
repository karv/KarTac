using System;
using KarTac.Batalla;
using KarTac.Skills;
using KarTac.Batalla.Orden;
using System.Linq;

namespace KarTac.IA
{
	public class AIMeléBásico : IInteractor
	{
		class SelectorTarget : ISelectorTarget
		{
			public SelectorTarget (SkillTresPasosShaped skl)
			{
				skill = skl;
			}

			SkillTresPasosShaped skill;

			public event Action<SelecciónRespuesta> AlResponder;

			public event Action AlCancelar;

			public bool Validar ()
			{
				return true;
			}

			public void Selecciona (Unidad unidad)
			{
				var unid = unidad.CampoBatalla.UnidadesVivas.FirstOrDefault (z => unidad.Equipo.EsEnemigo (z) && skill.GetÁrea ().Contiene (z.Pos));
				// Si al apsar el tiempo, la condición de poder atacar a alguien se violó, cancelar.
				if (unid == null)
					AlCancelar?.Invoke ();
				else
				{
					var r = new Unidad[1];
					r [0] = unid;
					var resp = new SelecciónRespuesta (r);
					AlResponder?.Invoke (resp);
				}
			}

			public void ClearStatus ()
			{
				AlResponder = null;
				AlCancelar = null;
			}

			public int MaxSelect { set; private get; }

			public bool IgualdadEstricta { set; private get; }

			public System.Collections.Generic.IList<Unidad> PosiblesBlancos { set; private get; }
		}

		public AIMeléBásico (Unidad unidad)
		{
			Unidad = unidad;

			// Encontrar el skill Golpe
			foreach (var x in unidad.PersonajeBase.InnerSkill)
			{
				var golpe = x as Golpe;
				if (golpe != null)
				{
					golpeSkill = golpe;
					break;
				}
			}
			Selector = new SelectorTarget (golpeSkill);
		}

		Golpe golpeSkill { get; }

		public Unidad Unidad { get; }

		public Campo Campo
		{
			get
			{
				return Unidad.CampoBatalla;
			}
		}

		public event Action AlTerminar;

		public void Ejecutar ()
		{
			/* Si HP < 30 huir.
			 * Si no: Rodear hasta tener a alguien cerca y atacarlo con Golpe
			 */ 
			if (debeHuir ())
				Unidad.OrdenActual = new Huir (
					Unidad,
					TimeSpan.FromSeconds (0.1));
			else if (alguienEnRango ())
				golpeSkill.Ejecutar ();
			else
				Unidad.OrdenActual = new Rodear (Unidad, golpeSkill.Rango * 0.9f);
			AlTerminar?.Invoke ();
		}

		bool debeHuir ()
		{
			return Unidad.AtributosActuales.HP.Valor < 30;
		}

		bool alguienEnRango ()
		{
			return Campo.UnidadesVivas.Any (z => Unidad.Equipo.EsEnemigo (z) && golpeSkill.GetÁrea ().Contiene (z.Pos));
		}

		public ISelectorTarget Selector { get; }
	}
}

