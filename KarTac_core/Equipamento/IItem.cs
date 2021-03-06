﻿using System.Collections.Generic;
using KarTac.IO;
using System.IO;
using System;

namespace KarTac.Equipamento
{
	public static class Lector
	{
		public static IItem Cargar (BinaryReader reader)
		{
			IItem ret;
			var recNombre = reader.ReadString ();

			switch (recNombre)
			{
				case "Espada":
					ret = new EqEspada ();
					break;
				case "Arco corto":
					ret = new Arco ();
					break;
				case "Poción":
					ret = new HpPoción ();
					break;
				case "Armadura de cuero":
					ret = new ArmaduraCuero ();
					break;
				case "Casco de cuero":
					ret = new CascoCuero ();
					break;
				case "Bastón":
					ret = new Bastón ();
					break;
				case "Lanza":
					ret = new Lanza ();
					break;
				case "Hacha":
					ret = new Hacha ();
					break;
				default:
					throw new IOException (recNombre + " no es un item.");
			}
			ret.Cargar (reader);
			return ret;
		}
	}

	public interface IItem : IGuardable
	{
		/// <summary>
		/// Tags
		/// </summary>
		ISet<string> Tags { get; }

		string Id { get; }

		string NombreCorto { get; }
	}
}