﻿using System.Collections.Generic;

namespace KarTac.Batalla.Exp
{
	public class DictionaryTag: Dictionary<IExp, double>, ITagging
	{
		public double TagValue (IExp tag)
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