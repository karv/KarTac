using System.Collections.Generic;

namespace KarTac
{
	public class DictionaryTag: Dictionary<string, float>, ITagging
	{
		public float TagValue (string tag)
		{
			return this [tag];
		}

		public IEnumerable<string> Tags
		{
			get
			{
				return Keys;
			}
		}
	}
}