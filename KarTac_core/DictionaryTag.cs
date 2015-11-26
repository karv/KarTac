using System.Collections.Generic;

namespace KarTac
{
	public class DictionaryTag: Dictionary<IExp, float>, ITagging
	{
		public float TagValue (IExp tag)
		{
			return this [tag];
		}

		public IEnumerable<IExp> Tags
		{
			get
			{
				return Keys;
			}
		}
	}
}