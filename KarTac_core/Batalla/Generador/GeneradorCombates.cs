using System;
using KarTac.Personajes;
using KarTac.Recursos;
using KarTac.Skills;
using System.Collections.Generic;

namespace KarTac.Batalla.Generador
{
	public static class GeneradorCombates
	{
		public static IEnumerable<Unidad> GenerarEquipoAleatorio (float totalExp, 
		                                                          Campo campo,
		                                                          params GeneradorUnidad.BuildOptions [] builds)
		{
			var numUnidades = builds.Length;
			var restante = totalExp;
			for (int i = numUnidades; i > 0; i--)
			{
				var usarExp = Utils.Randomize (restante / i, 0.3);
				restante -= usarExp;

				var gen = new GeneradorUnidad (builds [i - 1]);
				var uni = gen.Generar (usarExp, campo);
				yield return uni;
			}
		}
	}

	public class GeneradorUnidad
	{
		static Personaje generar ()
		{
			var pj = new Personaje ();
			pj.Atributos.Recs.Add (new HP ());

			pj.Atributos.Recs.Add (new AtributoGenérico ("Ataque", true));
			pj.Atributos.Recs.Add (new AtributoGenérico ("Defensa", true));
			pj.Atributos.Recs.Add (new AtributoGenérico ("Velocidad", true));
			pj.Atributos.Recs.Add (new AtributoGenérico ("Agilidad", true));
			pj.Atributos.Recs.Add (new Condición ());

			pj.Atributos.HP.Max = 100;
			pj.Atributos.HP.Valor = 100;
			pj.Atributos.HP.Regeneración = 60;
			pj.Atributos.Ataque.Inicial = 10;
			pj.Atributos.Ataque.CommitExpCoef = 0.3f;
			pj.Atributos.Agilidad.Inicial = 10;
			pj.Atributos.Agilidad.CommitExpCoef = 0.2f;
			pj.Atributos.Defensa.Inicial = 10;
			pj.Atributos.Defensa.CommitExpCoef = 0.35f;
			pj.Atributos.Velocidad.Inicial = 50;
			pj.Atributos.Velocidad.CommitExpCoef = 0.1f;
			pj.Atributos.Condición.Max = 120;

			pj.Nombre = "Persona";

			pj.InnerSkill.Add (new Golpe (pj));

			return pj;
		}

		/// <summary>
		/// Distribución de experiencia en los recursos 
		/// </summary>
		public readonly IDictionary<IRecurso, float> ExpDistr;
		public readonly Func<Unidad, IInteractor> Inter;

		Personaje generar (float exp)
		{
			var pj = generar ();
			normalizarDistr ();
			foreach (var x in ExpDistr)
			{
				if (!pj.Atributos.Recs.ContainsKey (x.Key.Nombre))
					pj.Atributos.Recs [x.Key.Nombre] = x.Key;

				pj.Atributos.Recs [x.Key.Nombre].CommitExp (exp * x.Value);
			}
			return pj;
		}

		public Unidad Generar (float exp, Campo campo)
		{
			var pers = generar (exp);
			var unidad = pers.ConstruirUnidad (campo);
			campo.AñadirUnidad (unidad);
			unidad.Interactor = Inter (unidad);
			return unidad;
		}

		void normalizarDistr ()
		{
			float suma = 0;

			foreach (var x in ExpDistr)
				suma += x.Value;
			foreach (var x in new List<IRecurso> (ExpDistr.Keys))
				ExpDistr [x] /= suma;
		}

		public GeneradorUnidad (BuildOptions opt)
		{
			ExpDistr = opt.DistribStats;
			Inter = opt.Interactor;
		}

		#region Builds

		public struct BuildOptions
		{
			public IDictionary<IRecurso, float> DistribStats { get; }

			public Func<Unidad, IInteractor> Interactor { get; }

			public BuildOptions (IDictionary<IRecurso, float> distr,
			                     Func<Unidad, IInteractor>  inter)
			{
				DistribStats = distr;
				Interactor = inter;
			}
		}

		public static BuildOptions Melee
		{
			get
			{
				var ret = new Dictionary<IRecurso, float> ();
				ret.Add (new AtributoGenérico ("Ataque", true), 1);
				ret.Add (new AtributoGenérico ("Defensa", true), 1);
				ret.Add (new AtributoGenérico ("Velocidad", true), 0.3f);
				return new BuildOptions (ret, u => new IA.AIMeléBásico (u));
			}
		}

		#endregion
	}
}

