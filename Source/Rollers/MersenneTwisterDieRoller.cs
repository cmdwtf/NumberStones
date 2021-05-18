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

		/// <inheritdoc cref="IDieRoller.Information"/>
		public override string Information => $"MersenneTwister 19937 ([source](https://github.com/cmdwtf/NumberStones/blob/main/Source/Random/MersenneTwister19937.cs))";
	}
}
