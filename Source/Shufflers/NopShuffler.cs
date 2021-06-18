using System.Collections.Generic;

namespace cmdwtf.NumberStones.Shufflers
{
	/// <summary>
	/// A do-nothing (no operation) shuffler.
	/// </summary>
	public sealed class NopShuffler : IShuffler
	{
		/// <inheritdoc cref="IShuffler.Information"/>
		public string Information => "Not Shuffled";

		/// <inheritdoc cref="IShuffler.Shuffle{T}(IList{T})"/>
		public void Shuffle<T>(IList<T> target) { }
	}
}
