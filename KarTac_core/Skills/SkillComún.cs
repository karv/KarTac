using KarTac.Personajes;
using KarTac.Batalla;
using System.Collections.Generic;

namespace KarTac.Skills
{
	public abstract class SkillComún : ISkill
	{
		protected SkillComún (Personaje usuario)
		{
			Usuario = usuario;
		}

		protected SkillComún (Unidad usuario)
		{
			Usuario = usuario.PersonajeBase;
		}

		public Personaje Usuario { get; }

		public Unidad UnidadUsuario
		{
			get
			{
				return Usuario.Unidad;
			}
		}

		public double TotalExp { get; protected set; }

		public double PeticiónExpAcumulada { get; protected set; }

		public abstract string Nombre { get; }

		public abstract string IconTextureName { get; }

		public abstract string Descripción { get; }

		public Campo CampoBatalla
		{
			get
			{
				return Usuario.Unidad.CampoBatalla;
			}
		}

		public abstract IEnumerable<ISkill> DesbloquearSkills ();

		public abstract bool PuedeAprender ();

		public virtual void AlAprender ()
		{
		}

		public abstract void Ejecutar ();

		public abstract bool Usable { get; }

		public void CommitExp (double exp)
		{
			TotalExp += exp;
			PeticiónExpAcumulada = 0;
		}

		protected virtual void OnTerminar (ISkillReturnType returnInfo)
		{
			Usuario.Unidad.OrdenActual = null;
			AlTerminarEjecución?.Invoke (returnInfo);
		}

		public event System.Action<ISkillReturnType> AlTerminarEjecución;

		#region Guardable

		public virtual void Guardar (System.IO.BinaryWriter writer)
		{
			// No se requiere guardar usuario
			writer.Write (GetType ().FullName);
			writer.Write (TotalExp);
		}

		public TObj Cargar<TObj> (System.IO.BinaryReader reader)
		{
			throw new System.NotImplementedException ();
		}

		#endregion
	}
}