using System.Collections.Generic;

namespace cmdwtf.NumberStones.Shufflers
{
	/// <summary>
	/// A random shuffler based on the Fisher Yates algorithm.
	/// </summary>
	public class FisherYatesShuffler : RandomShufflerBase
	{
		/// <inheritdoc cref="IShuffler.Information"/>
		public override string Information => $"Fisher Yates via {Random.GetType().Name}";

		/// <inheritdoc cref="IShuffler.Shuffle{T}(IList{T})"/>
		public override void Shuffle<T>(IList<T> target)
		{
			int itemCount = target.Count;

			if (itemCount < 2)
			{
				return;
			}

			for (int scan = itemCount - 1; scan > 0; scan--)
			{
				int nextSwap = Random.Next(scan);

				if (nextSwap == scan)
				{
					continue;
				}

				T tmp = target[nextSwap];
				target[nextSwap] = target[scan];
				target[scan] = tmp;
			}
		}
	}
}
