using System.Collections.Generic;
using System.Linq;

using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Extensions
{
	/// <summary>
	/// Extensions useful for linq operations.
	/// https://stackoverflow.com/a/6362642
	/// </summary>
	internal static class LinqExtensions
	{
		/// <summary>
		/// Break a list of items into chunks of a specific size
		/// </summary>
		/// <param name="source">The enumerable to operate on.</param>
		/// <returns>Chunks of the specifed size</returns>
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
		{
			while (source.Any())
			{
				yield return source.Take(chunksize);
				source = source.Skip(chunksize);
			}
		}

		/// <summary>
		/// A shortcut to easily collate a collection of dice booleans.
		/// </summary>
		/// <param name="source">The enumerable to operate on.</param>
		/// <returns>The collated result.</returns>
		public static DiceBoolean Collate(this IEnumerable<DiceBoolean> source)
			=> DiceBoolean.Collate(source);
	}
}
