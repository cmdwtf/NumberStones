
using cmdwtf.NumberStones.Random;

namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// A dice roller based on the <see cref="System.Random"/> RNG. Optionally,
	/// this class may be overriden with any RNG that implements
	/// <see cref="IRandom"/>.
	/// </summary>
	public class DotNetDieRoller : IDieRoller
	{
		protected readonly IRandom _random;

		/// <summary>
		/// Constructs a new roller
		/// </summary>
		public DotNetDieRoller() : this(Random.Instances.DotNet)
		{ }

		protected DotNetDieRoller(IRandom customRandom)
		{
			_random = customRandom;
		}

		/// <summary>
		/// Rolls a die with the given amount of sides, returning
		/// a random number based on the number of potential sides
		/// </summary>
		/// <param name="sides">The number of sides of the dice to roll</param>
		/// <returns>A number between 1 and the number of sides, or 0 if there are no sides</returns>
		public virtual int RollDie(int sides)
		{
			if (sides == 0)
			{
				return 0;
			}

			return _random.Next(0, sides) + 1;
		}

		/// <summary>
		/// The range of this roller
		/// </summary>
		public virtual long Range => int.MaxValue;
	}
}