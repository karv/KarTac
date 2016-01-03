using System.Collections.Generic;
using KarTac.Batalla;
using KarTac.Cliente.Controls;
using System;
using Microsoft.Xna.Framework;
using KarTac.Skills;

namespace KarTac.Cliente.Controls.Screens
{
	public class BattleScreen : Screen
	{
		public List<UnidadSprite> Unidades { get; private set; }

		public Campo CampoBatalla { get; }

		public BattleScreen (KarTacGame juego, Campo campo)
			: base (juego)
		{
			CampoBatalla = campo;
			contadorTiempo = new Label (this);
			contadorTiempo.Texto = () => CampoBatalla.DuraciónBatalla.ToString ("ss\\.fff");
			contadorTiempo.UseFont = @"UnitNameFont";
			contadorTiempo.Color = Color.White;
			contadorTiempo.Posición = new Point (0, 20);
			contadorTiempo.Include ();
		}

		Label contadorTiempo;

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
			CampoBatalla.Tick (gameTime.ElapsedGameTime);

			if (CampoBatalla.EquipoGanador != null)
			{
				CampoBatalla.Terminar ();
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
				u.AlSerBlanco += x => HacerDamageInfoString (x);
			}

			base.Inicializar ();
		}

		public VanishingString HacerDamageInfoString (ISkillReturnType sklRet)
		{
			if (sklRet.Color.HasValue)
			{
				var texto = Math.Abs (sklRet.Delta).ToString ();

				var mostrarDaño = new VanishingString (Game, texto, TimeSpan.FromSeconds (1));
				mostrarDaño.LoadContent ();
				mostrarDaño.ColorInicial = sklRet.Color.Value;
				mostrarDaño.Centro = sklRet.Loc.ToVector2 ();
				mostrarDaño.Include ();
				return mostrarDaño;
			}
			return null;
		}
	}
}