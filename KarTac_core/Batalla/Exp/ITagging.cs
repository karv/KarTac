using System.Collections.Generic;

namespace KarTac.Batalla.Exp
{
	/// <summary>
	/// Representa un objeto que tiene tags
	/// </summary>
	public interface ITagging
	{
		IEnumerable<IExp> Tags { get; }

		double TagValue (IExp tag);
	}
}