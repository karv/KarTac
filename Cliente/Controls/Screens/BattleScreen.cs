using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;


namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		public List<UnidadSprite> Unidades { get; }

		public Campo CampoBatalla { get; }

		public BattleScreen (KarTacGame juego, Campo campo)
			: base (juego)
		{
			CampoBatalla = campo;
		}

		/// <summary>
		/// Agrega una unidad
		/// </summary>
		/// <returns>Devuelve el control (recién creado) asociado a la unidad</returns>
		[Obsolete ("Usar Campo.Unidades")]
		public UnidadSprite AgregaUnidad (KarTac.Batalla.Unidad unit)
		{
			var ret = new UnidadSprite (this, unit);
			ret.Inicializar ();
			ret.Include ();
			return ret;
		}

		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update (gameTime);
			// Ejecutar órdenes
			foreach (var x in CampoBatalla.Unidades)
			{
				var ord = x.OrdenActual;
				if (ord != null)
					ord.Update (gameTime);
				else
				{
					// Pedir orden al usuario o a la IA
					var y = Unidades.Find (z => z.UnidadBase == x);
					y.Marcado = true;
					x.Interactor.Ejecutar ();
					y.Marcado = false;
				}
			}
		}

		public override void Inicializar ()
		{
			// Crear sprites de unidades
			Unidades = new List<UnidadSprite> (CampoBatalla.Unidades.Count);
			foreach (var u in CampoBatalla.Unidades)
			{
				var sprite = new UnidadSprite (this, u);
				Unidades.Add (sprite);
				sprite.Include ();
			}
			base.Inicializar ();
		}
	}
}
