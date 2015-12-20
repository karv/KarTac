using System.Collections.Generic;
using KarTac.IO;

namespace KarTac.Equipamento
{
	public interface IItem : IGuardable
	{
		/// <summary>
		/// Tags
		/// </summary>
		ISet<string> Tags { get; }


	}
}