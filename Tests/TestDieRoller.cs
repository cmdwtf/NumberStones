using System;

namespace cmdwtf.NumberStones.Tests
{
	public class TestDieRoller : Rollers.IDieRoller
	{
		public long Range { get; }

		public int RollDie(int sides) => throw new NotImplementedException();
	}
}
