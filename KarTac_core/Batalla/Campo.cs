using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using KarTac.Skills;

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
		public float ExpPorMinuto = 1;

		public ISelectorTarget SelectorTarget { get; set; }

		/// <summary>
		/// Unidades en el campo
		/// </summary>
		/// <value>The unidades.</value>
		public ICollection<Unidad> Unidades { get; }

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
			var r = new Random ();
			Unidades.Add (u);
			u.PosPrecisa = randomPointInRectangle (Área, r);
			u.PersonajeBase.AlMorir += RevisarEquipoGanador;
		}

		public Campo (Point tamaño)
		{
			Unidades = new List<Unidad> ();
			Área = new Rectangle (Point.Zero, tamaño);
		}

		public Rectangle Área { get; private set; }

		public void Tick (GameTime delta)
		{
			foreach (var x in UnidadesVivas)
			{
				var ord = x.OrdenActual;
				if (ord != null)
					ord.Update (delta);
				else
				{
					UnidadActual = x;
					// Pedir orden al usuario o a la IA
					AlRequerirOrdenAntes?.Invoke (x);
					x.Interactor.Ejecutar ();
					x.Interactor.AlTerminar += () => AlRequerirOrdenDespués?.Invoke (x);
				}
			}


			RecibirExp (delta.ElapsedGameTime);

			foreach (var x in UnidadesVivas)
			{
				x.AcumularPetición (delta.ElapsedGameTime);
				// Sus recursos
				foreach (var y in x.AtributosActuales.Recs)
				{
					y.Tick (delta);
				}
			}

			// Empuje
			Empujes (delta.ElapsedGameTime);
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
			var usarCoef = (origen.Equipo.EsAliado (destino) ? origen.AtributosActuales.Empuje.HaciaAliado : origen.AtributosActuales.Empuje.HaciaEnemigo) * 1000 / destino.AtributosActuales.Empuje.Masa;

			var Fuerza = usarCoef / dist;
			vect = vect * (Fuerza * (float)delta.TotalSeconds);
			destino.PosPrecisa += vect;

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
				foreach (var s in x.PersonajeBase.Skills)
				{
					x.PersonajeBase.Desbloqueables.UnionWith (s.DesbloquearSkills ());
				}

				// Agregar los skills que ya se deben aprender.
				foreach (var sk in new List<ISkill> (x.PersonajeBase.Desbloqueables))
				{
					if (sk.PuedeAprender ())
					{
						x.PersonajeBase.Desbloqueables.Remove (sk);
						x.PersonajeBase.Skills.Add (sk);
					}
				}
			}

			AlTerminar?.Invoke ();
		}

		static Vector2 randomPointInRectangle (Rectangle rect, Random r)
		{
			return new Vector2 (
				rect.Left + (float)r.NextDouble () * rect.Width,
				rect.Top + (float)r.NextDouble () * rect.Height
			);
		}

		public event Action<Unidad> AlRequerirOrdenAntes;
		public event Action<Unidad> AlRequerirOrdenDespués;
		public event Action AlTerminar;
	}
}