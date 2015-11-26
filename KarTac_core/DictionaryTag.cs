using System.Collections.Generic;

namespace KarTac
{
	public class DictionaryTag: Dictionary<IExperimentable, float>, ITagging
	{
		public float TagValue (IExperimentable tag)
		{
			return this [tag];
		}

		public IEnumerable<IExperimentable> Tags
		{
			get
			{
				return Keys;
			}
		}
	}
}