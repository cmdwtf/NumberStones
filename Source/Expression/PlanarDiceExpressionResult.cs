using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record PlanarDiceExpressionResult(PlanarResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a Planechase dice roll.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Planar;

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceExpressionResult.ToString"/>
		public override string ToString() => Result.ToString();
	}
}
