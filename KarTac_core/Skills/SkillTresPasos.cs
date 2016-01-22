using KarTac.Personajes;
using KarTac.Batalla;
using System;
using KarTac.Batalla.Orden;
using System.Collections.Generic;
using System.Linq;

namespace KarTac.Skills
{
	public abstract class SkillTresPasos : SkillComún
	{
		protected SkillTresPasos (Personaje usuario)
			: base (usuario)
		{
		}

		protected SkillTresPasos (Unidad usuario)
			: base (usuario)
		{
		}

		public override void Ejecutar ()
		{
			var ords = new IOrden[3];
			ords [0] = ConstruirPreparación ();
			ords [1] = ConstruirEjecución ();
			ords [2] = ConstruirTerminal ();
			AlIniciarPreparación?.Invoke ();
			UnidadUsuario.OrdenActual = new OrdenSerie (UnidadUsuario, ords);
		}

		protected virtual IOrden ConstruirPreparación ()
		{
			var ret = new Quieto (Usuario.Unidad, CalcularTiempoPreparación ());
			ret.AlTerminar += delegate
			{
				AlIniciarEjecución?.Invoke ();
			};
			return ret;
		}

		protected virtual IOrden ConstruirEjecución ()
		{
			var ret = new OrdenPedirTarget (UnidadUsuario);
			var inter = UnidadUsuario.Interactor;
			inter.Selector.MaxSelect = MaxSelect;
			inter.Selector.IgualdadEstricta = IgualdadEstricta;
			inter.Selector.PosiblesBlancos = new List<Unidad> (CampoBatalla.Unidades.Where (SeleccionaTarget).OrderBy (x => UnidadUsuario.Equipo.EsAliado (x)));

			inter.Selector.AlResponder += delegate(SelecciónRespuesta obj)
			{
				foreach (var x in obj.Selección)
				{
					var rt = EffectOnTarget (x);
					x.OnSerBlanco (rt);
				}
				obj.Clear ();
				AlResponder?.Invoke ();

			};
			inter.Selector.AlCancelar += delegate
			{
				UnidadUsuario.OrdenActual = null;
				AlCancelar?.Invoke ();
			};
			ret.AlTerminar += delegate
			{
				AlIniciarCooldown?.Invoke ();
			};
			ret.AlMostrarLista += delegate
			{
				AlMostrarLista?.Invoke ();
			};
			return ret;
		}

		/// <summary>
		/// El efecto en el blanco
		/// </summary>
		protected abstract ISkillReturnType EffectOnTarget (Unidad unid);

		protected virtual IOrden ConstruirTerminal ()
		{
			var ret = new Quieto (Usuario.Unidad, CalcularTiempoUso ());
			return ret;
		}

		protected abstract TimeSpan CalcularTiempoUso ();

		protected abstract TimeSpan CalcularTiempoPreparación ();

		protected abstract bool SeleccionaTarget (Unidad u);

		protected abstract int MaxSelect { get; }

		protected abstract bool IgualdadEstricta { get; }

		protected abstract ISkillReturnType LastReturn { get; set; }

		public event Action AlIniciarPreparación;
		public event Action AlIniciarEjecución;
		public event Action AlIniciarCooldown;
		public event Action AlResponder;
		public event Action AlCancelar;
		public event Action AlMostrarLista;
	}
}