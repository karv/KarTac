using System.Collections.Generic;

namespace KarTac
{
	/// <summary>
	/// Representa un objeto que tiene tags
	/// </summary>
	public interface ITagging
	{
		IEnumerable<IExperimentable> Tags { get; }

		float TagValue (IExperimentable tag);
	}
}