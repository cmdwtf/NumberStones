namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// #DOC
	/// </summary>
	public static class Instances
	{
		public static IDieRoller DefaultRoller => MersenneTwisterRoller;
		public static readonly IDieRoller DotNetRoller = new DotNetDieRoller();
		public static readonly IDieRoller MaxRoller = new MaxDieRoller();
		public static readonly IDieRoller MinRoller = new MinDieRoller();
		public static readonly IDieRoller MersenneTwisterRoller = new MersenneTwisterDieRoller();
	}
}
