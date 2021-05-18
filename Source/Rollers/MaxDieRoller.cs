namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// A die roller that always returns the number of sides on the die.
	/// </summary>
	public sealed class MaxDieRoller : IDieRoller
	{
		/// <summary>
		/// Roll the "Max" die
		/// </summary>
		/// <param name="sides">The number of sides on the die to roll</param>
		/// <returns>The number of sides on the die</returns>
		public int RollDie(int sides) => sides;

		/// <summary>
		/// The difference between a minimum and maximum roll is always 0, as the roll is fixed.
		/// </summary>
		public long Range => 0;

		/// <inheritdoc cref="IDieRoller.Information"/>
		public string Information => $"{nameof(MaxDieRoller)}: Forces minimum dice roll.";
	}
}