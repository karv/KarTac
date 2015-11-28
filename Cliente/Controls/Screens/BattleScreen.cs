using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using KarTac.Cliente.Controls;

namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		public List<UnidadSprite> Unidades { get; }

		public BottomMenu Menú { get; }

		public KarTac.Batalla.Unidad UnidadActual
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

		public BattleScreen (KarTacGame juego)
			: base (juego)
		{
			Menú = new BottomMenu (this);
			Menú.Include ();
		}

		/// <summary>
		/// Agrega una unidad
		/// </summary>
		/// <returns>Devuelve el control (recién creado) asociado a la unidad</returns>
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
		}
	}
}
