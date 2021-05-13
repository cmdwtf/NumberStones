namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An enumeration describing the mode of exploding dice being used
	/// </summary>
	public enum ExplodingDiceMode
	{
		/// <summary>
		/// None, not exploding
		/// </summary>
		None = 0,
		/// <summary>
		/// Classic exploding: each max roll allows another dice to be rolled
		/// </summary>
		Classic,
		/// <summary>
		/// Compound exploding: like classic, except the total value is returned as a single result
		/// </summary>
		Compound,
		/// <summary>
		/// Penetrating exploding: like classic, except any exploded dice have a -1 penalty to their value
		/// </summary>
		Penetrating
	}
}
