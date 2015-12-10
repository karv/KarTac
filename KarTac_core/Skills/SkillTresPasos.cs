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
			Preparación (CalcularTiempoPreparación ());
		}

		/// <summary>
		/// Preparar el skill
		/// Invocar base.Preparación al final
		/// </summary>
		public virtual void Preparación (TimeSpan tiempoPreparación)
		{
			AlIniciarPreparación?.Invoke ();
			Usuario.Unidad.OrdenActual = new Quieto (Usuario.Unidad, tiempoPreparación);
			Usuario.Unidad.OrdenActual.AlTerminar += delegate
			{
				Ejecución ();
			};
		}

		protected abstract TimeSpan CalcularTiempoUso ();

		protected abstract TimeSpan CalcularTiempoPreparación ();

		protected abstract bool SeleccionaTarget (Unidad u);

		protected abstract int MaxSelect { get; }

		protected abstract bool IgualdadEstricta { get; }

		public virtual void Ejecución ()
		{
			var selector = CampoBatalla.SelectorTarget;
			AlIniciarEjecución?.Invoke ();

			selector.MaxSelect = MaxSelect;
			selector.PosiblesBlancos = new List<Unidad> (CampoBatalla.Unidades.Where (SeleccionaTarget).OrderBy (x => UnidadUsuario.Equipo.EsAliado (x)));
			selector.IgualdadEstricta = IgualdadEstricta;

			if (!selector.Validar ())
				throw new Exception ();

			selector.AlResponder += delegate (SelecciónRespuesta obj)
			{
				Terminal (obj);	
				OnTerminar ();
				selector.ClearStatus (); // Limpia el cache temporal
				AlResponder?.Invoke ();
			};

			selector.AlCancelar += delegate
			{
				AlCancelar?.Invoke ();
			};

			selector.Selecciona (UnidadUsuario);
		}

		/// <summary>
		/// Código heredado debe ir antes de base.Termilal
		/// </summary>
		public abstract void Terminal (SelecciónRespuesta obj);

		protected override void OnTerminar ()
		{
			AlIniciarCooldown?.Invoke ();
			base.OnTerminar ();
			var ordQuieto = new Quieto (UnidadUsuario, CalcularTiempoUso ());
			UnidadUsuario.OrdenActual = ordQuieto;
		}

		public event Action AlIniciarPreparación;
		public event Action AlIniciarEjecución;
		public event Action AlIniciarCooldown;
		public event Action AlResponder;
		public event Action AlCancelar;
	}
}