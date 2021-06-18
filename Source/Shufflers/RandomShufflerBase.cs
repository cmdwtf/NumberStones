using System.Collections.Generic;

using cmdwtf.NumberStones.Random;

namespace cmdwtf.NumberStones.Shufflers
{
	/// <summary>
	/// A base for a shuffler based on randomness of some type.
	/// </summary>
	public abstract class RandomShufflerBase : IShuffler
	{
		/// <inheritdoc cref="IShuffler.Information"/>
		public virtual string Information => $"{GetType().Name} via {Random.GetType().Name}";

		/// <summary>
		/// Creates a new random shuffler with the default random source.
		/// </summary>
		public RandomShufflerBase() : this(NumberStones.Random.Instances.DefaultRandom)
		{ }

		/// <summary>
		/// Creates a new random shuffler with a specified random source.
		/// </summary>
		/// <param name="random">The random source to use.</param>
		protected RandomShufflerBase(IRandom random)
		{
			Random = random;
		}

		/// <summary>
		/// The source of entropy to use with this shuffler.
		/// </summary>
		protected IRandom Random { get; private init; }

		/// <inheritdoc cref="IShuffler.Shuffle{T}(IList{T})"/>
		public abstract void Shuffle<T>(IList<T> target);
	}
}
