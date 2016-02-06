using System;
using KarTac.Skills;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using KarTac.Personajes;
using Moggle.Controles;
using Moggle.Controles.Listas;
using Moggle.Screens;

namespace KarTac.Controls
{
	public class ListaSkills : ContenedorBotón, IListaControl
	{
		public ListaSkills (IScreen screen)
			: base (screen)
		{
		}

		public ISkill SkillSeleccionado
		{
			get{ return skillData [ÍndiceSkillSel]; }
		}

		int índiceSkillSel;

		int numUsables;

		/// <summary>
		/// Devuelve o establece el índice del skill seleccionado, bajo el orden mostrado
		/// </summary>
		public int ÍndiceSkillSel
		{
			get
			{ 
				return índiceSkillSel; 
			}
			set
			{
				var nuevoInd = Math.Min (Math.Max (value, 0), Count - 1);
				BotónEnÍndice (ÍndiceSkillSel).Color = 
					getSkillColor (false, BotónEnÍndice (ÍndiceSkillSel).Habilidato);
				índiceSkillSel = nuevoInd;
				BotónEnÍndice (ÍndiceSkillSel).Color = 
					getSkillColor (true, BotónEnÍndice (ÍndiceSkillSel).Habilidato);
				AlCambiarSelección?.Invoke ();
			}
		}

		#region Colores

		static Color getSkillColor (bool selected, bool habil)
		{
			var ret = selected ? skillSelColor : skillNoSelColor;
			if (!habil)
				ret = new Color (ret.R / 2 + 128, ret.G / 2 + 128, ret.B / 2 + 128, ret.A);
			return ret;
		}

		static Color skillNoSelColor
		{
			get{ return Color.Red; }
		}

		static Color skillSelColor
		{
			get{ return Color.Green; }
		}

		#endregion

		#region Lista control

		void IListaControl.SeleccionaSiguiente ()
		{
			ÍndiceSkillSel++;
		}

		void IListaControl.SeleccionaAnterior ()
		{
			ÍndiceSkillSel--;
		}

		object IListaControl.Seleccionado
		{
			get
			{
				return SkillSeleccionado;
			}
		}

		#endregion

		readonly List<ISkill> skillData = new List<ISkill> ();

		public void Add (ISkill sk)
		{
			Botón bt;
			if (sk.Usable)
			{
				skillData.Insert (numUsables, sk);
				bt = Add (numUsables++);
				bt.Color = Color.Red;
			}
			else
			{
				skillData.Add (sk);
				bt = Add ();
				bt.Habilidato = false;
				bt.Color = Color.Gray;
			}
			bt.Textura = sk.IconTextureName;
		}

		public void Populate (Personaje pj)
		{
			Clear ();
			foreach (var x in pj.Skills)
			{
				Add (x);
			}
		}

		public new void Clear ()
		{
			base.Clear ();
			skillData.Clear ();
			numUsables = 0;
		}

		public event Action AlCambiarSelección;
	}
}

