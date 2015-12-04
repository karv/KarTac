using System.Collections.Generic;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;
using Microsoft.Xna.Framework;


namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen: Screen
	{
		#if FPS
		readonly Label fpsLabel;
		#endif

		public List<UnidadSprite> Unidades { get; private set; }

		public Campo CampoBatalla { get; }

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

			#if FPS
			fpsLabel = new Label (this);
			fpsLabel.Texto = () => string.Format ("fps: {0}", juego.Fps.AverageFramesPerSecond);
			fpsLabel.UseFont = @"UnitNameFont";
			fpsLabel.Color = Color.White;
			#endif
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

			#if FPS
			fpsLabel.Include ();
			#endif

			base.Inicializar ();
		}
	}
}
