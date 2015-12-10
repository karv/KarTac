using KarTac.Personajes;
using KarTac.Batalla;
using KarTac.Batalla.Shape;

namespace KarTac.Skills
{
	public abstract class SkillTresPasosShaped: SkillTresPasos
	{
		protected SkillTresPasosShaped (Personaje usuario)
			: base (usuario)
		{
		}

		protected SkillTresPasosShaped (Unidad usuario)
			: base (usuario)
		{
		}

		public abstract IShape GetÁrea ();

		protected override bool SeleccionaTarget (Unidad u)
		{
			return GetÁrea ().Contiene (u.Pos);
		}
	}
}