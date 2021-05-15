using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record CoinExpressionResult(CoinResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a coin toss.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Coin;

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceExpressionResult.ToString"/>
		public override string ToString() => Result.ToString();
	}
}
