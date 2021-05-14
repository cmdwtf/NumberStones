namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// An interface representing an object that can simulate dice rolls.
	/// </summary>
	public interface IDieRoller
	{
		/// <summary>
		/// The difference between the minimum and maximum value of this roller.
		/// </summary>
		long Range { get; }

		/// <summary>
		/// Rolls the die of specified sides.
		/// </summary>
		/// <param name="sides">The number of sides of the die to roll</param>
		/// <returns>A number, perhaps random, based on the number of sides of the die</returns>
		int RollDie(int sides);
	}
}