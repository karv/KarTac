﻿using System;

namespace KarTac.Recursos
{
	public class Maná : RecursoAcotadoRegenerativo
	{
		public Maná ()
		{
			Max = 5;
			Valor = Max;
		}

		public override string Icono
		{
			get
			{
				return @"Icons/Recursos/Maná"; 
			}
		}

		public override void CommitExp (double exp)
		{
			Max += 4 * (float)exp;
			PeticiónExpAcumulada = 0;
		}

		public override string Nombre
		{
			get
			{
				return "Maná";
			}
		}

		public override bool VisibleBatalla
		{
			get
			{
				return true;
			}
		}

		protected override void PedirExp (TimeSpan time, KarTac.Batalla.Campo campo)
		{
			var pct = Valor / Max;

			PeticiónExpAcumulada += (1 - pct) * time.TotalMinutes;
		}

		protected override float Regen
		{
			get
			{
				return Max;
			}
		}
	}
}