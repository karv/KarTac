using KarTac.Batalla;
using System;

namespace KarTac.Buff
{
	public class BuffPielPiedra : BuffCronometrado
	{
		public BuffPielPiedra (Unidad portador)
			: base (portador)
		{
		}

		public new Unidad Portador
		{
			get
			{
				return base.Portador as Unidad;
			}
		}

		protected override KarTac.Batalla.Exp.ITagging ExpTags
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		public override string Nombre
		{
			get
			{
				return "Piel de piedra";
			}
		}

		IModificador defMod;

		/// <summary>
		/// Establece la defensa del buff.
		/// </summary>
		public float BuffDefMod { set; private get; }

		public override void Insertar ()
		{
			defMod = Portador.AtributosActuales.AddModifier ("Defensa", BuffDefMod);
		}

		protected override void Terminar ()
		{
			Portador.AtributosActuales.RemoveModifier (defMod);
		}

		/// <summary>
		/// Duración del buff
		/// </summary>
		/// <value>The tiempo inicial.</value>
		public override TimeSpan TiempoInicial { get; set; }
	}
}