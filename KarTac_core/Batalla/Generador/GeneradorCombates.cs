using System;
using KarTac.Personajes;
using KarTac.Recursos;
using KarTac.Skills;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using OpenTK.Platform.MacOS;
using System.Threading;

namespace KarTac.Batalla.Generador
{
	public class GeneradorCombates
	{
		/// <summary>
		/// Experiencia que se le dará a todo el equipo
		/// </summary>
		public float TotalExp;

		/// <summary>
		/// Número de unidades que debe generar.
		/// </summary>
		public int NumUnidades = 3;
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
					pj.Atributos.Recs.Add (x.Key);
				else
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
			unidad.Interactor = new IA.AIMeléBásico (unidad);
		}

		void normalizarDistr ()
		{
			float suma = 0;
			foreach (var x in ExpDistr)
				suma += x.Value;
			foreach (var x in ExpDistr)
				x.Value /= suma;
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

