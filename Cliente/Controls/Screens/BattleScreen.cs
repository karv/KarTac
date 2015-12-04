using System.Collections.Generic;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;
using MonoGame.Extended.BitmapFonts;


namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		public List<UnidadSprite> Unidades { get; private set; }

		public Campo CampoBatalla { get; }

		public bool MostrarFps = true;

		BitmapFont FpsFont;

		public BattleScreen (KarTacGame juego, Campo campo)
			: base (juego)
		{
			CampoBatalla = campo;

			CampoBatalla.AlRequerirOrdenAntes += delegate(Unidad x)
			{
				var y = Unidades.Find (z => z.UnidadBase == x);
				y.Marcado = true;
			};

			CampoBatalla.AlRequerirOrdenDespués += delegate(Unidad x)
			{
				var y = Unidades.Find (z => z.UnidadBase == x);
				y.Marcado = false;
			};

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
			CampoBatalla.Tick (gameTime);
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

		public override void Dibujar (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Dibujar (gameTime);

			if (MostrarFps)
			{
				
			}
		}
	}
}
