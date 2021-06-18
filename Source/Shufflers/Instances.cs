namespace cmdwtf.NumberStones.Shufflers
{
	/// <summary>
	/// Easy to use instances of the different available shufflers.
	/// </summary>
	public static class Instances
	{
		/// <summary>
		/// The default shuffler. Currently an instance of <see cref="FisherYatesShuffler"/>
		/// </summary>
		public static IShuffler DefaultShuffler => FisherYatesShuffler;

		/// <summary>
		/// A shuffler that will not perform any shuffling.
		/// </summary>
		public static IShuffler NopShuffler { get; } = new NopShuffler();

		/// <summary>
		/// A shuffler based on the Fisher Yates shuffling algorithm with the Mersenne Twister "MT19937" PRNG algorithm.
		/// </summary>
		public static IShuffler FisherYatesShuffler { get; } = new FisherYatesShuffler();
	}
}
