
namespace cmdwtf.NumberStones.Random
{
	/// <summary>
	/// A static class that collects instances of RNG implementations.
	/// </summary>
	public static class Instances
	{
		/// <summary>
		/// The default random number generator to use. Currently the <see cref="Mt19937"/> PRNG.
		/// </summary>
		public static IRandom DefaultRandom => Mt19937;

		/// <summary>
		/// A random number generator based on the <see cref="System.Random"/> RNG.
		/// </summary>
		public static IRandom DotNet { get; } = new DotNetRandom();

		/// <summary>
		/// A random number generator based on the Mersenne Twister 19937 PRNG algorithm.
		/// </summary>
		public static IRandom Mt19937 { get; } = new MersenneTwister19937();
	}
}
