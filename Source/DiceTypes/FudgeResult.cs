namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// An enumeration representing the possible results of a Fudge (or Fate) die roll
	/// </summary>
	public enum FudgeResult
	{
		/// <summary>
		/// Zero, no value, one of the two blank sides of a Fudge (or Fate) die
		/// </summary>
		Nothing = 0,
		/// <summary>
		/// Plus, 1, one of the two "+" sides of a Fudge (or Fate) die
		/// </summary>
		Plus = 1,
		/// <summary>
		/// Minus, -1, one of the two "-" sides of a Fudge (or Fate) die
		/// </summary>
		Minus = -1
	}
}
