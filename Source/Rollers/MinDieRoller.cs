using System;

namespace cmdwtf.NumberStones.Rollers
{
	public sealed class MinDieRoller : IDieRoller
	{
		public int RollDie(int sides) => Math.Min(sides, 1);
	}
}