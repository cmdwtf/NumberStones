namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// A die roller based on the Mersenne Twister 19937 PRNG algorithm.
	/// </summary>
	public sealed class MersenneTwisterDieRoller : DotNetDieRoller
	{
		/// <summary>
		/// Constructs a new <see cref="MersenneTwisterDieRoller"/>
		/// </summary>
		public MersenneTwisterDieRoller() : base(Random.Instances.Mt19937)
		{

		}
	}
}
