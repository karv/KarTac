using System;
using KarTac.Skills;
using KarTac.Batalla.Orden;


namespace KarTac.Equipamento
{
	public class HpPoción : Equipamento
	{
		public class Efecto : SkillComún
		{
			public readonly IEquipamento ItemBase;

			public Efecto (KarTac.Personajes.Personaje usuario, IEquipamento itemBase)
				: base (usuario)
			{
				ItemBase = itemBase;
			}

			public override System.Collections.Generic.IEnumerable<ISkill> DesbloquearSkills ()
			{
				yield break;
			}

			public override string Descripción
			{
				get
				{
					return "Poción que cura 100 HP";
				}
			}

			public override void Ejecutar ()
			{
				UnidadUsuario.AtributosActuales.HP.Valor += 100;
				UnidadUsuario.PersonajeBase.Equipamento.Remove (ItemBase);
				var rt = new SkillReturnType (
					         100,
					         UnidadUsuario.AtributosActuales.HP,
					         UnidadUsuario.Pos);
				UnidadUsuario.OnSerBlanco (rt);
				UnidadUsuario.OrdenActual = new Quieto (
					UnidadUsuario,
					TimeSpan.FromMilliseconds (120));
			}

			public override string IconTextureName
			{
				get
				{
					return ItemBase.IconContentString;
				}
			}

			public override string Nombre
			{
				get
				{
					throw new NotImplementedException ();
				}
			}

			public override bool PuedeAprender ()
			{
				return false;
			}

			public override bool Usable
			{
				get
				{
					return true;
				}
			}



			
		}

		public override string IconContentString
		{
			get
			{
				return "Rect";
			}
		}

		public override string Nombre
		{
			get
			{
				return "Poción";
			}
		}

		protected override System.Collections.Generic.IEnumerable<ISkill> Skills
		{
			get
			{
				yield return new Efecto (Portador, this);
			}
		}

		public override System.Collections.Generic.IEnumerable<string> Tags
		{
			get
			{
				yield return "poción";
				yield return "item";
			}
		}
	}
}