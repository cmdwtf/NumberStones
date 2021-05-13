using System;

namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// A die roller that always returns the lowest value on the die
	/// </summary>
	public sealed class MinDieRoller : IDieRoller
	{
		/// <summary>
		/// Roll the "Min" die
		/// </summary>
		/// <param name="sides">The number of sides on the die to roll</param>
		/// <returns>1, unless the number of sides on the die is 0, in which case 0</returns>
		public int RollDie(int sides) => Math.Min(sides, 1);

		/// <summary>
		/// The difference between a minimum and maximum roll is always 0, as the roll is fixed.
		/// </summary>
		public long Range => 0;
	}
}