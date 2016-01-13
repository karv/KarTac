﻿using System.Collections.Generic;
using KarTac.IO;
using System.IO;

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
				default:
					throw new IOException ();
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

		string Nombre { get; }
	}
}