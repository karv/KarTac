using System.Collections.Generic;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		public List<UnidadSprite> Unidades { get; private set; }

		public Campo CampoBatalla { get; }

		public BattleScreen (KarTacGame juego, Campo campo)
			: base (juego)
		{
			CampoBatalla = campo;
		}

		public override Color BgColor
		{
			get
			{
				return Color.Green;
			}
		}

		/// <summary>
		/// Agrega una unidad
		/// </summary>
		/// <returns>Devuelve el control (recién creado) asociado a la unidad</returns>
		[Obsolete ("Usar Campo.Unidades")]
		public UnidadSprite AgregaUnidad (Unidad unit)
		{
			var ret = new UnidadSprite (this, unit);
			ret.Inicializar ();
			ret.Include ();
			return ret;
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			// Ejecutar órdenes
			CampoBatalla.Tick (gameTime);

			if (CampoBatalla.EquipoGanador != null)
			{
				CampoBatalla.Terminar ();
				AlTerminarBatalla?.Invoke ();
			}
		}

		public override void Inicializar ()
		{
			// Crear sprites de unidades
			Unidades = new List<UnidadSprite> (CampoBatalla.Unidades.Count);
			LoadContent ();
			foreach (var u in CampoBatalla.Unidades)
			{
				var sprite = new UnidadSprite (this, u);
				sprite.LoadContent ();
				Unidades.Add (sprite);
				sprite.Include ();
			}

			base.Inicializar ();
		}

		public event Action AlTerminarBatalla;
	}
}