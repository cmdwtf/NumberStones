
using cmdwtf.NumberStones.Random;

namespace cmdwtf.NumberStones.Rollers
{
	public class DotNetDieRoller : IDieRoller
	{
		protected readonly IRandom _random;

		public DotNetDieRoller() : this(Random.Instances.DefaultRandom)
		{ }

		protected DotNetDieRoller(IRandom customRandom)
		{
			_random = customRandom;
		}

		public virtual int RollDie(int sides)
		{
			if (sides == 0)
			{
				return 0;
			}

			return _random.Next(0, sides) + 1;
		}
	}
}