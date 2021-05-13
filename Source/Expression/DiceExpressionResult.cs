using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing a dice expression result
	/// </summary>
	public record DiceExpressionResult : ExpressionResult
	{
		/// <summary>
		/// A boolean representing if the value for the result is considered a success.
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public DiceBoolean Success { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical success.
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public DiceBoolean CriticalSuccess { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical failure.
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public DiceBoolean CriticalFailure { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// The type of dice that was rolled to get this result.
		/// </summary>
		public virtual DiceType Type { get; init; } = DiceType.Numerical;

		/// <summary>
		/// The number of sides that this dice term had.
		/// </summary>
		public decimal Sides { get; init; } = 0;
	}
}
