namespace cmdwtf.NumberStones.Rollers
{
	public sealed class MersenneTwisterDieRoller : DotNetDieRoller
	{
		public MersenneTwisterDieRoller() : base(new Random.MersenneTwister19937())
		{

		}
	}
}
