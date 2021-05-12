namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// Easy to use instances of the different available rollers.
	/// </summary>
	public static class Instances
	{
		/// <summary>
		/// The default roller. Currently an instance of <see cref="MersenneTwisterDieRoller"/>
		/// </summary>
		public static IDieRoller DefaultRoller => MersenneTwisterRoller;

		/// <summary>
		/// A die roller based on <see cref="System.Random"/>
		/// </summary>
		public static IDieRoller DotNetRoller { get; } = new DotNetDieRoller();

		/// <summary>
		/// A die roller that will always return the maximum number from a die.
		/// </summary>
		public static IDieRoller MaxRoller { get; } = new MaxDieRoller();

		/// <summary>
		/// A die roller that will always return the minimum number from a die.
		/// </summary>
		public static IDieRoller MinRoller { get; } = new MinDieRoller();

		/// <summary>
		/// A roller based on the Mersenne Twister "MT19937" PRNG algorithm.
		/// </summary>
		public static IDieRoller MersenneTwisterRoller { get; } = new MersenneTwisterDieRoller();
	}
}
