namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// The TermResult record represents a single result of one of the terms in a Expression
	/// </summary>
	public record ExpressionResult
	{
		/// <summary>
		/// An empty expression result
		/// </summary>
		public static ExpressionResult Empty { get; } = new() { TermType = "?" };

		/// <summary>
		/// The total for this term
		/// </summary>
		public decimal Value { get; init; } = 0m;

		/// <summary>
		/// A string representing the type of this Term. Possible values are "constant" or "d(sides)"
		/// In 1d6 + 5, the 1d6 term is of type "d6" and the 5 term is of type "constant"
		/// </summary>
		public string TermType { get; init; } = string.Empty;

		/// <summary>
		/// Returns a string representing this expression result
		/// </summary>
		/// <returns>A string representing this expression result</returns>
		public override string ToString() => $"{Value} ({TermType})";
	}
}