namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An enumeration that represents a comparison mode for a dice option to use
	/// </summary>
	public enum ComparisonDiceMode
	{
		/// <summary>
		/// No comparison mode
		/// </summary>
		None = 0,
		/// <summary>
		/// Checks if the result equals a specific number
		/// </summary>
		Equals,
		/// <summary>
		/// Checks if the result is greater than a specific number
		/// </summary>
		GreaterThan,
		/// <summary>
		/// Checks if the result is less than a specific number
		/// </summary>
		LessThan,
		/// <summary>
		/// Checks if the result is greater than or equal to a specific number
		/// </summary>
		GreaterThanEquals,
		/// <summary>
		/// Checks if the result is less than or equal to a specific number
		/// </summary>
		LessThanEquals,
		/// <summary>
		/// Checks if the result is not equal to a specific number
		/// </summary>
		Not
	}
}
