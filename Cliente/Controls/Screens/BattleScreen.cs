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

		public BottomMenu Menú { get; }

		public Campo CampoBatalla { get; }

		public Unidad UnidadActual
		{
			get
			{
				return Menú.UnidadActual;
			}
			set
			{
				Menú.UnidadActual = value;
			}
		}

		public BattleScreen (KarTacGame juego, Campo campo)
			: base (juego)
		{
			Menú = new BottomMenu (this);
			Menú.Include ();
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
			var keyb = Keyboard.GetState ();
			if (keyb.IsKeyDown (Keys.Down))
			{
				Menú.ÍndiceSkillSel++;
			}
			if (keyb.IsKeyDown (Keys.Up))
			{
				Menú.ÍndiceSkillSel--;
			}
			if (keyb.IsKeyDown (Keys.Enter))
			{
				Menú.SkillSeleccionado.Ejecutar (UnidadActual, CampoBatalla);
			}

			// Ejecutar órdenes
			foreach (var x in CampoBatalla.Unidades)
			{
				var ord = x.OrdenActual;
				if (ord != null)
					ord.Update (gameTime);
				else
				{
					
				}
			}
		}

		public override void Inicializar ()
		{
			// Crear sprites de unidades
			foreach (var u in CampoBatalla.Unidades)
			{
				var sprite = new UnidadSprite (this, u);
				sprite.Include ();
			}
			base.Inicializar ();
		}
	}
}
