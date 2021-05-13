
namespace cmdwtf.NumberStones.Random
{
	/// <summary>
	/// A static class that collects instances of RNG implementations.
	/// </summary>
	public static class Instances
	{
		/// <summary>
		/// A random number generator based on the <see cref="System.Random"/> RNG.
		/// </summary>
		/// CA2104 is thrown incorrectly on this line. Singleton.DefaultRandom is immutable.
		public static IRandom DotNet { get; } = new DotNetRandom();

		/// <summary>
		/// A random number generator based on the Mersenne Twister 19937 PRNG algorithm.
		/// </summary>
		public static IRandom Mt19937 { get; } = new MersenneTwister19937();
	}
}
