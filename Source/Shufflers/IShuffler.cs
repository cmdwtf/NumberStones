using System.Collections.Generic;

namespace cmdwtf.NumberStones.Shufflers
{
	/// <summary>
	/// An interface representing an object that can shuffle a list of items.
	/// </summary>
	public interface IShuffler
	{
		/// <summary>
		/// Shuffles a list of items.
		/// </summary>
		/// <param name="deck">The items to shuffle.</param>
		void Shuffle<T>(IList<T> target);

		/// <summary>
		/// User-presentable information about the shuffler used. May be in basic markdown.
		/// </summary>
		string Information { get; }
	}
}
