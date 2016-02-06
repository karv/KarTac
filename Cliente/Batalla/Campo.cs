using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using KarTac.Skills;
using KarTac.Batalla.Objetos;
using KarTac.Controls.Screens;

namespace KarTac.Batalla
{
	/// <summary>
	/// Representa un campo de batalla
	/// </summary>
	public class Campo
	{
		/// <summary>
		/// Experiencia por minuto
		/// </summary>
		public float ExpPorMinuto = 5;

		public BattleScreen BattleScreen;

		/// <summary>
		/// Duración de la batalla (hasta este momento, si no ha terminado)
		/// </summary>
		public TimeSpan DuraciónBatalla { get; private set; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

		List<IObjeto> objetos = new List<IObjeto> ();

		public IReadOnlyCollection<IObjeto> Objetos
		{
			get
			{
				return objetos.AsReadOnly ();
			}
		}

		/// <summary>
		/// Devuelve una copia enumerable de las unidades vivas
		/// </summary>
		public IEnumerable<Unidad> UnidadesVivas
		{
			get
			{
				foreach (var u in new List<Unidad> (Unidades))
				{
					if (u.EstáVivo)
						yield return u;
				}
			}
		}

		public void AñadirUnidad (Unidad u)
		{
			Unidades.Add (u);
			u.PersonajeBase.AlMorir += RevisarEquipoGanador;
		}

		public bool RequiereInteracciónInmediata = false;

		public Campo (Point tamaño)
		{
			Unidades = new List<Unidad> ();
			Área = new Rectangle (Point.Zero, tamaño);
			DuraciónBatalla = TimeSpan.Zero;
			Paredes = new List<Pared> ();
			Paredes.Add (new Pared (
				this,
				new Vector2 (0, 0),
				new Vector2 (
					Área.Width,
					0)));
			Paredes.Add (new Pared (
				this,
				new Vector2 (Área.Width, 0),
				new Vector2 (
					Área.Width,
					Área.Height)));
			Paredes.Add (new Pared (
				this,
				new Vector2 (Área.Width, Área.Height),
				new Vector2 (0, Área.Height)));
			Paredes.Add (new Pared (
				this,
				new Vector2 (0, Área.Height),
				new Vector2 (0, 0)));
		}

		public Rectangle Área { get; private set; }

		public void Tick (TimeSpan delta)
		{
			TimeSpan realDelta;
			var turnoUnidad = UnidadesVivas.FirstOrDefault (x => x.OrdenActual == null);
			if (turnoUnidad != null)
			{
				UnidadActual = turnoUnidad;
				// Pedir orden al usuario o a la IA
				AlRequerirOrdenAntes?.Invoke (turnoUnidad);
				turnoUnidad.Interactor.Ejecutar ();
				turnoUnidad.Interactor.AlTerminar += () => AlRequerirOrdenDespués?.Invoke (turnoUnidad);
				return;
			}
			else
			{
				realDelta = delta;

				// Lógica aquí
				// No debe de haber cambios lógicos al menos que nadie requiera orden
				DuraciónBatalla += realDelta;
				foreach (var x in UnidadesVivas)
				{
					// Se supone que aquí cada unidad tiene una orden
					x.OrdenActual.Update (realDelta);
					if (RequiereInteracciónInmediata)
						break;
				}

				RequiereInteracciónInmediata = false;
				RecibirExp (realDelta);

				foreach (var x in UnidadesVivas)
				{
					x.AcumularPetición (realDelta);

					// Sus recursos
					foreach (var y in x.AtributosActuales.Enumerar)
					{
						y.Tick (realDelta);
					}

					// Experiencia por equipment
					foreach (var y in x.PersonajeBase.Equipamento)
					{
						y.BattleUpdate (realDelta);
					}

					// Empuje
					Empujes (realDelta);

					// IObjetos
					foreach (var o in Objetos)
					{
						o.Update (realDelta);
					}
				}

			}

		}

		public Unidad UnidadActual { get; private set; }

		/// <summary>
		/// Unidades vivas reciben exp
		/// </summary>
		void RecibirExp (TimeSpan delta)
		{
			var mins = delta.TotalMinutes;
			foreach (var uni in Unidades.Where (x => x.PuedeRecibirExp))
			{
				uni.RecibirExp (mins * ExpPorMinuto);
			}
		}

		/// <summary>
		/// Realiza los empujes de unidades
		/// </summary>
		public void Empujes (TimeSpan delta)
		{
			foreach (var u in UnidadesVivas)
			{
				foreach (var v in UnidadesVivas)
				{
					if (u != v)
					{
						Empujar (u, v, delta);
					}
				}
			}
		}

		static void Empujar (Unidad origen, Unidad destino, TimeSpan delta)
		{
			var vect = destino.PosPrecisa - origen.PosPrecisa;
			var dist = vect.LengthSquared ();
			vect.Normalize ();
			var usarCoef = (origen.Equipo.EsAliado (destino) ? origen.AtributosActuales.Empuje.HaciaAliado : origen.AtributosActuales.Empuje.HaciaEnemigo) * 300 / destino.AtributosActuales.Empuje.Masa;

			var Fuerza = usarCoef / dist;
			vect = vect * (Fuerza * (float)delta.TotalSeconds);
			destino.Mover (vect);
		}

		public Equipo? EquipoGanador { get; private set; }

		void RevisarEquipoGanador ()
		{
			Equipo? currEq = null;
			foreach (var x in UnidadesVivas)
			{
				if (!currEq.HasValue)
					currEq = x.Equipo;
				else
				{
					if (!currEq.Value.Equals (x.Equipo))
					{
						EquipoGanador = null;
						return;
					}
				}
			}
			EquipoGanador = currEq;
		}

		/// <summary>
		/// Lo que hace cuando acaba una batalla
		/// </summary>
		public void Terminar ()
		{
			// Los skills
			// Añadir nuevos desbloqueados
			foreach (var x in Unidades)
			{
				x.CommitExp ();
				foreach (var s in x.PersonajeBase.InnerSkill)
				{
					x.PersonajeBase.Desbloqueables.UnionWith (s.DesbloquearSkills ());
					x.PersonajeBase.Desbloqueables.ExceptWith (x.PersonajeBase.InnerSkill); // No agregar cosas que ya sé.
				}

				// Agregar los skills que ya se deben aprender.
				foreach (var sk in new List<ISkill> (x.PersonajeBase.Desbloqueables))
				{
					if (sk.PuedeAprender ())
					{
						x.PersonajeBase.Desbloqueables.Remove (sk);
						x.PersonajeBase.InnerSkill.Add (sk);
						sk.AlAprender ();
					}
				}
				x.PersonajeBase.LimpiarUnidad ();
			}

			// Pagar al clan ganador
			var ganador = EquipoGanador.Value;
			foreach (var x in GetEquipos())
			{
				if (!ganador.Equals (x))
				{
					ganador.Drops.Dinero += x.Drops.Dinero;
				}
			}

			AlTerminar?.Invoke ();
		}

		public ICollection<Equipo> GetEquipos ()
		{
			var ret = new List<Equipo> ();
			foreach (var x in Unidades)
			{
				if (!ret.Contains (x.Equipo))
					ret.Add (x.Equipo);
			}
			return ret;
		}

		/// <summary>
		/// Devuelve la unidad más cercana a una posición
		/// </summary>
		public Unidad UnidadMásCercana (Vector2 pos, Func<Unidad, bool> pred)
		{
			Unidad másCercana = null;
			double lastDistSq = double.PositiveInfinity;
			foreach (var x in UnidadesVivas.Where (pred))
			{
				var distSq = (x.PosPrecisa - pos).LengthSquared ();
				if (distSq < lastDistSq)
				{
					lastDistSq = distSq;
					másCercana = x;
				}
			}
			return másCercana;
		}

		/// <summary>
		/// Devuelve la unidad más cercana a una posición
		/// </summary>
		public Unidad UnidadMásCercana (Vector2 pos)
		{
			Unidad másCercana = null;
			double lastDistSq = double.PositiveInfinity;
			foreach (var x in UnidadesVivas)
			{
				var distSq = (x.PosPrecisa - pos).LengthSquared ();
				if (distSq < lastDistSq)
				{
					lastDistSq = distSq;
					másCercana = x;
				}
			}
			return másCercana;
		}

		public ICollection<Pared> Paredes { get; }

		public event Action<Unidad> AlRequerirOrdenAntes;
		public event Action<Unidad> AlRequerirOrdenDespués;
		public event Action AlTerminar;
	}
}