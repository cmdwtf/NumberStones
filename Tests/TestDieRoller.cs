﻿using System;

namespace cmdwtf.NumberStones.Tests
{
	/// <summary>
	/// A die roller that can be used to feed specific values for testing.
	/// </summary>
	public class TestDieRoller : Rollers.IDieRoller
	{
		public long Range { get; }

		public int RollDie(int sides) => throw new NotImplementedException();
	}
}
