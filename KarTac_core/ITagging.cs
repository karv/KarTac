using System.Collections.Generic;

namespace KarTac
{
	/// <summary>
	/// Representa un objeto que tiene tags
	/// </summary>
	public interface ITagging
	{
		IEnumerable<string> Tags { get; }

		float TagValue (string tag);
	}
}