﻿using System.Collections.Generic;

namespace KarTac.Equipamento
{
	public class InventarioClan : List<IItem>
	{
		public int Dinero { get; set; }
	}
}