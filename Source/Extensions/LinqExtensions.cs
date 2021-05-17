using System.Collections.Generic;
using System.Linq;

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
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
		{
			while (source.Any())
			{
				yield return source.Take(chunksize);
				source = source.Skip(chunksize);
			}
		}
	}
}
