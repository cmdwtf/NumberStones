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
		/// Success being <see cref="DiceBoolean.False"/> is NOT the same as the
		/// <see cref="Failure"/> property being <see cref="DiceBoolean.True"/>.
		/// Success is modified by <see cref="Options.Target"/> (e.g.: "1d20>=10", and <see cref="Failure"/>
		/// is modified by <see cref="Options.Failure"/> (e.g.: "1d6f1f2").
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public virtual DiceBoolean Success { get; init; } = DiceBoolean.Unset;
		/// <summary>
		/// A boolean representing if the value for the result is considered a failure.
		/// Failure being <see cref="DiceBoolean.False"/> is NOT the same as the
		/// <see cref="Success"/> property being <see cref="DiceBoolean.True"/>.
		/// Success is modified by <see cref="Options.Target"/> (e.g.: "1d20>=10", and <see cref="Failure"/>
		/// is modified by <see cref="Options.Failure"/> (e.g.: "1d20>=10").
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public virtual DiceBoolean Failure { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical success.
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public virtual DiceBoolean CriticalSuccess { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// A boolean representing if the value for the result is considered a critical failure.
		/// Unless modified by an option, the value will be <see cref="DiceBoolean.Unset"/>
		/// </summary>
		public virtual DiceBoolean CriticalFailure { get; init; } = DiceBoolean.Unset;

		/// <summary>
		/// A record representing if the value from this dice was dropped.
		/// </summary>
		public virtual DiceDropResult Dropped { get; init; } = DiceDropResult.NotDropped;

		/// <summary>
		/// The type of dice that was rolled to get this result.
		/// </summary>
		public virtual DiceType Type { get; init; } = DiceType.Numerical;

		/// <summary>
		/// The number of sides that this dice term had.
		/// </summary>
		public virtual decimal Sides { get; init; } = 0;

		/// <inheritdoc cref="DiceExpressionResult.ToString"/>
		public override string ToString() =>
			Dropped == false
			? base.ToString()
			: $"~~{Dropped.OriginalValue} ({TermType})~~";
	}
}
