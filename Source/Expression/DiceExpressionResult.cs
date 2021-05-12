using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record DiceExpressionResult : ExpressionResult
	{
		/// <summary>
		/// A boolean representing if the value for the result is considered a success.
		/// </summary>
		public bool Success { get; init; } = false;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical success.
		/// </summary>
		public bool CriticalSuccess { get; init; } = false;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical failure.
		/// </summary>
		public bool CriticalFailure { get; init; } = false;

		/// <summary>
		/// The type of dice that was rolled to get this result.
		/// </summary>
		public virtual DiceType Type { get; init; } = DiceType.Polyhedron;

		/// <summary>
		/// The number of sides that this dice term had.
		/// </summary>
		public decimal Sides { get; init; } = 0;
	}
}
